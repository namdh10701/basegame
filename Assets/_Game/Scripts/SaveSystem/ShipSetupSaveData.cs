using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.SaveLoad
{
    [Serializable]
    public enum SetupProfile
    {
        Profile1,
        Profile2,
        Profile3,
    }
    
    [Serializable]
    public class ItemData
    {
        public ItemType ItemType = ItemType.None;
        public string ItemId;
    }
    
    [Serializable]
    public class ShipSetupData
    {
        public Dictionary<Vector2Int, ItemData> ShipData = new ();
        public Dictionary<int, ItemData> StashData = new ();
    }
    
    [Serializable]
    public class ShipSetupSaveData
    {
        public string CurrentShipId;
        public SetupProfile CurrentProfile = SetupProfile.Profile1;
        public Dictionary<string, Dictionary<SetupProfile, ShipSetupData>> ShipSetupData = new ();

        // public ShipSetupData CurrentShipSetupData => ShipSetupData[CurrentShipId][CurrentProfile];

        public ShipSetupData GetShipSetup(string shipId, SetupProfile setupProfile)
        {
            return ShipSetupData[shipId][setupProfile];
        }

        public void Init()
        {
            GameData.ShipTable.GetRecords().ForEach(record =>
            {
                ShipSetupData[record.Id] = new Dictionary<SetupProfile, ShipSetupData>()
                {
                    { SetupProfile.Profile1, new ShipSetupData() },
                    { SetupProfile.Profile2, new ShipSetupData() },
                    { SetupProfile.Profile3, new ShipSetupData() },
                };
            });
            
            
            if (string.IsNullOrEmpty(CurrentShipId))
            {
                CurrentShipId = GameData.ShipTable.GetRecords().FirstOrDefault()?.Id;
            }
        }
    }
}