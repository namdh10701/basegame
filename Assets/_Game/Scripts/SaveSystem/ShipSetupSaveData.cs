using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using UnityEngine;

namespace _Game.Scripts.SaveLoad
{
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
        public int CurrentProfileIndex = 0;
        public Dictionary<string, List<ShipSetupData>> ShipSetupData = new ();

        public ShipSetupData CurrentShipSetupData => ShipSetupData[CurrentShipId][CurrentProfileIndex];

        public ShipSetupData SwitchProfile(int profileIndex)
        {
            CurrentProfileIndex = profileIndex;
            return CurrentShipSetupData;
        }

        public void Init()
        {
            for (int i = 0; i < 3; i++)
            {
                var shipId = i.ToString().PadLeft(4, '0');
                ShipSetupData[shipId] = new List<ShipSetupData>()
                {
                    new ShipSetupData(),
                    new ShipSetupData(),
                    new ShipSetupData(),
                };
            }
            
            
            if (string.IsNullOrEmpty(CurrentShipId))
            {
                CurrentShipId = ShipSetupData.Keys.First();
            }
        }
    }
}