using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class Form1 : Form
    {
        #region Properties
        ChessBoardManager ChessBoard;
        SocketManager socket;
        Boolean isLanGame;
        Language lang = new Language();
        #endregion
        public Form1()
        {
            InitializeComponent();

            //Tránh xung đột thay đổi giao diện khi sử dụng Multi thread
            Control.CheckForIllegalCrossThreadCalls = false;

            isLanGame = false;

            ChessBoard = new ChessBoardManager(pnlChessBoard, txtPlayerName, pctbMark);
            ChessBoard.EndedGame += ChessBoard_EndedGame;
            ChessBoard.PlayerMarked += ChessBoard_PlayerMarked;

            prcbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;

            tmCoolDown.Interval = Cons.COOL_DOWN_INTERVAL;

            socket = new SocketManager();
            NewGame();
        }

        #region Methods

        private void btnLAN_Click(object sender, EventArgs e)
        {
            btnLAN.Enabled = false;
            isLanGame = true;
            socket.IP = txtIP.Text;

            if (!socket.ConnectServer()) //không kết nối tới được server thì tiến hành tạo server
            {
                socket.isServer = true;
                pnlChessBoard.Enabled = true;
                socket.CreateServer();
            }
            else
            {
                socket.isServer = false;
                pnlChessBoard.Enabled = false;
                // client lắng nghe tin từ server
                Listen();
            }
        }

        void EndGame()
        {
            tmCoolDown.Stop();
            pnlChessBoard.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            if(!isLanGame)
            {
                if (ChessBoard.NumberPlayerWin == 0)
                {
                    MessageBox.Show("Computer thắng");
                }
                else if(ChessBoard.NumberPlayerWin == 1)
                {
                    MessageBox.Show("Bạn đã thắng");
                }
                    
            }           
            //MessageBox.Show("End Game");
        }

        void NewGame()
        {
            prcbCoolDown.Value = 0;
            tmCoolDown.Stop();
            undoToolStripMenuItem.Enabled = true;

            //EnableElement(false);

            ChessBoard.DrawChessBoard();
        }

        void Undo()
        {
            ChessBoard.Undo();
        }

        void Quit()
        {
            Application.Exit();
        }

        void ChessBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            tmCoolDown.Start();
            
            prcbCoolDown.Value = 0;
        
            if (isLanGame)
            {
                pnlChessBoard.Enabled = false;
                undoToolStripMenuItem.Enabled = false;
                socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));

                Listen();
            }else
            {
                if(ChessBoard.CurrentPlayer == 0)
                {
                    ChessBoard.StartComputer();
                }            
            }
            
        }

        void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            EndGame();

            if(isLanGame)
            {
                socket.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));
            }
        }

        /// <summary>
        /// Sư kiện khi timer chạy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();

            if(prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                EndGame();
                if(isLanGame)
                {
                    socket.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));
                }
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
            if(isLanGame)
            {
                socket.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
            }      
            pnlChessBoard.Enabled = true;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
            if(isLanGame)
            {
                socket.Send(new SocketData((int)SocketCommand.UNDO, "", new Point()));
            }            
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
            if(isLanGame)
            {
                socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
            }           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit !!!", "Warning", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
                }
                catch
                {
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            if (string.IsNullOrEmpty(txtIP.Text))
            {
                txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
            //txtIP.Text = socket.GetLocalIPAddress();
        }

        private void EnableElement(Boolean check)
        {
            isLanGame = check;
            txtIP.Enabled = check;
            btnLAN.Enabled = check;
            txtIP.Visible = check;
            btnLAN.Visible = check;
        }

        private void playersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnableElement(true);
        }

        private void playerVsComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnableElement(false);

            ChessBoard.StartComputer();
        }

        void Listen()
        {
            //Tránh bị lỗi khi 1 bên thoát đột ngột
            try
            {
                Thread listenThread = new Thread(() =>
                {
                    SocketData data = (SocketData)socket.Receive();

                    ProcessData(data);
                });
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch
            {
                
            }
        }

        private void ProcessData(SocketData data)
        {
            switch (data.Command)
            {
                case (int)SocketCommand.NOTIFY:
                    MessageBox.Show(data.Message);
                    break;

                case (int)SocketCommand.NEW_GAME:
                    this.Invoke((MethodInvoker)(() => 
                    {
                        NewGame();
                        pnlChessBoard.Enabled = false;
                    }));
                    break;

                case (int)SocketCommand.SEND_POINT:
                    //vì timer chạy thread khác với main nên phải đưa vào invoke
                    this.Invoke((MethodInvoker)(() =>
                    {
                        prcbCoolDown.Value = 0;
                        pnlChessBoard.Enabled = true;
                        tmCoolDown.Start();
                        ChessBoard.OtherPlayerMark(data.Point);
                        undoToolStripMenuItem.Enabled = true;
                    }));
                    break;

                case (int)SocketCommand.UNDO:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        Undo();
                        prcbCoolDown.Value = 0;
                    }));
                    break;

                case (int)SocketCommand.END_GAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        if (ChessBoard.NumberPlayerWin == 0)
                        {
                            MessageBox.Show("Người chơi 1 thắng");
                        }
                        else if (ChessBoard.NumberPlayerWin == 1)
                        {
                            MessageBox.Show("Người chơi 2 thắng");
                        }
                    }));
                    break;

                case (int)SocketCommand.TIME_OUT:
                    MessageBox.Show("Hết giờ");
                    break;

                case (int)SocketCommand.QUIT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        tmCoolDown.Stop();
                        MessageBox.Show("Người chơi đã thoát");
                    }));
                    break;

                default:
                    break;
            }

            Listen();
        }

        #endregion

        #region DesignForm
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Move form
        bool IsDown = false;
        Point FirstPosition;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            IsDown = true;
            FirstPosition = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDown == true)
            {
                Point p = new Point();
                p.X = this.Location.X + (e.X - FirstPosition.X);
                p.Y = this.Location.Y + (e.Y - FirstPosition.Y);
                this.Location = p;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            IsDown = false;
        }

        string s = "5 items in a line to win";
        string[] l;
        int i = 0, j = 0;
        private void tmAnimationLabel_Tick(object sender, EventArgs e)
        {
            //adding the characters one by one to the label2
            if (i < s.Length - 1)
            {
                label2.Text += l[i].ToString();
            }
            //starting the third label after 3 charaters of label2 adding
            if (i >= 3 && i <= 27)
            {
                if (i <= s.Length + 2)
                    label3.Text += l[j].ToString();
                j++;
            }
            if (j <= s.Length)
                i++;
            else
            {
                i = j = 0;
                label3.Text = label2.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupAnimationLabel();
            btnLAN.ButtonText = "Kết nối";
        }

        private void tsmiViet_CheckedChanged(object sender, EventArgs e)
        {
            if (tsmiViet.Checked)
            {
                btnLAN.ButtonText = "Kết nối";
                this.lang.SetLanguage((int)eLanguage.TiengViet);
                this.lang.ChangeLanguage(this.Name, this);
                this.tsmiAnh.Checked = false;
            }
        }

        private void tsmiAnh_CheckedChanged(object sender, EventArgs e)
        {
            if (tsmiAnh.Checked)
            {
                btnLAN.ButtonText = "Connect";
                this.lang.SetLanguage((int)eLanguage.TiengAnh);
                this.lang.ChangeLanguage(this.Name, this);
                this.tsmiViet.Checked = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.facebook.com/Nguyen.Gamer.UIT");
        }

        private void SetupAnimationLabel()
        {
            label1.Text = s;
            // converting to string to string array
            l = new string[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                l[i] = s[i].ToString();
            }
            //setting up the 3 label's Location and font properties into same
            label1.Location = label2.Location = label3.Location = new Point(3, 171);
            label1.Font = label2.Font = label3.Font = new System.Drawing.Font("Elephant",
                14.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((byte)(0)));
            //giving different color to the middile label(label2)
            this.label1.ForeColor = this.label3.ForeColor = Color.White;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            label2.Text = label3.Text = "";

            tmAnimationLabel.Start();
        }
        #endregion

    }
}
