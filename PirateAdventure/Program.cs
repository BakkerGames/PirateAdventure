// Program.cs - 06/22/2018

using System;

namespace PirateAdventure
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].Equals("/test", StringComparison.OrdinalIgnoreCase))
                {
                    TestData();
                    return;
                }
            }
            try
            {
                RunGame();
                Console.WriteLine();
                Console.WriteLine(_numberOfMovesMessage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
            Console.Write(_endingMessage);
            Console.ReadLine();
        }

        private static void RunGame()
        {
            Initialize();
            ShowIntroduction();
            while (!gameOver)
            {
                if (needToLook)
                {
                    Look();
                    needToLook = false;
                }
                do
                {
                    GetCommand();
                }
                while (string.IsNullOrWhiteSpace(currCommandLine));
                if (!ParseCommand())
                {
                    Console.WriteLine(_cannotParseMessage);
                    continue;
                }
                if (!RunCommand())
                {
                    Console.WriteLine(_cannotDoMessage);
                    continue;
                }
                if (!countsAsMove)
                {
                    continue;
                }
                numMoves++;
                if (gameOver)
                {
                    break;
                }
                RunBackground();
            }
        }

        private static void ShowIntroduction()
        {
            Console.Write(_introMessage);
            Console.ReadLine();
        }

        private static void GetCommand()
        {
            Console.WriteLine();
            Console.Write(_enterCommand);
            currCommandLine = Console.ReadLine().Trim().ToUpper();
        }

        private static bool RunCommand()
        {
            countsAsMove = true;
#if DEBUG
            Console.WriteLine($"### Running command {currVerb} {currNoun} {currVerbNumber} {currNounNumber} {_verbNounList[currVerbNumber, 0]} {_verbNounList[currNounNumber, 1]}"); // todo
            Console.WriteLine();
            // todo ### handle quit in code, not here
            if (currVerb == "QUI")
            {
                gameOver = true;
                return true;
            }
#endif
            bool foundMatch = false;
            for (int i = 0; i < _commandCount; i++)
            {
                int verbPart = _commandArray[i, 0] / 150;
                int nounPart = _commandArray[i, 0] % 150;
                if (currVerbNumber == verbPart && currNounNumber == nounPart)
                {
                    foundMatch = true;
#if DEBUG
                    Console.WriteLine($"### matches command {i}"); // todo
#endif
                }
            }
            if (!foundMatch)
            {
                if (currVerbNumber == 1) // go
                {
                    if (currNounNumber >= 1 && currNounNumber <= _exitDirections)
                    {
                        if (_roomExitArray[currRoomNumber, currNounNumber] == 0)
                        {
                            // todo ### check for darkness
                            Console.WriteLine(_cannotGoThatWayMessage);
                            foundMatch = true;
                        }
                        else
                        {
                            currNounNumber = _roomExitArray[currRoomNumber, currNounNumber];
                            foundMatch = true;
                            needToLook = true;
                        }
                    }
                }
                else if (currVerbNumber == 10) // take
                {
                    Console.WriteLine("### take ###");
                    foundMatch = true;
                }
                else if (currVerbNumber == 18) // drop
                {
                    Console.WriteLine("### drop ###");
                    foundMatch = true;
                }
            }
#if DEBUG
            if (foundMatch) { Console.WriteLine(); }
#endif
            return foundMatch;
        }

        private static void RunBackground()
        {
            for (int i = 0; i < _commandCount; i++)
            {
                int verbPart = _commandArray[i, 0] / 150;
                if (verbPart != 0)
                {
                    continue;
                }
                int nounPart = _commandArray[i, 0] % 150;
                int randomPercent = sysRand.Next(100);
                if (randomPercent >= nounPart)
                {
#if DEBUG
                    Console.WriteLine($"### skip command   {i} {nounPart} {randomPercent}"); // todo
#endif
                    continue;
                }
#if DEBUG
                Console.WriteLine($"### random command {i} {nounPart} {randomPercent}"); // todo
#endif
            }
        }
    }
}
