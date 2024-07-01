using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.UI;
using _Game.Features.Home;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Sheets;
using InventoryItem = _Game.Features.Inventory.InventoryItem;

namespace _Game.Features.MyShip
{
    
    [Binding]
    public class MainShipSheet : SheetWithViewModel
    {
        public class InputData<T>
        {
            public T Value { get; }

            protected InputData(T data)
            {
                Value = data;
            }
        }
        
        private string _currentShipId;
        
        public override UniTask WillEnter(Memory<object> args)
        {
            var receivedObj = args.ToArray().FirstOrDefault();

            if (receivedObj == null)
            {
                return UniTask.CompletedTask;
            }

            if (receivedObj is ShipSelectionSheet.ShipSelectionSheetOutputData shipSelectionData)
            {
                OnShipIdReceived(shipSelectionData.Value);
            } 
            else if (receivedObj is EquipmentSheet.EquipmentSheetOutputData equipmentData)
            {
                OnEquipmentItemsReceived(equipmentData.Value);
            }
            else
            {
                throw new NotSupportedException();
            }
            
            return UniTask.CompletedTask;
        }
        
        private void OnShipIdReceived(string shipId)
        {
            _currentShipId = shipId;
            Debug.Log("Selected ship ID: " + _currentShipId);
        }

        private void OnEquipmentItemsReceived(List<InventoryItem> items)
        {
            foreach (var inventoryItemData in items)
            {
                Debug.Log("Received: " + inventoryItemData.Id);
            }
        }

        [Binding]
        public async void NavToShipSelectionSheet()
        {
            await MyShipScreen.Instance.ShowSheet(Sheets.ShipSelectionSheet, _currentShipId);
        }
        
        [Binding]
        public async void NavToEquipmentScreen()
        {
            var itemList = GetCurrentInventoryItemList();
            await MyShipScreen.Instance.ShowSheet(Sheets.EquipmentSheet, itemList);
        }

        private List<InventoryItemData> GetCurrentInventoryItemList()
        {
            return new List<InventoryItemData>();
        }
    }
}