using System;
using System.Threading.Tasks.Dataflow;

namespace Chess
{
    // Класс проверки хода
    class MoveFunctions
    {
        // Проверка хода для ладьи
        static public bool CanRookMove(int initialX, int initialY, int newX, int newY, ChessBoard board)
        {
            bool canMove = true;

            if (initialX != newX && initialY != newY)
            {
                return false;
            }
            else if (initialX != newX)
            {
                if (initialX > newX)
                {
                    for (int i = initialX - 1; i > newX; i--)
                    {
                        if (board.Board[initialY, i] != null)
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = initialX + 1; i < newX; i++)
                    {
                        if (board.Board[initialY, i] != null)
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
            }
            else if (initialY != newY)
            {
                if (initialY > newY)
                {
                    for (int i = initialY - 1; i > newY; i--)
                    {
                        if (board.Board[i, initialX] != null)
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = initialY + 1; i < newY; i++)
                    {
                        if (board.Board[i, initialX] != null)
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
            }

            return canMove;
        }

        // Проверка хода для коня
        static public bool CanHorseMove(int initialX, int initialY, int newX, int newY)
        {
            return (((Math.Abs(newX - initialX) == 1 && Math.Abs(newY - initialY) == 2) ||
                     (Math.Abs(newY - initialY) == 1 && Math.Abs(newX - initialX) == 2)));
        }
        
        // Проверка хода для слона
        static public bool CanBishopMove(int initialX, int initialY, int newX, int newY, ChessBoard board)
        {
            bool canMove = true;


            if (Math.Abs(initialX - newX) == Math.Abs(initialY - newY))
            {
                if (initialX < newX && initialY < newY)
                {
                    for (int x = initialX + 1, y = initialY + 1;
                        x < newX && y < newY;
                        x++, y++)
                    {
                        if (board.Board[y, x] != null)
                        {
                            canMove = false;
                            break;
                        }
                    }
                } 
                else if (initialX < newX && initialY > newY)
                {
                    for (int x = initialX + 1, y = initialY - 1;
                        x < newX && y > newY;
                        x++, y--)
                    {
                        if (board.Board[y, x] != null)
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
                else if (initialX > newX && initialY < newY)
                {
                    for (int x = initialX - 1, y = initialY + 1;
                        x > newX && y < newY;
                        x--, y++)
                    {
                        if (board.Board[y, x] != null)
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
                else if (initialX > newX && initialY > newY)
                {
                    for (int x = initialX - 1, y = initialY - 1;
                        x > newX && y > newY;
                        x--, y--)
                    {
                        if (board.Board[y, x] != null)
                        {
                            canMove = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                canMove = false;
            }

            return canMove;
        }
        
        // Проверка хода для короля
        static public bool CanKingMove(int initialX, int initialY, int newX, int newY)
        {
            return ((Math.Abs(initialX - newX) == 1 && Math.Abs(initialY - newY) == 1) ||
                    (Math.Abs(initialX - newX) == 1 && Math.Abs(initialY - newY) == 0) ||
                    (Math.Abs(initialX - newX) == 0 && Math.Abs(initialY - newY) == 1));
        }

        // Проверка хода для пешки
        static public bool CanPawnMove(int initialX, int initialY, int newX, int newY, bool color, ChessBoard board)
        {
            bool canMove = true;
            
            if (Math.Abs(initialX - newX) > 1)
            {
                canMove = false;
            }
            else if (color)
            {
                if (initialY < newY || Math.Abs(initialY - newY) > 2 || Math.Abs(initialY - newY) == 2 && initialY != 6)
                {
                    canMove = false;
                }

                if (Math.Abs(initialX - newX) == 1)
                {
                    if (initialY - newY != 1 || board.Board[newY, newX] == null)
                    {
                        canMove = false;
                    }
                }

                if (initialX == newX && board.Board[newY, newX] != null)
                {
                    canMove = false;
                }
            }
            else if (!color)
            {
                if (initialY > newY || Math.Abs(initialY - newY) > 2 || Math.Abs(initialY - newY) == 2 && initialY != 1)
                {
                    canMove = false;
                }
                
                if (Math.Abs(initialX - newX) == 1)
                {
                    if (newY - initialY != 1 || board.Board[newY, newX] == null)
                    {
                        canMove = false;
                    }
                } 
                
                if (initialX == newX && board.Board[newY, newX] != null)
                {
                    canMove = false;
                }
            }

            return canMove;
        }
    }
    
    class Piece
    {
        private char nameOfPiece;
        private bool pieceColor;
        
        public char NameOfPiece => nameOfPiece;
        public bool PieceColor => pieceColor;

        protected Piece(char name, bool color)
        {
            nameOfPiece = name;
            pieceColor = color;
        }

        public virtual bool Move(int newX, int newY, ChessBoard board)
        {
            return true;
        }
    }

    class Pawn : Piece
    {
        private int x;
        private int y;

        public Pawn(bool color, int x, int y) : base('P', color)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Move(int newX, int newY, ChessBoard board)
        {
            if (MoveFunctions.CanPawnMove(this.x, this.y, newX, newY, PieceColor, board))
            {
                board.Board[newY, newX] = board.Board[this.y, this.x];
                board.Board[this.y, this.x] = null;
                this.x = newX;
                this.y = newY;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Rook : Piece
    {
        private int x;
        private int y;
        public Rook(bool color, int x, int y) : base('R', color)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Move(int newX, int newY, ChessBoard board)
        {
            if (MoveFunctions.CanRookMove(this.x, this.y, newX, newY, board))
            {
                board.Board[newY, newX] = board.Board[this.y, this.x];
                board.Board[this.y, this.x] = null;
                this.x = newX;
                this.y = newY;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Horse : Piece
    {
        private int x;
        private int y;
        public Horse(bool color, int x, int y) : base('H', color)
        {
            this.x = x;
            this.y = y;
        }
        
        public override bool Move(int newX, int newY, ChessBoard board)
        {
            if (MoveFunctions.CanHorseMove(this.x, this.y, newX, newY))
            {
                board.Board[newY, newX] = board.Board[this.y, this.x];
                board.Board[this.y, this.x] = null;
                this.x = newX;
                this.y = newY;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Bishop : Piece
    {
        private int x;
        private int y;
        public Bishop(bool color, int x, int y) : base('B', color)
        {
            this.x = x;
            this.y = y;
        }
        
        public override bool Move(int newX, int newY, ChessBoard board)
        {
            if (MoveFunctions.CanBishopMove(this.x, this.y, newX, newY, board))
            {
                board.Board[newY, newX] = board.Board[this.y, this.x];
                board.Board[this.y, this.x] = null;
                this.x = newX;
                this.y = newY;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Queen : Piece
    {
        private int x;
        private int y;
        public Queen(bool color, int x, int y) : base('Q', color)
        {
            this.x = x;
            this.y = y;
        }
        
        public override bool Move(int newX, int newY, ChessBoard board)
        {
            if (MoveFunctions.CanRookMove(this.x, this.y, newX, newY, board) ||
                MoveFunctions.CanBishopMove(this.x, this.y, newX, newY, board))
            {
                board.Board[newY, newX] = board.Board[this.y, this.x];
                board.Board[this.y, this.x] = null;
                this.x = newX;
                this.y = newY;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class King : Piece
    {
        private int x;
        private int y;
        public King(bool color, int x, int y) : base('K', color)
        {
            this.x = x;
            this.y = y;
        }
        
        public override bool Move(int newX, int newY, ChessBoard board)
        {
            if (MoveFunctions.CanKingMove(this.x, this.y, newX, newY))
            {
                board.Board[newY, newX] = board.Board[this.y, this.x];
                board.Board[this.y, this.x] = null;
                this.x = newX;
                this.y = newY;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class ChessBoard
    {
        private Piece[,] board;

        public Piece[,] Board
        {
            get => board;
            set => board = value;
        }

        public ChessBoard()
        {
            board = new Piece[8, 8];

            board[0, 0] = new Rook(false, 0, 0);
            board[0, 1] = new Horse(false, 1, 0);
            board[0, 2] = new Bishop(false, 2, 0);
            board[0, 3] = new Queen(false, 3, 0);
            board[0, 4] = new King(false, 4, 0);
            board[0, 5] = new Bishop(false, 5, 0);
            board[0, 6] = new Horse(false, 6, 0);
            board[0, 7] = new Rook(false, 7, 0);

            for (int x = 0; x < 8; x++)
            {
                board[1, x] = new Pawn(false, x, 1);
            }

            for (int y = 2; y < 6; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[y, x] = null;
                }
            }

            for (int x = 0; x < 8; x++)
            {
                board[6, x] = new Pawn(true, x, 6);
            }

            board[7, 0] = new Rook(true, 0, 7);
            board[7, 1] = new Horse(true, 1, 7);
            board[7, 2] = new Bishop(true, 2, 7);
            board[7, 3] = new Queen(true, 3, 7);
            board[7, 4] = new King(true, 4, 7);
            board[7, 5] = new Bishop(true, 5, 7);
            board[7, 6] = new Horse(true, 6, 7);
            board[7, 7] = new Rook(true, 7, 7);
        }

        public void drawDesk()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != null)
                    {
                        Console.Write("{0}{1}    ", (board[i, j].PieceColor ? "W" : "B"), board[i, j].NameOfPiece);
                        continue;
                    }

                    Console.Write("{0}    ", "__");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static Tuple<int, int> Transform(char a, int b)
        {
            Tuple<int, int> tuple = new Tuple<int, int>(a - 'a', 8 - b);
            return tuple;
        }
        static void Main(string[] args)
        {
            ChessBoard board = new ChessBoard();
            bool isWhiteTurn = true;
            while (true)
            {
                try
                {
                    Console.Clear();
                    board.drawDesk();
                    string[] motion = Console.ReadLine().Split('-');
                    Tuple<int, int> position = Transform(motion[0][0], motion[0][1] - '0');
                    Tuple<int, int> newPosition = Transform(motion[1][0], motion[1][1] - '0');
                    if (board.Board[position.Item2, position.Item1].PieceColor == isWhiteTurn)
                    {
                        if (board.Board[newPosition.Item2, newPosition.Item1] == null ||
                            board.Board[newPosition.Item2, newPosition.Item1].PieceColor != isWhiteTurn)
                        {
                            if (board.Board[position.Item2, position.Item1]
                                .Move(newPosition.Item1, newPosition.Item2, board))
                            {
                                isWhiteTurn = !isWhiteTurn;
                            }
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}