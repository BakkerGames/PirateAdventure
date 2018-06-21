// Testing.cs - 06/21/2018

using System;

namespace PirateAdventure
{
    partial class Program
    {
        public static void TestData()
        {
            for (int X = 0; X < _commandCount; X++)
            {
                Console.WriteLine($"{X}: {_commandArray[X, 0]}");
                int verb = _commandArray[X, 0] / 150;
                int noun = _commandArray[X, 0] % 150;
                if (verb == 0)
                {
                    Console.WriteLine($"{noun}%");
                }
                else if (noun == 0)
                {
                    Console.WriteLine($"{_nounVerbList[verb, 0]}");
                }
                else
                {
                    Console.WriteLine($"{_nounVerbList[verb, 0]} {_nounVerbList[noun, 1]}");
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
                            Console.WriteLine($"    if {ItemDesc(ll)} carried");
                            break;
                        case 2:
                            Console.WriteLine($"    if {ItemDesc(ll)} in room");
                            break;
                        case 3:
                            Console.WriteLine($"    if {ItemDesc(ll)} carried or in room");
                            break;
                        case 4:
                            Console.WriteLine($"    if room = {RoomDesc(ll)}");
                            break;
                        case 5:
                            Console.WriteLine($"    if {ItemDesc(ll)} not in room");
                            break;
                        case 6:
                            Console.WriteLine($"    if {ItemDesc(ll)} not carried");
                            break;
                        case 7:
                            Console.WriteLine($"    if room not {RoomDesc(ll)}");
                            break;
                        case 8:
                            Console.WriteLine($"    if flag {ll} true");
                            break;
                        case 9:
                            Console.WriteLine($"    if flag {ll} false");
                            break;
                        case 10:
                            Console.WriteLine($"    if carrying anything");
                            break;
                        case 11:
                            Console.WriteLine($"    if carrying nothing");
                            break;
                        case 12:
                            Console.WriteLine($"    if {ItemDesc(ll)} not carried or in room");
                            break;
                        case 13:
                            Console.WriteLine($"    if {ItemDesc(ll)} somewhere");
                            break;
                        case 14:
                            Console.WriteLine($"    if {ItemDesc(ll)} nowhere");
                            break;
                        default:
                            Console.WriteLine($"    #UNUSED# {k} {ll}");
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
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        private static void TestDoAction(int value, int X, ref int IP)
        {
            if (value == 0)
            {
                return;
            }
            if (value > 101)
            {
                Console.WriteLine($"    > {_messages[value - 50]}");
                return;
            }
            if (value < 52)
            {
                Console.WriteLine($"    > {_messages[value]}");
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
                    Console.WriteLine($"    : take {ItemDesc(P)}");
                    break;
                case 2: // drop
                    P = TestGetDataValue(X, ref IP);
                    Console.WriteLine($"    : drop {ItemDesc(P)}");
                    break;
                case 3: // teleport
                    P = TestGetDataValue(X, ref IP);
                    Console.WriteLine($"    : teleport to {RoomDesc(P)}");
                    break;
                case 4: // item to nowhere
                case 8: // item to nowhere
                    P = TestGetDataValue(X, ref IP);
                    Console.WriteLine($"    : send {ItemDesc(P)} to nowhere");
                    break;
                case 5: // turn on dark
                    Console.WriteLine($"    : turn on dark");
                    break;
                case 6: // turn off dark
                    Console.WriteLine($"    : turn off dark");
                    break;
                case 7: // turn on flag
                    P = TestGetDataValue(X, ref IP);
                    Console.WriteLine($"    : set flag {P} true");
                    break;
                case 9: // turn off flag
                    P = TestGetDataValue(X, ref IP);
                    Console.WriteLine($"    : set flag {P} false");
                    break;
                case 10: // dead
                    Console.WriteLine("    : dead");
                    break;
                case 11: // item goes to room
                    P = TestGetDataValue(X, ref IP);
                    P1 = TestGetDataValue(X, ref IP);
                    Console.WriteLine($"    : {ItemDesc(P)} goes to {RoomDesc(P1)}");
                    break;
                case 12: // game over
                    Console.WriteLine("    : game over");
                    break;
                case 13: // look
                    Console.WriteLine("    : look");
                    break;
                case 14: // check treasures
                    Console.WriteLine("    : check treasures");
                    break;
                case 15: // show inventory
                    Console.WriteLine("    : show inventory");
                    break;
                case 16: // flag 0 true
                    Console.WriteLine("    : set flag 0 true");
                    break;
                case 17: // flag 0 false
                    Console.WriteLine("    : set flag 0 false");
                    break;
                case 18: // torch recharged
                    Console.WriteLine("    : torch is recharged and sent nowhere");
                    break;
                case 19: // clear screen
                    Console.WriteLine("    : clear screen");
                    break;
                case 20: // save game
                    Console.WriteLine("    : save game");
                    break;
                case 21: // swap two items
                    P = TestGetDataValue(X, ref IP);
                    P1 = TestGetDataValue(X, ref IP);
                    Console.WriteLine($"    : swap items {ItemDesc(P)} and {ItemDesc(P1)}");
                    break;
                default:
                    Console.WriteLine($"    : #{value - 51}#");
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
            return result.Replace(" ", "_").Replace("*", "");
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
