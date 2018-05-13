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

        /// <summary>
        /// Danh sách đối tượng Player gồm tên và tích của người chơi
        /// </summary>
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

        /// <summary>
        /// Vị trí index của người chơi hiện tại
        /// </summary>
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

        /// <summary>
        /// Tên người chơi
        /// </summary>
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

        /// <summary>
        /// Tích đánh của người chơi
        /// </summary>
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

        /// <summary>
        /// Ma trận danh sách lưu lại các ô cờ
        /// </summary>
        private List<List<Button>> matrix;
        public List<List<Button>> Matrix
        {
            get
            {
                return matrix;
            }

            set
            {
                matrix = value;
            }
        }

        /// <summary>
        /// Event khi người chơi đánh 1 ô cờ
        /// </summary>
        private event EventHandler playerMarked;
        public event EventHandler PlayerMarked
        {
            add
            {
                playerMarked += value;
            }
            remove
            {
                playerMarked -= value;
            }
        }

        /// <summary>
        /// Event kết thúc trò chơi
        /// </summary>
        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
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
            ChessBoard.Enabled = true;
            Matrix = new List<List<Button>>();

            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                Matrix.Add(new List<Button>());

                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };

                    btn.Click += Btn_Click;

                    ChessBoard.Controls.Add(btn);

                    Matrix[i].Add(btn);

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

            if(playerMarked != null)
            {
                playerMarked(this, new EventArgs());
            }

            if (isEndGame(btn))
            {
                EndGame();
            }

        }

        public void EndGame()
        {
            if(endedGame != null)
            {
                endedGame(this, new EventArgs());
            }
        }

        private Point GetChessPoint(Button btn)
        {
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matrix[vertical].IndexOf(btn);

            Point point = new Point(horizontal, vertical);

            return point;
        }

        private bool isEndGame(Button btn)
        {
            return isEndHorizontal(btn) || isEndVertical(btn) || isEndPrimaryDiagonal(btn) || isEndSubDiagonal(btn);
        }

        /// <summary>
        /// Xử lý thắng thua theo chiều ngang
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        private bool isEndHorizontal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countLeft = 0;
            for (int i = point.X; i >= 0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                {
                    break;
                }
            }


            int countRight = 0;
            for (int i = point.X + 1; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                {
                    break;
                }
            }

            return countLeft + countRight == 5;
        }

        /// <summary>
        /// Xử lý thắng thua theo chiều dọc
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        private bool isEndVertical(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                {
                    break;
                }
            }


            int countBottom = 0;
            for (int i = point.Y + 1; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                {
                    break;
                }
            }

            return countTop + countBottom == 5;
        }

        /// <summary>
        /// xử lý thắng thua theo đường chéo chính
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        private bool isEndPrimaryDiagonal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if ((point.Y - i) < 0 || (point.X - i) < 0)
                    break;

                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                {
                    break;
                }
            }


            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if ((point.Y + i) >= Cons.CHESS_BOARD_HEIGHT || (point.X + i) >= Cons.CHESS_BOARD_WIDTH)
                    break;

                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                {
                    break;
                }
            }

            return countTop + countBottom == 5;
        }

        /// <summary>
        /// Xử lý thắng thua theo đường chéo phụ
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        private bool isEndSubDiagonal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if ((point.X + i) > Cons.CHESS_BOARD_WIDTH || (point.Y - i) < 0)
                    break;

                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                {
                    break;
                }
            }


            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if ((point.Y + i) >= Cons.CHESS_BOARD_HEIGHT || (point.X - i) < 0)
                    break;

                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                {
                    break;
                }
            }

            return countTop + countBottom == 5;
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
