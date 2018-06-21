// Program.Variables.cs - 06/21/2018

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
    }
}
