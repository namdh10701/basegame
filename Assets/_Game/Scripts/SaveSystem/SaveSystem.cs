using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.SaveSystem;
using _Base.Scripts.Utils;
using Cysharp.Threading.Tasks;
using Online;

namespace _Game.Scripts.SaveLoad
{
    public static class SaveSystem
    {
        public static SaveData GameSave;

        public static void LoadSave()
        {
            // FIXME: Delete
            // SaveLoadManager.DeleteSave(1);

            // GameSave = SaveLoadManager.ReadSave(1);
            // if (GameSave == null)
            // {
            //     SaveLoadManager.WriteDefaultSave(SaveData.DefaultSave);
            //     GameSave = SaveLoadManager.ReadSave(1);
            // }

            // Load backend data
            // GameSave.MapStatus = PlayfabManager.Instance.MapStatus;
            GameSave = new SaveData();
            foreach (var item in PlayfabManager.Instance.Items)
            {
                item.Level = item.Level + 1;
                GameSave.OwnedItems.Add(item);
            }
            GameSave.ShipSetupSaveData = PlayfabManager.Instance.Equipment.EquipmentShips;
            if (GameSave.ShipSetupSaveData == null)
            {
                GameSave.ShipSetupSaveData = new ShipSetupSaveData();
                GameSave.ShipSetupSaveData.Init();
            }
        }
        public static void SaveGame()
        {
            DebounceUtility.Debounce(() => SaveLoadManager.WriteSave(GameSave), 500);
        }
    }
}