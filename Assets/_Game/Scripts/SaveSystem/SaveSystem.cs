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
            
            GameSave = SaveLoadManager.ReadSave(1);
            if (GameSave == null)
            {
                SaveLoadManager.WriteDefaultSave(SaveData.DefaultSave);
                GameSave = SaveLoadManager.ReadSave(1);
            }
            
            // Load backend data
            // GameSave.MapStatus = PlayfabManager.Instance.MapStatus;
            GameSave.ShipSetupSaveData = PlayfabManager.Instance.Equipment.EquipmentShips;
            if (GameSave.ShipSetupSaveData.CurrentShip == null)
            {
                GameSave.ShipSetupSaveData.CurrentShip = GameSave.OwnedShips.First();
            }
        }
        public static void SaveGame()
        {
            DebounceUtility.Debounce(() => SaveLoadManager.WriteSave(GameSave), 500);
        }
    }
}