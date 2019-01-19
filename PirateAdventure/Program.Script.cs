// Program.Script.cs - 01/19/2019

using System;
using System.IO;

namespace PirateAdventure
{
    public partial class Program
    {

        public static int scriptLineNum = 0;
        public static bool scriptBreakFlag = false;

        public static string[] scriptLines;

        public static void FillScriptLines() {
            Console.Write("Enter script filename: ");
            string _scriptFilename = Console.ReadLine();
            if (string.IsNullOrEmpty(_scriptFilename))
            {
                Console.WriteLine("Filename not specified!");
                Console.Write("Press enter to continue...");
                Console.ReadLine();
                return;
            }
            // handle a string with quotes, as from shift-right-click "Copy as path"
            if (_scriptFilename.StartsWith("\""))
            {
                _scriptFilename = _scriptFilename.Substring(1);
            }
            if (_scriptFilename.EndsWith("\""))
            {
                _scriptFilename = _scriptFilename.Substring(0, _scriptFilename.Length - 1);
            }
            // load script into memory
            scriptLines = File.ReadAllLines(_scriptFilename);
        }
    }
}
