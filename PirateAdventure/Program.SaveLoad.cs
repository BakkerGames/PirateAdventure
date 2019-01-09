// Program.SaveLoad.cs - 01/09/2019

// Note: This Save/Load has checksum logic which relies on ordering of key/value pairs
// in a JObject. When a JObject is converted to a string and back, if the order of the
// key/value pairs is not the same, the checksum will fail. Also, if removing the checksum
// value changes the order of the rest of the JObject, the checksum will fail. If this
// is a possibility, the checksum logic will need to be altered. Since the JSON definition
// of a JObject is that it is an unordered list, then some implementations may not preserve
// the order.

using Common.JSON;
using System;
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
            saveData.Add("checksum", CalcMD5OfString(saveData.ToString()));
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            File.WriteAllText($"{savePath}\\{saveFilename}", saveData.ToString());
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
            if (!string.IsNullOrEmpty(answer))
            {
                if (!answer.ToUpper().StartsWith("Y"))
                {
                    return false;
                }
            }
            JObject saveData = JObject.Parse(File.ReadAllText($"{savePath}\\{saveFilename}"));
            string md5Checksum = (string)saveData.GetValue("checksum");
            saveData.Remove("checksum"); // checksum value is not part of the checksum
            if (!md5Checksum.Equals(CalcMD5OfString(saveData.ToString())))
            {
                throw new SystemException("Corrupt SaveData file found");
            }
            if (!_gameName.Equals((string)saveData.GetValue("gamename"))
                || _version != (int)saveData.GetValue("version"))
            {
                throw new SystemException("Incorrect SaveData file found");
            }
            DateTime saveDataTime = (DateTime)saveData.GetValue("savedate");
            Console.WriteLine();
            Console.WriteLine($"LOADING DATA FROM {TimeZoneInfo.ConvertTimeFromUtc(saveDataTime, TimeZoneInfo.Local)}...");
            Console.WriteLine();
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
