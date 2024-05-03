using _Base.Scripts.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.SaveLoad
{
    public static class SaveSystem
    {
        public static SaveData GameSave;
        public static void LoadSave()
        {
            GameSave = SaveLoadManager.ReadSave(1);
            if (GameSave == null)
            {
                SaveLoadManager.WriteDefaultSave(SaveData.DefaultSave);
                GameSave = SaveLoadManager.ReadSave(1);
            }
        }
        public static void SaveGame()
        {
            SaveLoadManager.WriteSave(GameSave);
        }
    }
}