using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessgame
{
    class UserInteraction
    {
        static void Main(string[] args)
        {
            UserInteraction program = new UserInteraction();
            program.Start();
            Console.ReadKey();
        }

        void Start()
        { 
            ChessGame chessGame = new ChessGame();
            ChessPiece[,] chessboard = chessGame.CreateChessboard();
            chessGame.InitChessboard(chessboard);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("CONSOLE CHESS V1.1\n");
            Console.ResetColor();
            DisplayChessboard(chessboard);
            PlayChess(chessboard, chessGame);
        }

        void PlayChess(ChessPiece[,] chessboard, ChessGame chessGame)
        {
            int move = 1;
            while (true)
            {
                Turn turn = chessGame.DetermineTurn(move);
                Console.WriteLine($"{turn}'s turn");
                Position fromPos = ReadPosition("Enter 'from' position: ");
                Position toPos = ReadPosition("Enter 'to' position: ");
                try
                {
                    chessGame.CheckMove(chessboard, fromPos, toPos, turn);
                    chessGame.DoMove(chessboard, fromPos, toPos);
                    move++;
                    DisplayChessboard(chessboard);
                    DisplayEliminatedPieces(chessGame);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
        }

        void DisplayChessboard(ChessPiece[,] chessboard)
        {
            Console.WriteLine("\n   A  B  C  D  E  F  G  H");
            for (int r = 0; r < chessboard.GetLength(0); r++)
            {
                Console.Write(r + 1 + " ");
                for (int c = 0; c < chessboard.GetLength(1); c++)
                {
                    if ((r + c) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    DisplayChessPiece(chessboard[r, c]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            
        }

        void DisplayChessPiece(ChessPiece chessPiece)
        {
            if (!(chessPiece == null))
            {
                if (chessPiece.colour == ChessPieceColour.Black)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                switch (chessPiece.type)
                {
                    case ChessPieceType.Pawn:
                        Console.Write(" p ");
                        break;
                    case ChessPieceType.Rook:
                        Console.Write(" r ");
                        break;
                    case ChessPieceType.Knight:
                        Console.Write(" k ");
                        break;
                    case ChessPieceType.Bishop:
                        Console.Write(" b ");
                        break;
                    case ChessPieceType.Queen:
                        Console.Write(" Q ");
                        break;
                    case ChessPieceType.King:
                        Console.Write(" K ");
                        break;
                }
            }
            else
            {
                Console.Write("   ");
            }
            Console.ResetColor();
        }

        Position ReadPosition(string question)
        {
            Position userPos = new Position();
            bool correct = false;

            do
            {
                Console.Write(question);
                string input = Console.ReadLine().ToUpper();
                try
                {
                    userPos.col = input[0] - 'A';
                    userPos.row = int.Parse(input[1].ToString()) - 1;
                    correct = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message + "\nPlease enter a valid coordinate in the format <LETTER><NUMBER>");
                }
            } while (!correct);
            return userPos;
        }

        void DisplayEliminatedPieces(ChessGame chessGame)
        {
            Console.WriteLine(chessGame.deadWhite);
            Console.WriteLine(chessGame.deadBlack);
        }
    }
}
