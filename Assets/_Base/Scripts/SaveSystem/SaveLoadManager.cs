using System;
using System.IO;
using _Game.Scripts.SaveLoad;
using Newtonsoft.Json;
using UnityEngine;

namespace _Base.Scripts.SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        public static void WriteSave(SaveData saveData)
        {
            RecreateSaveDirectory();
            string saveDataString = JsonUtility.ToJson(saveData);
            File.WriteAllText(GenerateSaveFileName(saveData.SaveId), saveDataString);
        }
        public static SaveData ReadSave(int slotId)
        {
            if (!File.Exists(GenerateSaveFileName(slotId)))
                return null;
            RecreateSaveDirectory();
            string saveDataString = File.ReadAllText(GenerateSaveFileName(slotId));
            SaveData saveData = JsonUtility.FromJson<SaveData>(saveDataString);
            return saveData;
        }
        private static void RecreateSaveDirectory()
        {
            if (!Directory.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Saves"))
                Directory.CreateDirectory(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Saves");
        }

        private static string GenerateSaveFileName(int slotId)
        {
            return Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Saves" +
                   Path.AltDirectorySeparatorChar + "SOF" + slotId + ".dat";
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
