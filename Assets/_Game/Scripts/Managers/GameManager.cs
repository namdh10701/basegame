using _Base.Scripts;
using _Base.Scripts.SaveSystem;
using _Game.Scripts.SaveSystem;
using Map;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class GameManager : BaseGameManager
    {
        public Database.Database Database;
        public SaveData SaveData;
        public override void LoadDatabase()
        {
            Database?.Load();
        }
        public override void LoadSave()
        {
            SaveData = SaveLoadManager.ReadSave(1);
            if (SaveData == null)
            {
                SaveLoadManager.WriteDefaultSave(new SaveData(1, 0));
                SaveData = SaveLoadManager.ReadSave(1);
            }
        }
        public override void SaveGame()
        {
            SaveLoadManager.WriteSave(SaveData);
        }
    }
}
