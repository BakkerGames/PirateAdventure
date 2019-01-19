// OutputCommands.cs - 01/19/2019

using System;
using System.IO;
using System.Text;

namespace PirateAdventure
{
    partial class Program
    {
        private static string CommandOutputFilename;

        private static StringBuilder commandCodeText = new StringBuilder();

        public static void OutputCommands()
        {
            Console.Write("Enter filename for storing commands: ");
            CommandOutputFilename = Console.ReadLine();
            if (string.IsNullOrEmpty(CommandOutputFilename))
            {
                Console.WriteLine("Filename not specified!");
                Console.Write("Press enter to continue...");
                Console.ReadLine();
                return;
            }
            // handle a string with quotes, as from shift-right-click "Copy as path"
            if (CommandOutputFilename.StartsWith("\""))
            {
                CommandOutputFilename = CommandOutputFilename.Substring(1);
            }
            if (CommandOutputFilename.EndsWith("\""))
            {
                CommandOutputFilename = CommandOutputFilename.Substring(0, CommandOutputFilename.Length - 1);
            }
            for (int X = 0; X < _commandCount; X++)
            {
                TestingWrite($"{X.ToString("000")}:");
                for (int i = 0; i < _commandValueCount; i++)
                {
                    TestingWrite($" {_commandArray[X, i]}");
                }
                TestingWriteLine();
                TestingWrite("    ");
                for (int i = 0; i < _commandValueCount; i++)
                {
                    if (i < 1 || i > 5)
                    {
                        TestingWrite($" {_commandArray[X, i] / 150}/{_commandArray[X, i] % 150}");
                    }
                    else
                    {
                        TestingWrite($" {_commandArray[X, i] / 20}/{_commandArray[X, i] % 20}");
                    }
                }
                TestingWriteLine();
                int verb = _commandArray[X, 0] / 150;
                int noun = _commandArray[X, 0] % 150;
                if (verb == 0)
                {
                    TestingWriteLine($"{noun}%");
                }
                else if (noun == 0)
                {
                    TestingWriteLine($"{_verbNounList[verb, 0]}");
                }
                else
                {
                    if (verb == 41 && noun == 20)
                    {
                        TestingWriteLine("UNLIGHT TORCH"); // unlock and unlight are both the verb "UNL", need special handling
                    }
                    else
                    {
                        TestingWriteLine($"{_verbNounList[verb, 0]} {_verbNounList[noun, 1]}");
                    }
                }
                for (int w = 1; w <= 5; w++)
                {
                    int ll = _commandArray[X, w] / 20;
                    int k = _commandArray[X, w] % 20;
                    switch (k)
                    {
                        case 0:
                            // nothing
                            break;
                        case 1:
                            TestingWriteLine($"    if {ItemDesc(ll)} carried");
                            break;
                        case 2:
                            TestingWriteLine($"    if {ItemDesc(ll)} in room");
                            break;
                        case 3:
                            TestingWriteLine($"    if {ItemDesc(ll)} carried or in room");
                            break;
                        case 4:
                            TestingWriteLine($"    if room = {RoomDesc(ll)}");
                            break;
                        case 5:
                            TestingWriteLine($"    if {ItemDesc(ll)} not in room");
                            break;
                        case 6:
                            TestingWriteLine($"    if {ItemDesc(ll)} not carried");
                            break;
                        case 7:
                            TestingWriteLine($"    if room not {RoomDesc(ll)}");
                            break;
                        case 8:
                            TestingWriteLine($"    if flag {ll} true");
                            break;
                        case 9:
                            TestingWriteLine($"    if flag {ll} false");
                            break;
                        case 10:
                            TestingWriteLine($"    if carrying anything");
                            break;
                        case 11:
                            TestingWriteLine($"    if carrying nothing");
                            break;
                        case 12:
                            TestingWriteLine($"    if {ItemDesc(ll)} not carried or in room");
                            break;
                        case 13:
                            TestingWriteLine($"    if {ItemDesc(ll)} somewhere");
                            break;
                        case 14:
                            TestingWriteLine($"    if {ItemDesc(ll)} nowhere");
                            break;
                        default:
                            TestingWriteLine($"    #UNUSED# {k} {ll}");
                            break;
                    }
                }
                int IP = 0;
                for (int y = 6; y <= 7; y++)
                {
                    int y1 = _commandArray[X, y] / 150;
                    int y2 = _commandArray[X, y] % 150;
                    TestDoAction(y1, X, ref IP);
                    TestDoAction(y2, X, ref IP);
                }
                TestingWriteLine();
            }
            try
            {
                File.WriteAllText(CommandOutputFilename, commandCodeText.ToString());
                Console.WriteLine($"Commands written to {CommandOutputFilename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while writing file: {ex.Message}");
            }
            Console.Write("Press enter to continue...");
            Console.ReadLine();
        }

        private static void TestingWriteLine()
        {
            TestingWrite("\r\n");
        }

        private static void TestingWriteLine(string value)
        {
            TestingWrite(value);
            TestingWrite("\r\n");
        }

        private static void TestingWrite(string value)
        {
            commandCodeText.Append(value);
            // Console.Write(value);
        }

        private static void TestDoAction(int value, int X, ref int IP)
        {
            if (value == 0)
            {
                return;
            }
            if (value > 101)
            {
                TestingWriteLine($"    > {_messages[value - 50]}");
                return;
            }
            if (value < 52)
            {
                TestingWriteLine($"    > {_messages[value]}");
                return;
            }
            int P = 0;
            int P1 = 0;
            switch (value - 51)
            {
                case 0:
                    // do nothing
                    break;
                case 1: // take
                    P = TestGetDataValue(X, ref IP);
                    TestingWriteLine($"    : take {ItemDesc(P)}");
                    break;
                case 2: // drop
                    P = TestGetDataValue(X, ref IP);
                    TestingWriteLine($"    : drop {ItemDesc(P)}");
                    break;
                case 3: // teleport
                    P = TestGetDataValue(X, ref IP);
                    TestingWriteLine($"    : teleport to {RoomDesc(P)}");
                    break;
                case 4: // item to nowhere
                    P = TestGetDataValue(X, ref IP);
                    TestingWriteLine($"    : send {ItemDesc(P)} to nowhere");
                    break;
                case 5: // turn on dark
                    TestingWriteLine($"    : turn on dark");
                    break;
                case 6: // turn off dark
                    TestingWriteLine($"    : turn off dark");
                    break;
                case 7: // turn on flag
                    P = TestGetDataValue(X, ref IP);
                    TestingWriteLine($"    : set flag {P} true");
                    break;
                case 8: // item to nowhere
                    P = TestGetDataValue(X, ref IP);
                    TestingWriteLine($"    : send {ItemDesc(P)} to nowhere");
                    break;
                case 9: // turn off flag
                    P = TestGetDataValue(X, ref IP);
                    TestingWriteLine($"    : set flag {P} false");
                    break;
                case 10: // dead
                    TestingWriteLine("    : dead");
                    break;
                case 11: // item goes to room
                    P = TestGetDataValue(X, ref IP);
                    P1 = TestGetDataValue(X, ref IP);
                    TestingWriteLine($"    : {ItemDesc(P)} goes to {RoomDesc(P1)}");
                    break;
                case 12: // game over
                    TestingWriteLine("    : game over");
                    break;
                case 13: // look
                    TestingWriteLine("    : look");
                    break;
                case 14: // check treasures
                    TestingWriteLine("    : check treasures");
                    break;
                case 15: // show inventory
                    TestingWriteLine("    : show inventory");
                    break;
                case 16: // flag 0 true
                    TestingWriteLine("    : set flag 0 true");
                    break;
                case 17: // flag 0 false
                    TestingWriteLine("    : set flag 0 false");
                    break;
                case 18: // torch recharged
                    TestingWriteLine("    : torch recharged");
                    break;
                case 19: // clear screen
                    TestingWriteLine("    : clear screen");
                    break;
                case 20: // save game
                    TestingWriteLine("    : save game");
                    break;
                case 21: // swap two items
                    P = TestGetDataValue(X, ref IP);
                    P1 = TestGetDataValue(X, ref IP);
                    TestingWriteLine($"    : swap items {ItemDesc(P)} and {ItemDesc(P1)}");
                    break;
                default:
                    TestingWriteLine($"    : #{value - 51}#");
                    break;
            }
        }

        private static int TestGetDataValue(int X, ref int IP)
        {
            int P = 0;
            int W = 0;
            int M = 0;
            do
            {
                IP++;
                W = _commandArray[X, IP];
                P = W / 20;
                M = W % 20;
            } while (M != 0);
            return P;
        }

        public static string ItemDesc(int value)
        {
            string result = _itemDescriptions[value];
            if (result.EndsWith("/"))
            {
                result = result.Substring(0, result.IndexOf("/"));
            }
            result = result.Replace(" ", "_").Replace("*", "");
            // add {value} to items with same descriptions
            switch (value)
            {
                case 25: // bottle of rum
                case 49: // bottle of rum
                    result += $"_{value}";
                    break;
            }
            return result;
        }

        public static string RoomDesc(int value)
        {
            string result = _roomLongDesc[value];
            if (result.EndsWith("/"))
            {
                result = result.Substring(result.IndexOf("/") + 1);
                result = result.Substring(0, result.Length - 1);
            }
            return result.Replace(" ", "_");
        }
    }
}
