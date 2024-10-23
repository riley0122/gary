using System;

namespace Gary
{
    internal class Program
    {
        static void Main(string[] programArgs)
        {
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
                        // Create a new ChessBoard and discard the old one
                    break;
                    case "go":
                        // Start calculating
                    break;
                    case "stop":
                        // Actually stop
                    break;
                    case "ponderhit":
                        // TODO
                    break;
                    case "quit":
                        running = false;
                    break;
                }
            }
        }
    }
}