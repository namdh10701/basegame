using System;
using System.IO;
using _Game.Scripts.SaveSystem;
using UnityEngine;

namespace _Base.Scripts.SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        private static readonly string SaveVersion = "1";
        public static void WriteSave(SaveData saveData)
        {
            try
            {
                RecreateSaveDirectory();

                using (var fs = new FileStream(GenerateSaveFileName(saveData.SaveId), FileMode.Create, FileAccess.Write))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        bw.Write(SaveVersion);
                        bw.Write(saveData.SaveId);
                        bw.Write(saveData.Coin);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error while writing save slot " + saveData.SaveId + "! " + ex.Message +
                               "Inner: " + (ex.InnerException != null ? ex.InnerException.Message : "None"));
            }
        }
        public static SaveData ReadSave(int slotId)
        {
            if (!File.Exists(GenerateSaveFileName(slotId)))
                return null;

            try
            {
                RecreateSaveDirectory();

                SaveData saveData = new SaveData();
                using (var fs = new FileStream(GenerateSaveFileName(slotId), FileMode.Open, FileAccess.Read))
                {
                    using (var br = new BinaryReader(fs))
                    {
                        if (br.ReadString() != SaveVersion)
                            throw new NotImplementedException("Updater for old save files is not implemented!");
                        saveData.SaveId = br.ReadInt32();
                        saveData.Coin = br.ReadInt32();
                        return saveData;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error while reading save slot " + slotId + "! " + ex.Message +
                               "Inner: " + (ex.InnerException != null ? ex.InnerException.Message : "None"));
                DeleteSave(slotId);
                return null;
            }
        }
        private static void RecreateSaveDirectory()
        {
            if (!Directory.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Saves"))
                Directory.CreateDirectory(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Saves");
        }

        private static string GenerateSaveFileName(int slotId)
        {
            return Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Saves" +
                   Path.AltDirectorySeparatorChar + "Temp2" + slotId + ".temp2";
        }
        public static void DeleteSave(int slotId)
        {
            File.Delete(GenerateSaveFileName(slotId));
        }

        public static void WriteDefaultSave(SaveData saveData)
        {
            WriteSave(saveData);
        }
    }
}
