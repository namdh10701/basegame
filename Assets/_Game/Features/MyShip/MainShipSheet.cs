using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.UI;
using _Game.Features.Home;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
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
        [SerializeField] ShipConfig _shipsConfig;
        [SerializeField] GameObject _tabEditShip;
        [SerializeField] Button _btnShipSelection;
        [SerializeField] Button _btnShipEdit;
        [SerializeField] Transform _parentShip;

        GridManager _gridManager;
        GameObject _ship;
        const string directory = "Assets/_Game/Scriptable Objects/ShipGridProfiles/";
        public override UniTask Initialize(Memory<object> args)
        {
            _btnShipEdit.onClick.AddListener(OnShipEditClick);

            Initialize(_shipsConfig.currentShipId);
            return UniTask.CompletedTask;
        }

        void Initialize(string shipID)
        {
            foreach (var ship in _shipsConfig.ships)
            {
                if (ship.id == shipID)
                {
                    if (_ship != null)
                    {
                        Destroy(_ship);
                    }
                    _ship = Instantiate(ship.ship);
                    _ship.transform.SetParent(_parentShip, false);
                    _gridManager = _ship.GetComponentInChildren<GridManager>();
                }
            }

            _gridManager.Initialize();
            _gridManager.LoadInventoryItems();
            EnableDragItem(false);
        }

        public override UniTask Cleanup(Memory<object> args)
        {
            _btnShipEdit.onClick.RemoveListener(OnShipEditClick);
            return UniTask.CompletedTask;
        }

        private void EnableDragItem(bool enable)
        {
            _gridManager.EnableDragItem(enable);
            _tabEditShip.SetActive(enable);
        }

        private void OnShipEditClick()
        {
            EnableDragItem(true);
        }

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
            _shipsConfig.currentShipId = shipId;
            Initialize(_shipsConfig.currentShipId);
            Debug.Log("Selected ship ID: " + _shipsConfig.currentShipId);
        }

        private void OnEquipmentItemsReceived(List<InventoryItem> items)
        {
            foreach (var inventoryItemData in items)
            {
                AddInventoryItems(inventoryItemData);
                Debug.Log("Received: " + inventoryItemData.Id);
            }
            _gridManager.LoadInventoryItems();
            EnableDragItem(false);
        }

        private void AddInventoryItems(InventoryItem item)
        {
            var inventoryItems = new List<_Base.Scripts.UI.InventoryItemInfo>();
            foreach (var inventoryItem in _gridManager.InventoryItemsReceivedDef.inventoryItemsReceived)
            {
                if (item.Type == inventoryItem.Type)
                {
                    foreach (var itemReceived in inventoryItem.inventoryItemsInfo)
                    {
                        if (itemReceived.inventoryItemData.gridItemDef.Id == item.Id)
                        {
                            inventoryItems.Add(itemReceived);
                        }
                    }
                }

            }
            _gridManager.AddInventoryItemsInfo(inventoryItems);

        }

        [Binding]
        public async void NavToShipSelectionSheet()
        {
            await MyShipScreen.Instance.ShowSheet(Sheets.ShipSelectionSheet, _shipsConfig.currentShipId);
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