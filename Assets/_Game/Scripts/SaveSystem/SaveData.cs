using System.Collections.Generic;
using System.IO;
using _Base.Scripts.SaveSystem;
using _Game.Scripts.InventorySystem;
using UnityEngine;
namespace _Game.Scripts.SaveLoad
{
    public class SaveData
    {
        public static SaveData DefaultSave = GetDefaultSave();

        public int SaveVersion;
        public int SaveId;
        public SkillSaveData SkillSaveData;
        public InventorySaveData InventorySaveData;

        public SaveData(int saveId)
        {
            SaveId = saveId;
        }
        public SaveData()
        {
        }


        public static SaveData GetDefaultSave()
        {
            SaveData defaultSave = new SaveData(1);

            InventorySaveData inventorySaveData = new InventorySaveData();

            List<InventoryData> owned = new List<InventoryData>();
            List<GearData> equipingGear = new List<GearData>();

            owned.Add(new GearData(1, GearType.Sword));
            owned.Add(new GearData(1, GearType.Hat));

            owned.Add(new InventoryData(1, InventoryType.Potion));
            inventorySaveData.OwnedInventories = owned;
            inventorySaveData.EquippingGears = equipingGear;
            defaultSave.InventorySaveData = inventorySaveData;

            return defaultSave;
        }

        public List<GearData> GetOwnedGears()
        {
            List<GearData> gearDatas = new List<GearData>();
            foreach (InventoryData inventoryData in InventorySaveData.OwnedInventories)
            {
                if (inventoryData is GearData gearData)
                {
                    gearDatas.Add(gearData);
                }
            }
            return gearDatas;
        }
    }
}

