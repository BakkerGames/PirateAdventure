// Program.cs - 12/04/2018

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
                if (args[0].Equals("/debug", StringComparison.OrdinalIgnoreCase))
                {
                    debugFullMessages = true;
                }
            }
            try
            {
                RunGame();
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
                RunBackground();
                if (gameOver)
                {
                    break;
                }
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
                Console.WriteLine(); // blank line after entering
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
                if (darkFlag)
                {
                    lightRemaining--;
                }
            }
        }

        private static void ShowIntroduction()
        {
            Console.Write(_introMessage);
            Console.ReadLine();
            Console.WriteLine();
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
            if (debugFullMessages)
            {
                Console.WriteLine($"### Running command {currVerb} {currNoun} {currVerbNumber} {currNounNumber} {_verbNounList[currVerbNumber, 0]} {_verbNounList[currNounNumber, 1]}");
                Console.WriteLine();
            }
#endif
            bool foundMatch = false;
            for (int commandNum = 0; commandNum < _commandCount; commandNum++)
            {
                int verbPart = _commandArray[commandNum, 0] / 150;
                int nounPart = _commandArray[commandNum, 0] % 150;
                if (currVerbNumber == verbPart && currNounNumber == nounPart)
                {
                    foundMatch = true;
#if DEBUG
                    if (debugFullMessages)
                    {
                        Console.WriteLine($"### matches command {commandNum}");
                    }
#endif
                    if (!CheckConditions(commandNum))
                    {
                        continue;
                    }
                    RunActions(commandNum);
                    break; // only run one command
                }
            }
            if (!foundMatch)
            {
                if (currVerbNumber == 1) // go
                {
                    if (currNounNumber >= 1 && currNounNumber <= _exitDirections)
                    {
                        if (darkFlag && _itemLocation[_litTorchItem] != currRoomNumber && _itemLocation[_litTorchItem] != -1)
                        {
                            Console.WriteLine("DANGEROUS TO MOVE IN THE DARK!");
                        }
                        if (_roomExitArray[currRoomNumber, currNounNumber - 1] == 0)
                        {
                            if (darkFlag && _itemLocation[_litTorchItem] != currRoomNumber && _itemLocation[_litTorchItem] != -1)
                            {
                                Console.WriteLine("I FELL DOWN AND BROKE MY NECK.");
                                currRoomNumber = _roomCount - 1; // never-never land
                                darkFlag = false;
                                needToLook = true;
                                foundMatch = true;
                            }
                            else
                            {
                                Console.WriteLine(_cannotGoThatWayMessage);
                                foundMatch = true;
                            }
                        }
                        else
                        {
                            currRoomNumber = _roomExitArray[currRoomNumber, currNounNumber - 1];
                            foundMatch = true;
                            needToLook = true;
                        }
                    }
                }
                else if (currVerbNumber == 10) // take
                {
                    int inventoryCount = 0;
                    int foundItem = -1;
                    for (int itemNum = 0; itemNum < _itemCount; itemNum++)
                    {
                        if (_itemLocation[itemNum] == -1)
                        {
                            inventoryCount++;
                        }
                        else if (_itemLocation[itemNum] == currRoomNumber)
                        {
                            if (_itemDescriptions[itemNum].EndsWith($"/{_verbNounList[currNounNumber, 1]}/"))
                            {
                                foundItem = itemNum;
                            }
                        }
                    }
                    if (foundItem < 0)
                    {
                        Console.WriteLine("I DON'T SEE IT HERE");
                    }
                    else if (inventoryCount >= _maxCarry) // inventory full
                    {
                        Console.WriteLine(_inventoryFullMsg);
                    }
                    else
                    {
                        _itemLocation[foundItem] = -1; // put in inventory
                        Console.WriteLine("TAKEN");
                    }
                    foundMatch = true;
                }
                else if (currVerbNumber == 18) // drop
                {
                    int foundItem = -1;
                    for (int itemNum = 0; itemNum < _itemCount; itemNum++)
                    {
                        if (_itemLocation[itemNum] == -1)
                        {
                            if (_itemDescriptions[itemNum].EndsWith($"/{_verbNounList[currNounNumber, 1]}/"))
                            {
                                foundItem = itemNum;
                            }
                        }
                    }
                    if (foundItem < 0)
                    {
                        Console.WriteLine("YOU AREN'T CARRYING THAT");
                    }
                    else
                    {
                        _itemLocation[foundItem] = currRoomNumber; // put in current room
                        Console.WriteLine("DROPPED");
                    }
                    foundMatch = true;
                }
            }
            return foundMatch;
        }

        private static void RunBackground()
        {
            for (int commandNum = 0; commandNum < _commandCount; commandNum++)
            {
                int verbPart = _commandArray[commandNum, 0] / 150;
                if (verbPart != 0)
                {
                    continue;
                }
                int nounPart = _commandArray[commandNum, 0] % 150;
                int randomPercent = sysRand.Next(100);
                if (randomPercent >= nounPart)
                {
#if DEBUG
                    if (debugFullMessages)
                    {
                        Console.WriteLine($"### skip command   {commandNum} {nounPart} {randomPercent}");
                    }
#endif
                    continue;
                }
#if DEBUG
                if (debugFullMessages)
                {
                    Console.WriteLine($"### random command {commandNum} {nounPart} {randomPercent}");
                }
#endif
                if (!CheckConditions(commandNum))
                {
                    continue;
                }
                RunActions(commandNum);
            }
        }
    }
}
