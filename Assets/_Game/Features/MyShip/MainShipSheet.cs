using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.UI;
using _Game.Features.Home;
using Cysharp.Threading.Tasks;
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


        private string _currentShipId = "0001";

        public override UniTask Initialize(Memory<object> args)
        {
            _btnShipSelection.onClick.AddListener(OnShipSelectionClick);
            _btnShipEdit.onClick.AddListener(OnShipEditClick);


            Initialize(_currentShipId);
            return UniTask.CompletedTask;
        }

        void Initialize(string shipID)
        {
            foreach (var ship in _shipsConfig.ships)
            {
                if (ship.id == shipID)
                {
                    var shipObject = Instantiate(ship.ship);
                    shipObject.transform.SetParent(_parentShip, false);
                    _gridManager = shipObject.GetComponentInChildren<GridManager>();
                }
            }

            _gridManager.Initialize();
            _gridManager.LoadInventoryItems();
            EnableDragItem(false);
        }

        public override UniTask Cleanup(Memory<object> args)
        {
            _btnShipSelection.onClick.RemoveListener(OnShipSelectionClick);
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

        private void OnShipSelectionClick()
        {
            // _tabEditShip.SetActive(false);
            // _gridImage.SetActive(false);
            // _tash.SetActive(false);
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
            _currentShipId = shipId;
            Debug.Log("Selected ship ID: " + _currentShipId);
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

            foreach (var inventoryItem in _gridManager.InventoryReceived.inventoryItemsInfo)
            {
                if (item.Id == inventoryItem.inventoryItemData.gridItemDef.Id)
                {
                    inventoryItems.Add(inventoryItem);
                }

            }
            _gridManager.AddInventoryItemsInfo(inventoryItems);

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