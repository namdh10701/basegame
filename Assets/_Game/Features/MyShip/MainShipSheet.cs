using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.UI;
using _Game.Features.Home;
using _Game.Features.Inventory;
using _Game.Scripts;
using _Game.Scripts.GD;
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
        [SerializeField] _Base.Scripts.UI.ShipConfig _shipsConfig;
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
            var inventoryItemsData = new List<InventoryItemData>();
            foreach (var inventoryItemData in items)
            {
                inventoryItemsData.Add(AddInventoryItems(inventoryItemData));
                Debug.Log("Received: " + inventoryItemData.Id);
            }

            _gridManager.Initialize();
            _gridManager.AddInventoryItemsInfo(inventoryItemsData);
            _gridManager.LoadInventoryItems();
            EnableDragItem(false);
        }

        private InventoryItemData AddInventoryItems(InventoryItem item)
        {
            GDConfig config = null;
            InventoryItemData inventoryItemData = new InventoryItemData
            {
                gridItemDef = new GridItemDef
                {
                    Id = item.Id,
                    Type = item.Type
                }
            };

            switch (item.Type)
            {
                case ItemType.CANNON:
                    config = GDConfigLoader.Instance.Cannons[item.Id];
                    break;
                case ItemType.AMMO:
                    config = GDConfigLoader.Instance.Ammos[item.Id];
                    inventoryItemData.gridItemDef.ShapeId = 0;
                    break;
            }

            if (config is IOperationConfig op)
            {
                inventoryItemData.gridItemDef.Name = op.OperationType;
                inventoryItemData.gridItemDef.ShapeId = item.Type == ItemType.CANNON ? (int)(OperationType)Enum.Parse(typeof(OperationType), op.OperationType, true) : 0;
                inventoryItemData.gridItemDef.Path = $"Database/GridItem/{item.Type}/{op.OperationType}";
            }

            return inventoryItemData;
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