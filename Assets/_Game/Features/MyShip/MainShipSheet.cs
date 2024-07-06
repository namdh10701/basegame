using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.UI;
using _Game.Features.MyShipScreen;
using _Game.Features.Inventory;
using _Game.Scripts.DB;
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
        [SerializeField] _Base.Scripts.UI.ShipConfig _shipsConfig;
        [SerializeField] GameObject _tabEditShip;
        [SerializeField] Button _btnShipSelection;
        [SerializeField] Toggle _btnShipEdit;
        [SerializeField] Transform _parentShip;

        [SerializeField] Button _btnRemoveAll;
        [SerializeField] Button _btnRemove;

        GridManager _gridManager;
        GameObject _ship;

        public override UniTask Initialize(Memory<object> args)
        {
            _btnShipEdit.onValueChanged.AddListener(OnShipEditClick);
            _btnRemoveAll.onClick.AddListener(OnRemoveAllClick);
            _btnRemove.onClick.AddListener(OnRemoveClick);

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

            _gridManager.Initialize(_shipsConfig.currentShipId);
            _gridManager.LoadInventoryItems();
            EnableDragItem(false);
        }

        public override UniTask Cleanup(Memory<object> args)
        {
            _btnShipEdit.onValueChanged.RemoveListener(OnShipEditClick);
            _btnRemoveAll.onClick.RemoveListener(OnRemoveAllClick);
            _btnRemove.onClick.RemoveListener(OnRemoveClick);

            return UniTask.CompletedTask;
        }

        private void EnableDragItem(bool enable)
        {
            _gridManager.EnableDragItem(enable);
            _tabEditShip.SetActive(enable);
        }

        private void OnShipEditClick(bool enable)
        {
            if (!enable)
                OnRemoveClick();
            EnableDragItem(enable);
        }

        private void OnRemoveAllClick()
        {
            OnRemoveClick();
            _gridManager.RemoveAllInventoryItems();
        }

        private void OnRemoveClick()
        {

            foreach (var item in _gridManager.InventoryItemsOnGrid)
            {
                item.EnableButtonClose(() =>
                {
                    var list = new List<InventoryItemData>();
                    list.Add(item.GetInventorInfo());
                    _gridManager.RemoveInventoryItemsInfo(list, _gridManager.GridConfig.grids[0]);
                    _gridManager.InventoryItemsOnGrid.Remove(item);
                    Destroy(item.gameObject);
                });
            }

            foreach (var item in _gridManager.InventoryItemsOnStash)
            {
                item.EnableButtonClose(() =>
                {
                    var list = new List<InventoryItemData>();
                    list.Add(item.GetInventorInfo());
                    _gridManager.RemoveInventoryItemsInfo(list, _gridManager.GridConfig.grids[1]);
                    _gridManager.InventoryItemsOnStash.Remove(item);
                    Destroy(item.gameObject);
                    Debug.Log("_gridManager.InventoryItemsOnStash: " + _gridManager.InventoryItemsOnStash.Count);
                    Debug.Log("_gridManager.InventoryItemsOnStash: " + _gridManager.InventoryItemsOnStash.Count);

                });
            }
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
        }

        private void OnEquipmentItemsReceived(List<InventoryItem> items)
        {
            if (items.Count == 0) return;
            
            var inventoryItemsData = new List<InventoryItemData>();
            foreach (var inventoryItemData in items)
            {
                inventoryItemsData.Add(AddInventoryItems(inventoryItemData));
            }

            _gridManager.AddInventoryItemsInfo(inventoryItemsData);
            _gridManager.LoadInventoryItemsOnGrid(_gridManager.GridConfig.grids[1].ItemsReceived.InventoryItemsData, _gridManager.GridConfig.grids[1], _gridManager.ParentCells[1]);
        }

        private InventoryItemData AddInventoryItems(InventoryItem item)
        {
            InventoryItemData inventoryItemData = new InventoryItemData();
            inventoryItemData.Id = item.Id;
            inventoryItemData.Type = item.Type;
            inventoryItemData.Shape = Database.GetShapeByTypeAndOperationType(item.Id, item.Type);
            switch (item.Type)
            {
                case ItemType.CANNON:
                    inventoryItemData.Image = Database.GetCannonImage(item.Id);
                    break;
                case ItemType.CREW:
                    inventoryItemData.Image = Database.GetCrewImage(item.Id);
                    break;
                case ItemType.AMMO:
                    inventoryItemData.Image = Database.GetAmmoImage(item.Id);
                    break;

            }

            return inventoryItemData;
        }

        [Binding]
        public async void NavToShipSelectionSheet()
        {
            OnRemoveClick();
            await MyShipScreen.MyShipScreen.Instance.ShowSheet(Sheets.ShipSelectionSheet, _shipsConfig.currentShipId);
        }

        [Binding]
        public async void NavToEquipmentScreen()
        {
            var itemList = GetCurrentInventoryItemList();
            OnRemoveClick();
            await MyShipScreen.MyShipScreen.Instance.ShowSheet(Sheets.EquipmentSheet, itemList);
        }

        private List<InventoryItemData> GetCurrentInventoryItemList()
        {
            return _gridManager.GridConfig.grids[1].ItemsReceived.InventoryItemsData;
        }
    }
}