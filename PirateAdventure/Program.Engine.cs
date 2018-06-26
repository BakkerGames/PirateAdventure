// Program.Engine.cs - 06/25/2018

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
                Console.Write($"### check condition {commandNum} {indexNum} {conditionNum} {conditionData}"); // todo
#endif
                result = true;
                switch (conditionNum)
                {
                    case 0: // nothing
                        result = true;
                        break;
                    case 1: // item carried
                        result = (_itemLocation[conditionData] == -1);
                        break;
                    case 2: // item in room
                        result = (_itemLocation[conditionData] == currRoomNumber);
                        break;
                    case 3: // item carried or in room
                        result = (_itemLocation[conditionData] == currRoomNumber || _itemLocation[conditionData] == -1);
                        break;
                    case 4: // room matches
                        result = (currRoomNumber == conditionData);
                        break;
                    case 5: // item not in room
                        result = (_itemLocation[conditionData] != currRoomNumber);
                        break;
                    case 6: // item not carried
                        result = (_itemLocation[conditionData] != -1);
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
                        for (int i = 0; i <= _itemCount; i++)
                        {
                            if (_itemLocation[conditionData] == -1)
                            {
                                result = true;
                                break;
                            }
                        }
                        break;
                    case 11: // inventory is empty
                        result = true;
                        for (int i = 0; i <= _itemCount; i++)
                        {
                            if (_itemLocation[conditionData] == -1)
                            {
                                result = false;
                                break;
                            }
                        }
                        break;
                    case 12: // item not carried and not in room
                        result = (_itemLocation[conditionData] != -1 && _itemLocation[conditionData] != currRoomNumber);
                        break;
                    case 13: // item is somewhere
                        result = (_itemLocation[conditionData] != 0);
                        break;
                    case 14: // item is nowhere
                        result = (_itemLocation[conditionData] == 0);
                        break;
                    default:
                        Console.WriteLine($"#ERROR# Unknown condition: {conditionNum} {conditionData}");
                        break;
                }
#if DEBUG
                Console.WriteLine($" - {result}"); // todo
#endif
                if (!result)
                {
                    break;
                }
            }
#if DEBUG
            Console.WriteLine($"### overall result {result}"); // todo
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
            if (actionNum > 101)
            {
                Console.WriteLine(_messages[actionNum - 50]);
                return;
            }
            if (actionNum == 0)
            {
                return;
            }
            if (actionNum < 52)
            {
                Console.WriteLine(_messages[actionNum]);
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
                    for (int itemNum = 0; itemNum <= _itemCount; itemNum++)
                    {
                        if (_itemLocation[itemNum] == -1)
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
                    _itemLocation[dataValue] = -1;
                    break;
                case 2:
                    // drop to current room //2=700
                    // 700 {GOSUB}1050
                    //     :_itemLocation(P)=R
                    //     :{GOTO}960
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
                    _itemLocation[dataValue] = 0;
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
                    _itemLocation[dataValue] = 0;
                    break;
                case 9:
                    // turn off flag
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    _systemFlags[dataValue] = false;
                    break;
                case 10:
                    // dead
                    Console.WriteLine("I'M DEAD...");
                    currRoomNumber = _roomCount;
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
                    for (int Z = 0; Z <= _itemCount; Z++)
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
                    // show inventory //15=890
                    // 890 {PRINT}"I'M CARRYING:"
                    //     :K$="NOTHING"
                    //     :{FOR}Z=0{TO}IL
                    //     :{IF}_itemLocation(Z)<>-1{THEN}910{ELSE}{GOSUB}280
                    //     :{IF}{LEN}(TP$)+POS(0)>63{PRINT}
                    // 900 {PRINT}TP$;".",;
                    //     :K$=""
                    // 910 {NEXT}
                    //     :{PRINT}K$
                    //     :{GOTO}960
                    // 280 TP$=_itemLocation$(Z)
                    //     :{IF}{RIGHT$}(TP$,1)="/"{FOR}W={LEN}(TP$)-1{TO}1{STEP}-1
                    //     :{IF}{MID$}(TP$,W,1)="/"{THEN}TP$={LEFT$}(TP$,W-1){ELSE}{NEXT}W
                    // 290 {RETURN}
                    ShowInventory();
                    break;
                case 16:
                    // flag 0 true //16=920
                    // 920 P=0
                    //     :{GOTO}800
                    // 800 SF=SF {OR}{CINT}(.5+2^P)
                    //     :{GOTO}960
                    _systemFlags[0] = true;
                    break;
                case 17:
                    // flag 0 false //17=930
                    // 930 P=0
                    //     :{GOTO}820
                    // 820 SF=SF{AND}{NOT}{CINT}(.5+2^P)
                    //     :{GOTO}960
                    _systemFlags[0] = false;
                    break;
                case 18:
                    //18=940
                    break;
                case 19:
                    // clear screen //19=950
                    Console.WriteLine();
                    break;
                case 20:
                    // save game //20=710
                    Console.WriteLine("    : save game");
                    break;
                case 21:
                    // swap two items //21=750
                    // 750 {GOSUB}1050
                    //     :L=P
                    //     :{GOSUB}1050
                    //     :Z=_itemLocation(P)
                    //     :_itemLocation(P)=_itemLocation(L)
                    //     :_itemLocation(L)=Z
                    //     :{GOTO}960
                    dataValue = GetDataValue(commandNum, ref dataPointer);
                    dataValue2 = GetDataValue(commandNum, ref dataPointer);
                    int P2 = _itemLocation[dataValue];
                    _itemLocation[dataValue] = _itemLocation[dataValue2];
                    _itemLocation[dataValue2] = P2;
                    break;
                default:
                    Console.WriteLine($"#ERROR# Unknown action: {actionNum - 51}");
                    break;
            }
            // 960 {NEXT}Y

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
