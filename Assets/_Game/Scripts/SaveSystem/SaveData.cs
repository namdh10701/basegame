using System.Collections.Generic;
using _Game.Features.Shop;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.SkillSystem;
using UnityEngine;
namespace _Game.Scripts.SaveLoad
{
    public class SaveData
    {
        public static SaveData DefaultSave = GetDefaultSave();

        public int SaveVersion;
        public int SaveId;
        // public SkillSaveData SkillSaveData;
        // public InventorySaveData InventorySaveData;
        public ShipSetupSaveData ShipSetupSaveData;

        public CountOfGacha CountOfGacha;

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

            // InventorySaveData inventorySaveData = new InventorySaveData();
            //
            // List<IInventoryData> owned = new List<IInventoryData>();
            // List<GearData> equipingGear = new List<GearData>();
            //
            // GearData gearData = new GearData(1, GearType.Sword, Rarity.Common);
            // Debug.Log(gearData.Id + " DEFAULT");
            // owned.Add(gearData);
            // owned.Add(new GearData(2, GearType.Hat, Rarity.Rare));
            //
            //
            // inventorySaveData.OwnedInventories = owned;
            // inventorySaveData.EquippingGears = equipingGear;
            // defaultSave.InventorySaveData = inventorySaveData;
            //
            //
            // SkillSaveData skillSaveData = new SkillSaveData(1);
            // skillSaveData.SkillDatas.Add(new SkillData(1, 1));
            // skillSaveData.SkillDatas.Add(new SkillData(2, 1));
            //
            // defaultSave.SkillSaveData = skillSaveData;

            defaultSave.ShipSetupSaveData = new ShipSetupSaveData();
            defaultSave.ShipSetupSaveData.Init();

            return defaultSave;
        }

        public List<GearData> GetOwnedGears()
        {
            List<GearData> gearDatas = new List<GearData>();
            // foreach (IInventoryData inventoryData in InventorySaveData.OwnedInventories)
            // {
            //     if (inventoryData is GearData gearData)
            //     {
            //         gearDatas.Add(gearData);
            //     }
            // }
            return gearDatas;
        }
    }
}

