// Program.cs - 01/19/2019

using System;

namespace PirateAdventure
{
    public partial class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].Equals("/commands", StringComparison.OrdinalIgnoreCase))
                {
                    OutputCommands();
                    return 0;
                }
#if DEBUG
                if (args[0].Equals("/debug", StringComparison.OrdinalIgnoreCase))
                {
                    debugFullMessages = true;
                }
#endif
                if (args[0].Equals("/script", StringComparison.OrdinalIgnoreCase))
                {
                    FillScriptLines();
                    runScript = true;
                }
            }
            try
            {
                if (!LoadGameData())
                {
                    Initialize();
                    ShowIntroduction();
                }
                RunGame();
                if (!gameSaved)
                {
                    Console.WriteLine(_numberOfMovesMessage());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return 1;
            }
            Console.WriteLine();
            Console.Write(_endingMessage);
            Console.ReadLine();
            return 0;
        }

        private static void RunGame()
        {
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
                // check if torch is burning out
                if (_itemLocation[_litTorchItem] != _itemNowhere)
                {
                    lightRemaining--;
                    if (lightRemaining <= 0)
                    {
                        Console.WriteLine("LIGHT HAS RUN OUT");
                        _itemLocation[_litTorchItem] = _itemNowhere; // torch to nowhere
                    }
                    else if (lightRemaining < 25)
                    {
                        Console.WriteLine($"LIGHT RUNS OUT IN {lightRemaining} TURNS!");
                    }
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
            if (runScript && !scriptBreakFlag && scriptLineNum < scriptLines.Length)
            {
                currCommandLine = scriptLines[scriptLineNum].Trim().ToUpper();
                Console.WriteLine(currCommandLine);
                scriptLineNum++;
                if (currCommandLine.StartsWith("BREAK"))
                {
                    scriptBreakFlag = true;
                    Console.WriteLine();
                    Console.WriteLine("ENTER 'CONTINUE' TO RESUME SCRIPT");
                    currCommandLine = ""; // get another command
                }
            }
            else
            {
                currCommandLine = Console.ReadLine().Trim().ToUpper();
                if (currCommandLine.Equals("CONTINUE"))
                {
                    scriptBreakFlag = false;
                    currCommandLine = ""; // get another command
                }
            }
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
            bool canRunActions = false;
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
                    canRunActions = true;
                    RunActions(commandNum);
                    break; // only run one command
                }
            }
            if (!foundMatch || !canRunActions)
            {
                if (currVerbNumber == 1) // go
                {
                    if (currNounNumber >= 1 && currNounNumber <= _exitDirections)
                    {
                        if (darkFlag && _itemLocation[_litTorchItem] != currRoomNumber && _itemLocation[_litTorchItem] != _itemInventory)
                        {
                            Console.WriteLine("DANGEROUS TO MOVE IN THE DARK!");
                        }
                        if (_roomExitArray[currRoomNumber, currNounNumber - 1] == 0)
                        {
                            if (darkFlag && _itemLocation[_litTorchItem] != currRoomNumber && _itemLocation[_litTorchItem] != _itemInventory)
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
                    int foundItem = _itemInventory;
                    for (int itemNum = 0; itemNum < _itemCount; itemNum++)
                    {
                        if (_itemLocation[itemNum] == _itemInventory)
                        {
                            inventoryCount++;
                        }
                        else if (_itemLocation[itemNum] == currRoomNumber)
                        {
                            string itemName = _verbNounList[currNounNumber, 1];
                            if (itemName.StartsWith("*"))
                            {
                                itemName = itemName.Substring(1);
                            }
                            if (itemName.Length > _wordSize) // trim longer nouns
                            {
                                itemName = itemName.Substring(0, _wordSize);
                            }
                            if (_itemDescriptions[itemNum].EndsWith($"/{itemName}/"))
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
                        _itemLocation[foundItem] = _itemInventory; // put in inventory
                        Console.WriteLine("TAKEN");
                    }
                    foundMatch = true;
                }
                else if (currVerbNumber == 18) // drop
                {
                    int foundItem = _itemInventory;
                    for (int itemNum = 0; itemNum < _itemCount; itemNum++)
                    {
                        if (_itemLocation[itemNum] == _itemInventory)
                        {
                            string itemName = _verbNounList[currNounNumber, 1];
                            if (itemName.StartsWith("*"))
                            {
                                itemName = itemName.Substring(1);
                            }
                            if (itemName.Length > _wordSize) // trim longer nouns
                            {
                                itemName = itemName.Substring(0, _wordSize);
                            }
                            if (_itemDescriptions[itemNum].EndsWith($"/{itemName}/"))
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
