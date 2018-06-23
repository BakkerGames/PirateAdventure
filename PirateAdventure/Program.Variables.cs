// Program.Variables.cs - 06/22/2018

using System;

namespace PirateAdventure
{
    public partial class Program
    {
        private static Random sysRand = new Random();
        private static bool gameOver = false;
        private static int numMoves = 0;
        private static string currCommandLine = "";
        private static string currVerb = "";
        private static string currNoun = "";
        private static int currVerbNumber = 0;
        private static int currNounNumber = 0;
        private static int currRoomNumber = 1;
        private static bool needToLook = true;
        private static bool countsAsMove = false;

        private static void Initialize()
        {
            gameOver = false;
            numMoves = 0;
            currCommandLine = "";
            currVerb = "";
            currNoun = "";
            currVerbNumber = 0;
            currNounNumber = 0;
            currRoomNumber = 1;
            needToLook = true;
            countsAsMove = false;
            for (int i = 0; i < _itemCount; i++)
            {
                _itemLocation[i] = _itemStartLocation[i];
            }
        }
    }
}
