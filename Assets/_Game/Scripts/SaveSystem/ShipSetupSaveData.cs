﻿using System;
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

        public ItemData()
        {
        }

        public ItemData(ItemType itemType, string itemId)
        {
            ItemType = itemType;
            ItemId = itemId;
        }
    }
    
    [Serializable]
    public class ShipSetupData
    {
        public Dictionary<string, ItemData> ShipData = new ();
        public Dictionary<string, ItemData> StashData = new ();
    }
    
    [Serializable]
    public class ShipSetupSaveData
    {
        public string CurrentShipId;
        public SetupProfile CurrentProfile = SetupProfile.Profile1;
        public Dictionary<string, Dictionary<string, ShipSetupData>> ShipSetupData = new ();

        public ShipSetupData CurrentShipSetupData => GetShipSetup(CurrentShipId, CurrentProfile);

        public ShipSetupData GetShipSetup(string shipId, SetupProfile setupProfile)
        {
            return ShipSetupData[shipId][setupProfile.ToString()];
        }

        public void Init()
        {
            GameData.ShipTable.GetRecords().ForEach(record =>
            {
                ShipSetupData[record.Id] = new Dictionary<string, ShipSetupData>()
                {
                    { SetupProfile.Profile1.ToString(), new ShipSetupData() },
                    { SetupProfile.Profile2.ToString(), new ShipSetupData() },
                    { SetupProfile.Profile3.ToString(), new ShipSetupData() },
                };
            });
            
            
            if (string.IsNullOrEmpty(CurrentShipId))
            {
                CurrentShipId = GameData.ShipTable.GetRecords().FirstOrDefault()?.Id;
            }
        }
    }
}