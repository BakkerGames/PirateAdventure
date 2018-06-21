// Program.cs - 06/21/2018

using System;

namespace PirateAdventure
{
    partial class Program
    {
        static void Main(string[] args)
        {
            TestData();
            return;
            try
            {
                RunGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
            string s = (numMoves == 1) ? "" : "S";
            Console.WriteLine($"YOU FINISHED IN {numMoves} MOVE{s}.");
            Console.WriteLine();
            Console.Write("THANKS FOR PLAYING!!! PRESS ENTER...");
            Console.ReadLine();
        }

        private static void RunGame()
        {
            Initialize();
            ShowIntroduction();
            while (!gameOver)
            {
                GetCommand();
                if (!ParseCommand())
                {
                    Console.WriteLine("I DON'T UNDERSTAND THAT!");
                    continue;
                }
                RunCommand();
                numMoves++;
                if (!gameOver)
                {
                    RunBackground();
                }
            }
        }

        private static void GetCommand()
        {
            Console.WriteLine();
            Console.Write("ENTER COMMAND> ");
            currCommandLine = Console.ReadLine().Trim().ToUpper();
        }

        private static bool ParseCommand()
        {
            if (string.IsNullOrWhiteSpace(currCommandLine))
            {
                return false;
            }
            if (currCommandLine.Contains(" "))
            {
                int pos = currCommandLine.IndexOf(" ");
                currVerb = currCommandLine.Substring(0, pos).Trim();
                currNoun = currCommandLine.Substring(pos).Trim();
                if (currNoun.Contains(" "))
                {
                    return false;
                }
            }
            else
            {
                currVerb = currCommandLine;
                currNoun = "";
            }
            return true;
        }

        private static void RunCommand()
        {
            Console.WriteLine($"### Running command {currVerb} {currNoun}"); // todo
            if (currVerb == "QUIT")
            {
                gameOver = true;
                return;
            }
        }

        private static void RunBackground()
        {
            //throw new NotImplementedException();
        }

        private static void Initialize()
        {
            gameOver = false;
            numMoves = 0;
        }

        private static void ShowIntroduction()
        {
            Console.Write(_introMessage);
            Console.ReadLine();
        }
    }
}
