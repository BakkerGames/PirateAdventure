// Program.Parse.cs - 12/04/2018

namespace PirateAdventure
{
    public partial class Program
    {
        private static bool ParseCommand()
        {
            // split into verb noun
            currVerb = "";
            currNoun = "";
            currVerbNumber = 0;
            currNounNumber = 0;
            if (string.IsNullOrWhiteSpace(currCommandLine))
            {
                return false;
            }
            if (currCommandLine.Contains(" "))
            {
                int pos = currCommandLine.IndexOf(" ");
                currVerb = currCommandLine.Substring(0, pos).Trim();
                currNoun = currCommandLine.Substring(pos).Trim();
                if (currNoun.Contains(" "))
                {
                    return false;
                }
            }
            else
            {
                currVerb = currCommandLine;
            }
            if (currVerb.Length > _wordSize)
            {
                currVerb = currVerb.Substring(0, _wordSize);
            }
            if (currNoun.Length > _wordSize)
            {
                currNoun = currNoun.Substring(0, _wordSize);
            }
            // find numeric values of verb noun
            for (int i = 0; i < _wordCount; i++)
            {
                string temp0 = _verbNounList[i, 0];
                string temp1 = _verbNounList[i, 1];
                // remove leading asterisk indicating alias
                if (temp0.StartsWith("*"))
                {
                    temp0 = temp0.Substring(1);
                }
                if (temp1.StartsWith("*"))
                {
                    temp1 = temp1.Substring(1);
                }
                if (temp0.Length > _wordSize) // trim longer verbs
                {
                    temp0 = temp0.Substring(0, _wordSize);
                }
                if (temp1.Length > _wordSize) // trim longer nouns
                {
                    temp1 = temp1.Substring(0, _wordSize);
                }
                if (!string.IsNullOrEmpty(temp0) && currVerb.Equals(temp0))
                {
                    currVerbNumber = i;
                }
                if (!string.IsNullOrEmpty(currNoun) && !string.IsNullOrEmpty(temp1) && currNoun.Equals(temp1))
                {
                    currNounNumber = i;
                }
            }
            // bump alias up to base word
            while (currVerbNumber > 0 && _verbNounList[currVerbNumber, 0].StartsWith("*"))
            {
                currVerbNumber--;
            }
            while (currNounNumber > 0 && _verbNounList[currNounNumber, 1].StartsWith("*"))
            {
                currNounNumber--;
            }
            // check if not found
            if (currVerbNumber == 0)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(currNoun) && currNounNumber == 0)
            {
                return false;
            }
            // done
            return true;
        }
    }
}
