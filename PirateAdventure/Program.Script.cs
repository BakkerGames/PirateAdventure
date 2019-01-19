using System.IO;

namespace PirateAdventure
{
    public partial class Program
    {

        private const string ScriptFilepath = "Scripts\\RUN_SCRIPT.TXT";
        public static int scriptLineNum = 0;
        public static bool scriptBreakFlag = false;

        public static string[] scriptLines;

        public static void FillScriptLines() {
            scriptLines = File.ReadAllLines(ScriptFilepath);
        }
    }
}
