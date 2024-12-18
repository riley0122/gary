﻿namespace Gary
{
    internal class Program
    {
        static void Main(string[] programArgs)
        {
            ChessBoard internal_board = new ChessBoard();
            bool running = true;
            while (running)
            {
                string input = Console.ReadLine();
                string[] args = input.Split(" ");
                string command = args[0];

                switch (command)
                {
                    case "uci":
                        Console.Write("id name Gary\n");
                        Console.Write("id author Riley0122\n");
                        Console.Write("uciok\n");
                    break;
                    case "isready":
                        // TODO: Check if actually is ready
                        Console.Write("readyok\n");
                    break;
                    case "ucinewgame":
                        // reset
                    break;
                    case "position":
                        internal_board = new ChessBoard();
                        string fen = args[1];
                        if (args[1] == "startpos") {
                            fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
                        }
                        internal_board.LoadFromFEN(fen);
                        if (args.Length >= 3 && args[2] == "moves") {
                            string[] moves = args[3..args.Length];
                            foreach (string move in moves)
                            {
                                internal_board.makeMove(move);
                            }
                        }
                    break;
                    case "go":
                        // Console.Write("info score cp " + internal_board.getScore() + "\n");
                        string bestmove = getBestMove(internal_board);
                        Console.Write("bestmove " + bestmove + "\n");
                    break;
                    case "stop":
                        // Actually stop
                    break;
                    case "ponderhit":
                        // TODO
                    break;
                    case "debug":
                        switch (args[1]) {
                            case "showboard":
                                Console.Write(internal_board.ToString());
                            break;
                            case "movevalid":
                                Square fromSquare = new Square(args[2][0], args[2][1] - '0', internal_board);
                                Square toSquare = new Square(args[2][2], args[2][3] - '0', internal_board);
                                IPiece piece = internal_board.GetPieceAt(fromSquare);
                                if(piece is null) {
                                    Console.Write("N | No piece at " + fromSquare.ToString() + "\n");
                                } else if (piece.IsMoveValid(toSquare)) {
                                    Console.Write("Y | Move is valid\n");
                                } else {
                                    Console.Write("N | Move is not valid\n");
                                }
                            break;
                            case "getvalidmoves":
                                Square sqr = new Square(args[2][0], args[2][1] - '0', internal_board);
                                IPiece piece_ = internal_board.GetPieceAt(sqr);
                                if(piece_ is null) {
                                    Console.Write("No piece at " + sqr.ToString() + "\n");
                                } else {
                                    Square[] legalMoves = piece_.GetValidMoves();
                                    Console.Write("Found " + legalMoves.Length + " legal moves\n");
                                    foreach (Square move in legalMoves) {
                                        if (piece_.IsMoveValid(move)) {
                                            Console.Write(move.ToString() + ", ");
                                        } else {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write(move.ToString() + "!");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write(" , ");
                                        }
                                    }
                                }
                            break;
                            case "help":
                                Console.Write("debug showboard\t\t-\tShows the internal chess board\n");
                                Console.Write("debug movevalid <move>\t\t-\tCheck if a move is valid\n");
                                Console.Write("debug getvalidmoves <square>\t\t-\tGet all valid moves of a piece\n");
                                Console.Write("debug help\t\t-\tThis command\n");
                            break;
                            default:
                                Console.Write("Invalid debug command!\n");
                                Console.Write("Run 'debug help' to see a list of commands.\n");
                            break;
                        }
                    break;
                    case "quit":
                        running = false;
                    break;
                }
            }
        }

        private static string getBestMove(ChessBoard internal_board)
        {
            // TODO: Actually get the best move
            // For now get a random move
            List<(string move, int score)> all_moves = new List<(string move, int score)>();
            foreach((IPiece piece, Square square) in internal_board.GetAllLegalMoves()) {
                string promoteTo = "";
                if((square.rank == 8 || square.rank == 1) && piece.GetPieceSymbol().ToLower() == "p") promoteTo = "q";
                string moveString = piece.CurrentPosition.ToString() + square.ToString() + promoteTo;

                ChessBoard hypotheticalBoard = internal_board.ImageineMove(moveString);
                int score = hypotheticalBoard.getScore();

                all_moves.Add((moveString, score));
            }
            
            var bestmove = all_moves.OrderBy(m => m.score).FirstOrDefault();
            if (internal_board.whiteToMove) {
                bestmove = all_moves.OrderByDescending(m => m.score).FirstOrDefault();
            }
            
            return bestmove.move;
        }
    }
}