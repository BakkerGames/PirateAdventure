// Program.Commands.cs - 01/08/2019

using System;

namespace PirateAdventure
{
    public partial class Program
    {
        private static void Look()
        {
            int startPos = (_roomLongDesc[currRoomNumber].StartsWith("*")) ? 1 : 0;
            int endPos = _roomLongDesc[currRoomNumber].IndexOf("/");
            if (endPos < 0)
            {
                endPos = _roomLongDesc[currRoomNumber].Length;
            }
            if (startPos > 0)
            {
                Console.WriteLine(_roomLongDesc[currRoomNumber].Substring(startPos, endPos - startPos));
            }
            else
            {
                string AorAN = (
                    _roomLongDesc[currRoomNumber][startPos] == 'A' ||
                    _roomLongDesc[currRoomNumber][startPos] == 'E' ||
                    _roomLongDesc[currRoomNumber][startPos] == 'I' ||
                    _roomLongDesc[currRoomNumber][startPos] == 'O' ||
                    _roomLongDesc[currRoomNumber][startPos] == 'U')
                    ? "AN" : "A";
                Console.Write($"I'M IN {AorAN} ");
                Console.WriteLine(_roomLongDesc[currRoomNumber].Substring(0, endPos));
            }
            // obvious exits
            bool anyExits = false;
            for (int i = 0; i < _exitDirections; i++)
            {
                if (_roomExitArray[currRoomNumber, i] != 0)
                {
                    if (!anyExits)
                    {
                        Console.WriteLine();
                        Console.Write("OBVIOUS EXITS: ");
                        anyExits = true;
                    }
                    else
                    {
                        Console.Write(", ");
                    }
                    Console.Write(_verbNounList[i + 1, 1]);
                }
            }
            if (anyExits)
            {
                Console.WriteLine();
            }
            // items seen
            bool anythingInRoom = false;
            for (int i = 0; i < _itemCount; i++)
            {
                if (_itemLocation[i] == currRoomNumber)
                {
                    if (!anythingInRoom)
                    {
                        Console.WriteLine();
                        Console.WriteLine("YOU SEE:");
                        anythingInRoom = true;
                    }
                    Console.Write("   ");
                    string tempDesc = _itemDescriptions[i];
                    if (tempDesc.EndsWith("/"))
                    {
                        Console.WriteLine(tempDesc.Substring(0, tempDesc.IndexOf("/")));
                    }
                    else
                    {
                        Console.WriteLine(tempDesc);
                    }
                }
            }
            // done
            needToLook = false;
            countsAsMove = false;
        }

        private static void ShowInventory()
        {
            bool haveAnything = false;
            for (int i = 0; i < _itemCount; i++)
            {
                if (_itemLocation[i] == _itemInventory)
                {
                    if (!haveAnything)
                    {
                        Console.WriteLine("I'M CARRYING:");
                        haveAnything = true;
                    }
                    Console.Write("   ");
                    if (_itemDescriptions[i].EndsWith("/"))
                    {
                        Console.WriteLine(_itemDescriptions[i].Substring(0, _itemDescriptions[i].IndexOf("/")));
                    }
                    else
                    {
                        Console.WriteLine(_itemDescriptions[i]);
                    }
                }
            }
            if (!haveAnything)
            {
                Console.WriteLine("YOU AREN'T CARRYING ANYTHING");
            }
            needToLook = false;
            countsAsMove = false;
        }
    }
}
