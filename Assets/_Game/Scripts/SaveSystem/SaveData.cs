using System.Collections.Generic;
using System.IO;
using _Base.Scripts.SaveSystem;
using _Game.Scripts.InventorySystem;
namespace _Game.Scripts.SaveLoad
{
    public class SaveData : IBinarySaveData
    {
        public static SaveData DefaultSave = GetDefaultSave();

        public int SaveId;
        public InventoryCollection<Gear> EquipingGears;
        public InventoryCollection<InventoryItem> OwnedInventoryItems;
        public CharacterDefinition SelectedCharacter;

        public SaveData(int saveId)
        {
            SaveId = saveId;
        }
        public SaveData(int saveId, InventoryCollection<Gear> gearCombination, InventoryCollection<InventoryItem> ownedInventoryItems)
        {
            SaveId = saveId;
            EquipingGears = gearCombination;
            OwnedInventoryItems = ownedInventoryItems;
        }
        public SaveData()
        {

        }

        public void Read(BinaryReader br)
        {

        }

        public void Write(BinaryWriter bw)
        {
        }

        public static SaveData GetDefaultSave()
        {
            SaveData defaultSave = new SaveData(1);
            return defaultSave;
        }
    }
}

