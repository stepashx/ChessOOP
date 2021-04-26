using System;

namespace Chess
{

    interface I
    {
        
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
    }

    class Pawn : Piece
    {
        public Pawn(bool color) : base('P', color) {}
    }
    
    class Rook : Piece
    {
        public Rook(bool color) : base('R', color) {}
    }
    
    class Horse : Piece
    {
        public Horse(bool color) : base('H', color) {}
    }
    
    class Bishop : Piece
    {
        public Bishop(bool color) : base('B', color) {}
    }
    
    class Queen : Piece
    {
        public Queen(bool color) : base('Q', color) {}
    }
    
    class King : Piece
    {
        public King (bool color) : base('K', color) {}
    }
    class ChessBoard
    {
        private Piece[,] board;

        public ChessBoard()
        {
            board = new Piece[8, 8];

            board[0, 0] = new Rook(false);
            board[0, 1] = new Horse(false);
            board[0, 2] = new Bishop(false);
            board[0, 3] = new Queen(false);
            board[0, 4] = new King(false);
            board[0, 5] = new Bishop(false);
            board[0, 6] = new Horse(false);
            board[0, 7] = new Rook(false);
            
            for (int x = 0; x < 8; x++)
            {
                board[1, x] = new Pawn(false);
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
                board[6, x] = new Pawn(true);
            }
            
            board[7, 0] = new Rook(true);
            board[7, 1] = new Horse(true);
            board[7, 2] = new Bishop(true);
            board[7, 3] = new Queen(true);
            board[7, 4] = new King(true);
            board[7, 5] = new Bishop(true);
            board[7, 6] = new Horse(true);
            board[7, 7] = new Rook(true);
        }

        public void drawDesk()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != null) 
                    {
                        Console.Write("{0}{1}   ", (board[i,j].PieceColor ? "W" : "B"), board[i, j].NameOfPiece);
                        continue;
                    }
                    Console.Write("{0}   ", "00");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ChessBoard board = new ChessBoard();
            while (true)
            {
                string[] motion = Console.ReadLine().Split('-');
                Console.Clear();
                board.drawDesk();
                break;
            }
        }
    }
}