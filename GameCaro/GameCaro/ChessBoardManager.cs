using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public class ChessBoardManager
    {
        #region Properties
        private Panel chessBoard;

        public Panel ChessBoard
        {
            get
            {
                return chessBoard;
            }

            set
            {
                chessBoard = value;
            }
        }

        private List<Player> player;

        public List<Player> Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }

        private int currentPlayer;

        public int CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }

            set
            {
                currentPlayer = value;
            }
        }

        private TextBox playerName;

        public TextBox PlayerName
        {
            get
            {
                return playerName;
            }

            set
            {
                playerName = value;
            }
        }

        private PictureBox playerMark;

        public PictureBox PlayerMark
        {
            get
            {
                return playerMark;
            }

            set
            {
                playerMark = value;
            }
        }

        

        #endregion

        #region Initialize
        public ChessBoardManager(Panel chessBoard, TextBox playerName, PictureBox mark)
        {
            this.ChessBoard = chessBoard;
            this.PlayerName = playerName;
            this.PlayerMark = mark;
            
            this.Player = new List<Player>()
            {
                new Player("Player 1", Image.FromFile(Application.StartupPath + "\\Resources\\P1.png")),
                new Player("Player 2", Image.FromFile(Application.StartupPath + "\\Resources\\P2.png"))
            };

            CurrentPlayer = 0;

            ChangePlayer();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm tạo bàng cờ
        /// </summary>
        public void DrawChessBoard()
        { 
            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch
                    };

                    btn.Click += Btn_Click;

                    ChessBoard.Controls.Add(btn);
                    oldButton = btn;
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + Cons.CHESS_HEIGHT);
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
        }

        //sự kiện click cho ô cờ
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.BackgroundImage != null)
                return;

            Mark(btn);

            ChangePlayer();

            
        }

        private void Mark(Button btn)
        {
            //đổi icon x, o
            btn.BackgroundImage = Player[CurrentPlayer].Mark;

            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
        }

        private void ChangePlayer()
        {
            //đổi tên người chơi
            PlayerName.Text = Player[CurrentPlayer].Name;

            PlayerMark.Image = Player[CurrentPlayer].Mark;
        }
        #endregion
    }
}
