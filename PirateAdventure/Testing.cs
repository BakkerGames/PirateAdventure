// Testing.cs - 12/01/2017

using System;

namespace PirateAdventure
{
    partial class Program
    {
        public static void TestData()
        {
            for (int X = 0; X < CL_commandCount; X++)
            {
                int verb = CA[X, 0] / 150;
                int noun = CA[X, 0] % 150;
                if (verb == 0)
                {
                    Console.WriteLine($"{noun}%");
                }
                else if (noun == 0)
                {
                    Console.WriteLine($"{NVS[verb, 0]}");
                }
                else
                {
                    Console.WriteLine($"{NVS[verb, 0]} {NVS[noun, 1]}");
                }
                for (int w = 1; w <= 5; w++)
                {
                    int ll = CA[X, w] / 20;
                    int k = CA[X, w] % 20;
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
                    int y1 = CA[X, y] / 150;
                    int y2 = CA[X, y] % 150;
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
                Console.WriteLine($"    > {MSS_messages[value - 50]}");
                return;
            }
            if (value < 52)
            {
                Console.WriteLine($"    > {MSS_messages[value]}");
                return;
            }
            int P = 0;
            switch (value - 51)
            {
                case 0:
                    // do nothing
                    break;
                case 3: // teleport
                    P = GetEmbeddedValue(X, ref IP);
                    Console.WriteLine($"    : teleport to {RoomDesc(P)}");
                    break;
                default:
                    Console.WriteLine($"    : {value - 51}");
                    break;
            }
        }

        private static int GetEmbeddedValue(int X, ref int IP)
        {
            int P = 0;
            int W = 0;
            int M = 0;
            do
            {
                IP++;
                W = CA[X, IP];
                P = W / 20;
                M = W % 20;
            } while (M != 0);
            return P;
        }

        public static string ItemDesc(int value)
        {
            string result = IAS_itemDescriptions[value];
            if (result.EndsWith("/"))
            {
                result = result.Substring(0, result.IndexOf("/"));
            }
            return result.Replace(" ", "_").Replace("*", "");
        }

        public static string RoomDesc(int value)
        {
            string result = RSS[value];
            if (result.EndsWith("/"))
            {
                result = result.Substring(result.IndexOf("/") + 1);
                result = result.Substring(0, result.Length - 1);
            }
            return result.Replace(" ", "_");
        }
    }
}
