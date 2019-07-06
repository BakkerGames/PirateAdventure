// Program.SaveLoad.cs - 01/29/2019

// Note: This Save/Load has checksum logic which relies on ordering of key/value pairs
// in a JObject. When a JObject is serialized to a string and back, if the order of the
// key/value pairs is not the same, the checksum will fail. If this is a possibility,
// the checksum logic will need to be altered. Since the JSON.org definition of a JObject
// is that it is an unordered list, then some implementations may not preserve the order.

using Common.JSON;
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PirateAdventure
{
    public partial class Program
    {
        private static string savePath = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\{_gameName}";
        private static string saveFilename = "savedata.json";

        private static void SaveGameData()
        {
            JObject saveData = new JObject();
            JObject fullData = new JObject();
            saveData.Add("gamename", _gameName);
            saveData.Add("version", _version);
            saveData.Add("savedate", DateTime.UtcNow);
            saveData.Add("gameover", gameOver);
            saveData.Add("nummoves", numMoves);
            saveData.Add("currroomnumber", currRoomNumber);
            saveData.Add("darkflag", darkFlag);
            saveData.Add("lightremaining", lightRemaining);
            for (int i = 0; i < _itemCount; i++)
            {
                saveData.Add($"itemlocation_{i}", _itemLocation[i]);
            }
            for (int i = 0; i < _flagCount; i++)
            {
                saveData.Add($"systemflag_{i}", _systemFlags[i]);
            }
            fullData.Add(_gameName, saveData);
            fullData.Add("checksum", CalcMD5OfString(saveData.ToString()));
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            File.WriteAllText($"{savePath}\\{saveFilename}", fullData.ToString());
        }

        private static bool LoadGameData()
        {
            if (runScript)
            {
                return false;
            }
            if (!Directory.Exists(savePath))
            {
                return false;
            }
            if (!File.Exists($"{savePath}\\{saveFilename}"))
            {
                return false;
            }
            Console.Write("LOAD SAVE GAME FILE? [Y/N] ");
            string answer = Console.ReadLine();
            if (!string.IsNullOrEmpty(answer) && !answer.ToUpper().StartsWith("Y"))
            {
                Console.Write("DELETE OLD SAVE GAME FILE? [Y/N] ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && !answer.ToUpper().StartsWith("Y"))
                {
                    Console.WriteLine();
                    return false;
                }
                File.Delete($"{savePath}\\{saveFilename}");
                Console.WriteLine("SAVE GAME FILE DELETED");
                Console.WriteLine();
                return false;
            }
            JObject fullData = JObject.Parse(File.ReadAllText($"{savePath}\\{saveFilename}"));
            JObject saveData = (JObject)fullData.GetValue(_gameName);
            string md5Checksum = (string)fullData.GetValue("checksum");
            if (!md5Checksum.Equals(CalcMD5OfString(saveData.ToString())))
            {
                throw new SystemException("Corrupt SaveData file found");
            }
            if (!_gameName.Equals((string)saveData.GetValue("gamename"))
                || _version != (int)saveData.GetValue("version"))
            {
                throw new SystemException("Incorrect SaveData file found");
            }
            string savedate = (string)saveData.GetValue("savedate");
            DateTime saveDateTime = DateTime.Parse(savedate, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            Console.WriteLine();
            Console.WriteLine($"LOADING DATA FROM {TimeZoneInfo.ConvertTimeFromUtc(saveDateTime, TimeZoneInfo.Local)}...");
            gameOver = (bool)saveData.GetValue("gameover");
            numMoves = (int)saveData.GetValue("nummoves");
            currRoomNumber = (int)saveData.GetValue("currroomnumber");
            darkFlag = (bool)saveData.GetValue("darkflag");
            lightRemaining = (int)saveData.GetValue("lightremaining");
            for (int i = 0; i < _itemCount; i++)
            {
                _itemLocation[i] = (int)saveData.GetValue($"itemlocation_{i}");
            }
            for (int i = 0; i < _flagCount; i++)
            {
                _systemFlags[i] = (bool)saveData.GetValue($"systemflag_{i}");
            }
            File.Delete($"{savePath}\\{saveFilename}");
            Console.WriteLine();
            return true;
        }

        private static string CalcMD5OfString(string value)
        {
            StringBuilder hexResult = new StringBuilder();
            MD5 hasher = MD5.Create();
            byte[] byteBuffer = Encoding.UTF8.GetBytes(value);
            byte[] md5Result = hasher.ComputeHash(byteBuffer);
            foreach (byte b in md5Result)
            {
                hexResult.Append(b.ToString("x2"));
            }
            return hexResult.ToString();
        }
    }
}
