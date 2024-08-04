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
        public static SaveData DefaultSave = GetDefaultSave();

        public int SaveVersion;
        public int SaveId;
        // public SkillSaveData SkillSaveData;
        // public InventorySaveData InventorySaveData;
        public ShipSetupSaveData ShipSetupSaveData;
        public List<ItemData> OwnedItems = new();
        public List<ItemData> OwnedShips => OwnedItems.Where(v => v.ItemType == ItemType.SHIP).ToList();

        public CountOfGacha CountOfGacha;

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
            SaveData defaultSave = new SaveData(1);
            defaultSave.maxEnergy = 100;
            defaultSave.energy = defaultSave.maxEnergy;

            defaultSave.OwnedItems = new List<ItemData>()
            {
                new(ItemType.SHIP, Game.IDGenerator.Next(), "0001", 0, 1),
                new(ItemType.SHIP, Game.IDGenerator.Next(), "0002", 0, 1),
                new(ItemType.SHIP, Game.IDGenerator.Next(), "0003", 0, 1),
                new(ItemType.CREW, Game.IDGenerator.Next(), "2011", 0, 1),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0001", 0, 1),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0001", 0, 2),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0001", 0, 1),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0002", 1, 4),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0012", 0, 1),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0023", 0, 1),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0043", 0, 1),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0061", 0, 1),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0069", 0, 1),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0077", 0, 1),
                new(ItemType.CANNON, Game.IDGenerator.Next(), "0085", 0, 1),
                new(ItemType.AMMO, Game.IDGenerator.Next(), "1001", 0, 1),
                new(ItemType.AMMO, Game.IDGenerator.Next(), "1012", 0, 1),
                new(ItemType.AMMO, Game.IDGenerator.Next(), "1023", 0, 1),
                new(ItemType.AMMO, Game.IDGenerator.Next(), "1033", 0, 1),
                new(ItemType.AMMO, Game.IDGenerator.Next(), "1043", 0, 1),
                new(ItemType.AMMO, Game.IDGenerator.Next(), "1077", 0, 1),
                new(ItemType.AMMO, Game.IDGenerator.Next(), "1085", 0, 1),
                new(ItemType.MISC, Game.IDGenerator.Next(), "res_blueprint_ship", 0, 1),
                new(ItemType.MISC, Game.IDGenerator.Next(), "res_blueprint_ammo", 0, 1),
                new(ItemType.MISC, Game.IDGenerator.Next(), "res_blueprint_cannon", 0, 1),
                new(ItemType.MISC, Game.IDGenerator.Next(), "res_blueprint_cannon", 0, 1),
                new(ItemType.MISC, Game.IDGenerator.Next(), "res_blueprint_cannon", 0, 1),
            };

            defaultSave.Talent = new TalentSaveData();
            
            defaultSave.CountOfGacha = new CountOfGacha();
            defaultSave.ShipSetupSaveData = new ShipSetupSaveData();
            defaultSave.ShipSetupSaveData.CurrentShip = defaultSave.OwnedShips.First();
            defaultSave.ShipSetupSaveData.Init();

            defaultSave.Settings = new SettingSaveData();

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

