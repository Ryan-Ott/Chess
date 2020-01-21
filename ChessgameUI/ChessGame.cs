using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessgame
{
    class ChessGame
    {
        public ChessPiece[,] CreateChessboard()
        {
            ChessPiece[,] chessboard = new ChessPiece[8, 8];
            return chessboard;
        }

        public void InitChessboard(ChessPiece[,] chessboard)
        {
            for (int r = 0; r < chessboard.GetLength(0); r++)
            {
                for (int c = 0; c < chessboard.GetLength(1); c++)
                {
                    chessboard[r, c] = null;
                }
            }
            PutChessPieces(chessboard);
        }

        void PutChessPieces(ChessPiece[,] chessboard)
        {
            ChessPieceType[] order = {ChessPieceType.Rook, ChessPieceType.Knight, ChessPieceType.Bishop, ChessPieceType.King,
                                      ChessPieceType.Queen, ChessPieceType.Bishop, ChessPieceType.Knight, ChessPieceType.Rook};

            for (int c = 0; c < chessboard.GetLength(1); c++)
            {
                chessboard[1, c] = new ChessPiece { colour = ChessPieceColour.White, type = ChessPieceType.Pawn };
                chessboard[6, c] = new ChessPiece { colour = ChessPieceColour.Black, type = ChessPieceType.Pawn };
            }

            for (int c = 0; c < chessboard.GetLength(1); c++)
            {
                chessboard[0, c] = new ChessPiece { colour = ChessPieceColour.White, type = order[c] };
                chessboard[7, c] = new ChessPiece { colour = ChessPieceColour.Black, type = order[c] };
            }
        }

        public void DoMove(ChessPiece[,] chessboard, Position from, Position to)
        {
            chessboard[to.row, to.col] = chessboard[from.row, from.col];
            chessboard[from.row, from.col] = null;
        }

        public void CheckMove(ChessPiece[,] chessboard, Position from, Position to)
        {
            if (chessboard[from.row, from.col] == null)
            {
                throw new Exception("No chesspiece at current 'from' coordinates");
            }
            else if (!(chessboard[to.row, to.col] == null))
            {
                throw new Exception("A chesspiece is already at the currently chosen 'to' coordinates");
            }
            else if (!ValidMove(chessboard[from.row, from.col], from, to))
            {
                throw new Exception("Move is invalid for this chesspiece");
            }
            else if (from.col >= chessboard.GetLength(1) || from.row >= chessboard.GetLength(0) || to.col >= chessboard.GetLength(1) || to.row >= chessboard.GetLength(0))
            {
                throw new Exception("Coordinates out of bounds");
            }
        }

        public bool ValidMove(ChessPiece chessPiece, Position from, Position to)
        {
            int hor = Math.Abs(to.col - from.col);
            int ver = Math.Abs(to.row - from.row);

            switch (chessPiece.type)
            {
                case ChessPieceType.Pawn:
                    if (hor == 0 && ver == 1)
                    {
                        return true;
                    }
                    break;
                case ChessPieceType.Rook:
                    if (hor * ver == 0)
                    {
                        return true;
                    }
                    break;
                case ChessPieceType.Knight:
                    if (hor * ver == 2)
                    {
                        return true;
                    }
                    break;
                case ChessPieceType.Bishop:
                    if (hor == ver)
                    {
                        return true;
                    }
                    break;
                case ChessPieceType.Queen:
                    if (hor * ver == 0 || hor == ver)
                    {
                        return true;
                    }
                    break;
                case ChessPieceType.King:
                    if (hor == 1 || ver == 1 || (hor == 1 && ver == 1))
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}
