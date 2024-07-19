using System.Collections;
using System.Collections.Generic;
using System.IO;
using _Base.Scripts.JsonAdapter;
using _Game.Scripts.SaveLoad;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace _Base.Scripts.SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        static JsonSerializerSettings _settings;

        public static JsonSerializerSettings Settings
        {
            get
            {
                if (_settings != null) return _settings;
                
                _settings = new JsonSerializerSettings();
                Settings.Converters.Add(new Vector2IntConverter());
                Settings.Converters.Add(new DictionaryVector2IntICollectionConverter());
                Settings.Converters.Add(new StringEnumConverter());

                return _settings;
            }
        }

        public static void WriteSave(SaveData saveData)
        {
            RecreateSaveDirectory();
            // string saveDataString = JsonUtility.ToJson(saveData);
            string saveDataString = JsonConvert.SerializeObject(saveData, Formatting.Indented, Settings);
            Debug.Log("saveDataString: " + saveDataString);
            File.WriteAllText(GenerateSaveFileName(saveData.SaveId), saveDataString);
        }
        public static SaveData ReadSave(int slotId)
        {
            if (!File.Exists(GenerateSaveFileName(slotId)))
                return null;
            RecreateSaveDirectory();
            string saveDataString = File.ReadAllText(GenerateSaveFileName(slotId)).Trim();
            Debug.Log("saveDataString: " + saveDataString);
            return JsonConvert.DeserializeObject<SaveData>(saveDataString, Settings);
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
