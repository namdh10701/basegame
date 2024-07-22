using System.Collections.Generic;
using _Game.Features.Shop;
using _Game.Scripts.InventorySystem;

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
        public List<string> OwnedShips = new();
        public List<ItemData> OwnedItems = new();

        public CountOfGacha CountOfGacha;

        public int gold;
        public int gem;
        public int energy;
        public int maxEnergy;

        public MapStatusSaveData MapStatus;

        public SettingSaveData Settings;

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
            defaultSave.maxEnergy = 100;
            defaultSave.energy = defaultSave.maxEnergy;

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
            defaultSave.CountOfGacha = new CountOfGacha();
            defaultSave.ShipSetupSaveData = new ShipSetupSaveData();
            defaultSave.ShipSetupSaveData.Init();

            defaultSave.Settings = new SettingSaveData();

            defaultSave.OwnedShips = new()
            {
                "0001",
                "0002",
                "0003",
            };
            
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

    public class SettingSaveData
    {
        public string Language;
        public bool MuteBGM;
        public bool MuteSFX;
    }

    public class MapStatusSaveData
    {
        public string StageId;
        public Map.Map SeaMap;
    }
}

