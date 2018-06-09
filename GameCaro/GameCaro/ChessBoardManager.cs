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
        private event EventHandler<ButtonClickEvent> playerMarked;
        public event EventHandler<ButtonClickEvent> PlayerMarked
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

        /// <summary>
        /// Tạo stack lưu lại tạo độ của quân cờ khi đánh
        /// </summary>
        private Stack<PlayInfo> playTimeLine;
        public Stack<PlayInfo> PlayTimeLine
        {
            get
            {
                return playTimeLine;
            }

            set
            {
                playTimeLine = value;
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
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm tạo bàng cờ
        /// </summary>
        public void DrawChessBoard()
        {
            ChessBoard.Enabled = true;
            ChessBoard.Controls.Clear();

            PlayTimeLine = new Stack<PlayInfo>();

            CurrentPlayer = 0;

            ChangePlayer();

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

            PlayTimeLine.Push(new PlayInfo(GetChessPoint(btn), CurrentPlayer));

            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;

            ChangePlayer();

            if (playerMarked != null)
            {
                playerMarked(this, new ButtonClickEvent(GetChessPoint(btn)));
            }

            if (isEndGame(btn))
            {
                EndGame();
            }
        }

        /// <summary>
        /// Xử lý khi người chơi khác đánh
        /// </summary>
        /// <param name="point"></param>
        public void OtherPlayerMark(Point point)
        {
            Button btn = Matrix[point.Y][point.X];

            if (btn.BackgroundImage != null)
                return;

            Mark(btn);

            PlayTimeLine.Push(new PlayInfo(GetChessPoint(btn), CurrentPlayer));

            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;

            ChangePlayer();

            if (isEndGame(btn))
            {
                EndGame();
            }
        }

        public void EndGame()
        {
            if (endedGame != null)
            {
                endedGame(this, new EventArgs());
            }
        }

        /// <summary>
        /// Đi lại cho quân cờ
        /// </summary>
        /// <returns></returns>
        public bool Undo()
        {
            if (PlayTimeLine.Count <= 0)
                return false;

            bool isUndo1 = UndoAStep();
            bool isUndo2 = UndoAStep();

            if (PlayTimeLine.Count <= 0)
            {
                CurrentPlayer = 0;
            }
            else
            {         
                PlayInfo oldPoint = PlayTimeLine.Peek();
                CurrentPlayer = oldPoint.CurrentPlayer == 1 ? 0 : 1;
            }

            return isUndo1 && isUndo2;
        }

        public bool UndoAStep()
        {
            if (PlayTimeLine.Count <= 0)
                return false;
            PlayInfo oldPoint = PlayTimeLine.Pop();
            Button btn = Matrix[oldPoint.Point.Y][oldPoint.Point.X];

            btn.BackgroundImage = null;

            if (PlayTimeLine.Count <= 0)
            {
                CurrentPlayer = 0;
            }
            else
            {
                oldPoint = PlayTimeLine.Peek();
                CurrentPlayer = oldPoint.CurrentPlayer == 1 ? 0 : 1;
            }

            ChangePlayer();

            return true;
        }

        /// <summary>
        /// Lấy vị trí X, Y của button được nhấn trong list Matrix
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
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
        }

        /// <summary>
        /// Thay lượt người chơi
        /// </summary>
        private void ChangePlayer()
        {
            //đổi tên người chơi
            PlayerName.Text = Player[CurrentPlayer].Name;

            PlayerMark.Image = Player[CurrentPlayer].Mark;
        }
        #endregion

        #region AI

        /// <summary>
        /// Mảng lưu lại điểm tấn công và phòng thủ để đưa ra nước đi tiếp theo tốt nhất
        /// </summary>
        private long[] ArrayPointAttack = new long[7] { 0, 3, 24, 192, 1536, 12288, 98304 };
        private long[] ArrayPointDefend = new long[7] { 0, 1, 9, 81, 729, 6561, 59049 };

        /// <summary>
        /// Khởi động điểm đi tiếp theo cho AI
        /// </summary>
        public void StartComputer()
        {
            if(PlayTimeLine.Count == 0)
            {
                OtherPlayerMark(new Point(Cons.CHESS_BOARD_WIDTH / 2, Cons.CHESS_BOARD_HEIGHT / 2));
            }
            else
            {
                Point point = FindChess();
                OtherPlayerMark(point);
            }
        }

        /// <summary>
        /// Hàm tìm kiếm nước đi tiếp theo cho ô AI
        /// </summary>
        /// <returns></returns>
        private Point FindChess()
        {
            Point point = new Point();
            long MaxPoint = 0;
            
            for(int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                for(int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    if(Matrix[i][j].BackgroundImage == Player[0].Mark)
                    {
                        long AttackPoint = PointAttackVertical(i, j) + PointAttackHorizontal(i, j) + PointAttackSubDiagonal(i, j) + PointAttackPrimaryDiagonal(i, j);
                        long DefendPoint = PointDefendVertical(i, j) + PointDefendHorizontal(i, j) + PointDefendSubDiagonal(i, j) + PointDefendPrimaryDiagonal(i, j);
                        long TempPoint = AttackPoint > DefendPoint ? AttackPoint : DefendPoint;
                        if(MaxPoint < TempPoint)
                        {
                            MaxPoint = TempPoint;
                            point = new Point(i, j);
                        }
                    }
                }
            }
            return point;
        }


        #region Điểm tấn công
        /// <summary>
        /// Tính điểm theo chiều dọc của bàn cờ
        /// </summary>
        /// <param name="currentRow">Vị trí dòng hiện tại của ô cờ được đánh</param>
        /// <param name="currentCol">Vị trí cột hiện tại của ô cờ được đánh</param>
        /// <returns></returns>
        private long PointAttackVertical(int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for(int i = 1; i < 7 && currentRow + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[currentRow + i][currentCol].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                }
                else if (Matrix[currentRow + i][currentCol].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 7 && currentRow - i >= 0; i++)
            {
                if (Matrix[currentRow - i][currentCol].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                }
                else if (Matrix[currentRow - i][currentCol].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            if (ChessPlayer == 2)
                return 0;

            TotalPoint -= ArrayPointDefend[ChessPlayer + 1];
            TotalPoint += ArrayPointAttack[ChessComputer];
            return TotalPoint;
        }

        /// <summary>
        /// Tính điểm theo chiều ngang của bàn cờ
        /// </summary>
        /// <param name="currentRow">Vị trí dòng hiện tại của ô cờ được đánh</param>
        /// <param name="currentCol">Vị trí cột hiện tại của ô cờ được đánh</param>
        /// <returns></returns>
        private long PointAttackHorizontal(int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 7 && currentCol + i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[currentRow][currentCol + i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                }
                else if (Matrix[currentRow][currentCol + i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 7 && currentCol - i >= 0; i++)
            {
                if (Matrix[currentRow][currentCol - i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                }
                else if (Matrix[currentRow][currentCol - i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            if (ChessPlayer == 2)
                return 0;

            TotalPoint -= ArrayPointDefend[ChessPlayer + 1];
            TotalPoint += ArrayPointAttack[ChessComputer];
            return TotalPoint;
        }

        /// <summary>
        /// Tính điểm theo đường chéo phụ
        /// </summary>
        /// <param name="currentRow">Vị trí dòng hiện tại của ô cờ được đánh</param>
        /// <param name="currentCol">Vị trí cột hiện tại của ô cờ được đánh</param>
        /// <returns></returns>
        private long PointAttackSubDiagonal(int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 7 && currentCol + i < Cons.CHESS_BOARD_WIDTH && currentRow - i >= 0; i++)
            {
                if (Matrix[currentRow - i][currentCol + i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                }
                else if (Matrix[currentRow - i][currentCol + i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 7 && currentCol - i >= 0 && currentRow + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[currentRow + i][currentCol - i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                }
                else if (Matrix[currentRow + i][currentCol - i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            if (ChessPlayer == 2)
                return 0;

            TotalPoint -= ArrayPointDefend[ChessPlayer + 1];
            TotalPoint += ArrayPointAttack[ChessComputer];
            return TotalPoint;
        }

        /// <summary>
        /// Tính điểm theo đường chéo chính
        /// </summary>
        /// <param name="currentRow">Vị trí dòng hiện tại của ô cờ được đánh</param>
        /// <param name="currentCol">Vị trí cột hiện tại của ô cờ được đánh</param>
        /// <returns></returns>
        private long PointAttackPrimaryDiagonal(int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 7 && currentCol + i < Cons.CHESS_BOARD_WIDTH && currentRow + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[currentRow + i][currentCol + i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                }
                else if (Matrix[currentRow + i][currentCol + i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 7 && currentCol - i >= 0 && currentRow - i >= 0; i++)
            {
                if (Matrix[currentRow - i][currentCol - i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                }
                else if (Matrix[currentRow - i][currentCol - i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                    break;
                }
                else
                {
                    break;
                }
            }

            if (ChessPlayer == 2)
                return 0;

            TotalPoint -= ArrayPointDefend[ChessPlayer + 1];
            TotalPoint += ArrayPointAttack[ChessComputer];
            return TotalPoint;
        }
        #endregion

        #region Điểm phòng ngự

        /// <summary>
        /// Tính điểm theo chiều dọc của bàn cờ
        /// </summary>
        /// <param name="currentRow">Vị trí dòng hiện tại của ô cờ được đánh</param>
        /// <param name="currentCol">Vị trí cột hiện tại của ô cờ được đánh</param>
        /// <returns></returns>
        private long PointDefendVertical(int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 7 && currentRow + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[currentRow + i][currentCol].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                    break;
                }
                else if (Matrix[currentRow + i][currentCol].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 7 && currentRow - i >= 0; i++)
            {
                if (Matrix[currentRow - i][currentCol].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                    break;
                }
                else if (Matrix[currentRow - i][currentCol].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            if (ChessComputer == 2)
                return 0;

            TotalPoint += ArrayPointDefend[ChessPlayer];
            return TotalPoint;
        }

        /// <summary>
        /// Tính điểm theo chiều ngang của bàn cờ
        /// </summary>
        /// <param name="currentRow">Vị trí dòng hiện tại của ô cờ được đánh</param>
        /// <param name="currentCol">Vị trí cột hiện tại của ô cờ được đánh</param>
        /// <returns></returns>
        private long PointDefendHorizontal(int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 7 && currentCol + i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[currentRow][currentCol + i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                    break;
                }
                else if (Matrix[currentRow][currentCol + i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 7 && currentCol - i >= 0; i++)
            {
                if (Matrix[currentRow][currentCol - i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                    break;
                }
                else if (Matrix[currentRow][currentCol - i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            if (ChessComputer == 2)
                return 0;

            TotalPoint += ArrayPointDefend[ChessPlayer];
            return TotalPoint;
        }

        /// <summary>
        /// Tính điểm theo đường chéo phụ
        /// </summary>
        /// <param name="currentRow">Vị trí dòng hiện tại của ô cờ được đánh</param>
        /// <param name="currentCol">Vị trí cột hiện tại của ô cờ được đánh</param>
        /// <returns></returns>
        private long PointDefendSubDiagonal(int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 7 && currentCol + i < Cons.CHESS_BOARD_WIDTH && currentRow - i >= 0; i++)
            {
                if (Matrix[currentRow - i][currentCol + i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                    break;
                }
                else if (Matrix[currentRow - i][currentCol + i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 7 && currentCol - i >= 0 && currentRow + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[currentRow + i][currentCol - i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                    break;
                }
                else if (Matrix[currentRow + i][currentCol - i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            if (ChessComputer == 2)
                return 0;
            
            TotalPoint += ArrayPointDefend[ChessPlayer];
            return TotalPoint;
        }

        /// <summary>
        /// Tính điểm theo đường chéo chính
        /// </summary>
        /// <param name="currentRow">Vị trí dòng hiện tại của ô cờ được đánh</param>
        /// <param name="currentCol">Vị trí cột hiện tại của ô cờ được đánh</param>
        /// <returns></returns>
        private long PointDefendPrimaryDiagonal(int currentRow, int currentCol)
        {
            long TotalPoint = 0;
            int ChessPlayer = 0;
            int ChessComputer = 0;

            for (int i = 1; i < 7 && currentCol + i < Cons.CHESS_BOARD_WIDTH && currentRow + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[currentRow + i][currentCol + i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                    break;
                }
                else if (Matrix[currentRow + i][currentCol + i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 7 && currentCol - i >= 0 && currentRow - i >= 0; i++)
            {
                if (Matrix[currentRow - i][currentCol - i].BackgroundImage == Player[0].Mark)
                {
                    ChessComputer++;
                    break;
                }
                else if (Matrix[currentRow - i][currentCol - i].BackgroundImage == Player[1].Mark)
                {
                    ChessPlayer++;
                }
                else
                {
                    break;
                }
            }

            if (ChessComputer == 2)
                return 0;

            TotalPoint += ArrayPointDefend[ChessPlayer];
            return TotalPoint;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Tạo class Event để lưu lại tọa độ button được click
    /// </summary>
    public class ButtonClickEvent: EventArgs
    {
        private Point clickedPoint;

        public Point ClickedPoint
        {
            get
            {
                return clickedPoint;
            }

            set
            {
                clickedPoint = value;
            }
        }

        public ButtonClickEvent(Point point)
        {
            this.ClickedPoint = point;
        }
    }
}
