// Program.Engine.cs - 01/08/2019

using System;

namespace PirateAdventure
{
    public partial class Program
    {
        private static bool CheckConditions(int commandNum)
        {
            bool result = false;
            for (int indexNum = 1; indexNum <= 5; indexNum++)
            {
                int tempValue = _commandArray[commandNum, indexNum];
                int conditionData = tempValue / 20;
                int conditionNum = tempValue - (conditionData * 20);
#if DEBUG
                if (debugFullMessages)
                {
                    if (conditionNum != 0)
                    {
                        Console.Write($"### check condition {commandNum} {indexNum} {conditionNum} {conditionData}");
                    }
                }
#endif
                result = true;
                switch (conditionNum)
                {
                    case 0: // nothing
                        result = true;
                        break;
                    case 1: // item carried
                        result = (_itemLocation[conditionData] == _itemInventory);
                        break;
                    case 2: // item in room
                        result = (_itemLocation[conditionData] == currRoomNumber);
                        break;
                    case 3: // item carried or in room
                        result = (_itemLocation[conditionData] == currRoomNumber || _itemLocation[conditionData] == _itemInventory);
                        break;
                    case 4: // room matches
                        result = (currRoomNumber == conditionData);
                        break;
                    case 5: // item not in room
                        result = (_itemLocation[conditionData] != currRoomNumber);
                        break;
                    case 6: // item not carried
                        result = (_itemLocation[conditionData] != _itemInventory);
                        break;
                    case 7: // room doesn't match
                        result = (currRoomNumber != conditionData);
                        break;
                    case 8: // flag is true
                        result = (_systemFlags[conditionData]);
                        break;
                    case 9: // flag is false
                        result = (!_systemFlags[conditionData]);
                        break;
                    case 10: // inventory not empty
                        result = false;
                        for (int i = 0; i < _itemCount; i++)
                        {
                            if (_itemLocation[conditionData] == _itemInventory)
                            {
                                result = true;
                                break;
                            }
                        }
                        break;
                    case 11: // inventory is empty
                        result = true;
                        for (int i = 0; i < _itemCount; i++)
                        {
                            if (_itemLocation[conditionData] == _itemInventory)
                            {
                                result = false;
                                break;
                            }
                        }
                        break;
                    case 12: // item not carried and not in room
                        result = (_itemLocation[conditionData] != _itemInventory && _itemLocation[conditionData] != currRoomNumber);
                        break;
                    case 13: // item is somewhere
                        result = (_itemLocation[conditionData] != _itemNowhere);
                        break;
                    case 14: // item is nowhere
                        result = (_itemLocation[conditionData] == _itemNowhere);
                        break;
                    default:
                        Console.WriteLine($"#ERROR# Unknown condition: {conditionNum} {conditionData}");
                        break;
                }
#if DEBUG
                if (debugFullMessages)
                {
                    if (conditionNum != 0)
                    {
                        Console.WriteLine($" - {result}");
                    }
                }
#endif
                if (!result)
                {
                    break;
                }
            }
#if DEBUG
            if (debugFullMessages)
            {
                Console.WriteLine($"### overall result {result}");
            }
#endif
            return result;
        }

        private static void RunActions(int commandNum)
        {
            // run actions
            int dataPointer = 0;
            for (int indexNum = 6; indexNum <= 7; indexNum++)
            {
                int actionValue = _commandArray[commandNum, indexNum];
                int action1 = actionValue / 150;
                int action2 = actionValue % 150;
                RunOneAction(action1, commandNum, ref dataPointer);
                if (gameOver)
                {
                    return;
                }
                RunOneAction(action2, commandNum, ref dataPointer);
                if (gameOver)
                {
                    return;
                }
            }
        }

        private static void RunOneAction(int actionNum, int commandNum, ref int dataPointer)
        {
            if (actionNum == 0)
            {
                return;
            }
            if (actionNum < 52)
            {
                Console.WriteLine(_messages[actionNum]);
                return;
            }
            if (actionNum > 101)
            {
                Console.WriteLine(_messages[actionNum - 50]);
                return;
            }
            int dataValue = 0;
            int dataValue2 = 0;
            switch (actionNum - 51)
            {
                case 0:
                    // do nothing
                    break;
                case 1:
                    // take
                    int inventoryCount = 0;
                    for (int itemNum = 0; itemNum < _itemCount; itemNum++)
                    {
                        if (_itemLocation[itemNum] == _itemInventory)
                        {
                            inventoryCount++;
                        }
                    }
                    if (inventoryCount >= _maxCarry) // inventory full
                    {
                        Console.WriteLine(_inventoryFullMsg);
                        return;
                    }
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    _itemLocation[dataValue] = _itemInventory;
                    break;
                case 2:
                    // drop to current room
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    _itemLocation[dataValue] = currRoomNumber;
                    break;
                case 3:
                    // teleport
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    currRoomNumber = dataValue;
                    break;
                case 4:
                    // item to nowhere
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    _itemLocation[dataValue] = _itemNowhere;
                    break;
                case 5:
                    // turn on dark flag
                    darkFlag = true;
                    break;
                case 6:
                    // turn off dark flag
                    darkFlag = false;
                    break;
                case 7:
                    // turn on flag
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    _systemFlags[dataValue] = true;
                    break;
                case 8:
                    // item to nowhere
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    _itemLocation[dataValue] = _itemNowhere;
                    break;
                case 9:
                    // turn off flag
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    _systemFlags[dataValue] = false;
                    break;
                case 10:
                    // dead
                    Console.WriteLine("I'M DEAD...");
                    currRoomNumber = _roomCount - 1; // never-never land
                    darkFlag = false;
                    Look();
                    break;
                case 11:
                    // item goes to room
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    dataValue2 = GetDataValue(commandNum, ref dataPointer);
                    _itemLocation[dataValue] = dataValue2;
                    break;
                case 12:
                    // game over
                    gameOver = true;
                    break;
                case 13:
                    // look
                    Look();
                    break;
                case 14:
                    // check treasures
                    int treasureCount = 0;
                    for (int Z = 0; Z < _itemCount; Z++)
                    {
                        if (_itemLocation[Z] == _treasureRoom && _itemDescriptions[Z].StartsWith("*"))
                        {
                            treasureCount++;
                        }
                    }
                    Console.WriteLine($"I'VE STORED {treasureCount} TREASURES.");
                    Console.WriteLine($"ON A SCALE OF 0 TO 100 THAT RATES A {(treasureCount * 100) / _totalTreasures}");
                    if (treasureCount < _totalTreasures)
                    {
                        break;
                    }
                    Console.WriteLine("WELL DONE.");
                    gameOver = true;
                    break;
                case 15:
                    // show inventory
                    ShowInventory();
                    break;
                case 16:
                    // flag 0 true
                    _systemFlags[0] = true;
                    break;
                case 17:
                    // flag 0 false
                    _systemFlags[0] = false;
                    break;
                case 18:
                    // torch recharged
                    // ### never used ###
                    lightRemaining = _lightTotal;
                    _itemLocation[_litTorchItem] = _itemInventory;
                    break;
                case 19:
                    // clear screen
                    // ### do nothing for now ###
                    break;
                case 20:
                    // save game
                    SaveGameData();
                    Console.WriteLine("SAVED");
                    gameOver = true;
                    gameSaved = true;
                    break;
                case 21:
                    // swap two items
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    dataValue2 = GetDataValue(commandNum, ref dataPointer);
                    int tempLoc = _itemLocation[dataValue];
                    _itemLocation[dataValue] = _itemLocation[dataValue2];
                    _itemLocation[dataValue2] = tempLoc;
                    break;
                default:
                    Console.WriteLine($"#ERROR# Unknown action: {actionNum - 51}");
                    break;
            }
        }

        private static int GetDataValue(int commandNum, ref int dataPointer)
        {
            int dataValue;
            int dataWord;
            int remainderValue;
            do
            {
                dataPointer++;
                dataWord = _commandArray[commandNum, dataPointer];
                dataValue = dataWord / 20;
                remainderValue = dataWord % 20;
            } while (remainderValue != 0);
            return dataValue;
        }
    }
}
