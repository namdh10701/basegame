using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Features.Shop;
using _Game.Scripts.Bootstrap;
using _Game.Scripts.InventorySystem;

namespace _Game.Scripts.SaveLoad
{
    public class SaveData
    {
        // public static SaveData DefaultSave = GetDefaultSave();

        public int SaveVersion;
        public int SaveId;
        // public SkillSaveData SkillSaveData;
        // public InventorySaveData InventorySaveData;
        public ShipSetupSaveData ShipSetupSaveData;
        public List<ItemData> OwnedItems = new();
        public List<ItemData> OwnedShips => OwnedItems.Where(v => v.ItemType == ItemType.SHIP).ToList();

        public int gold = 100000;
        public int gem;
        public int energy;
        public int maxEnergy;

        public MapStatusSaveData MapStatus;

        public TalentSaveData Talent;

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
            SaveData defaultSave = new SaveData(1);;
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

    public class TalentSaveData
    {
        public int CurrentLevel;
        public int OwnedNormalTalentId;
        public int OwnedPreTalentId;
    }
}

