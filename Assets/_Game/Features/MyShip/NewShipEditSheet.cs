using System;
using _Game.Features.Inventory;
using _Game.Features.MyShip.GridSystem;
using _Game.Scripts.DB;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityWeld.Binding;

namespace _Game.Features.MyShip
{
    public enum ViewMode
    {
        NORMAL,
        CONFIG_SHIP,
        CONFIG_STASH,
    }

    [Binding]
    public class NewShipEditSheet : SheetWithViewModel
    {
        public RectTransform ConfigSheet;
        public RectTransform ShipSpawnPoint;
        public RectTransform InventorySheet;
        public ShipConfigManager ShipConfigManager;

        #region ViewMode
        public ViewMode _viewMode = ViewMode.NORMAL;
        
        private void SetViewMode(ViewMode viewMode)
        {
            _viewMode = viewMode;

            if (viewMode == ViewMode.NORMAL)
            {
                ConfigSheet_PosY = 0;
                InventorySheet_PosY = 0;
            }
            else if (viewMode == ViewMode.CONFIG_SHIP)
            {
                ConfigSheet_PosY = 150;
                InventorySheet_PosY = (InventorySheet.transform as RectTransform)!.rect.height;
            }
            else if (viewMode == ViewMode.CONFIG_STASH)
            {
                ConfigSheet_PosY = 700;
                InventorySheet_PosY = (InventorySheet.transform as RectTransform)!.rect.height;
            }
            
            OnPropertyChanged(nameof(IsConfigMode));
        }

        #region Binding Prop: ConfigSheet_PosY

        /// <summary>
        /// ConfigSheet_PosY
        /// </summary>
        [Binding]
        public float ConfigSheet_PosY
        {
            get => _configSheet_PosY;
            set
            {
                if (Equals(_configSheet_PosY, value))
                {
                    return;
                }

                _configSheet_PosY = value;

                // var rect = ConfigSheet.transform as RectTransform;
                // rect.anchoredPosition = new Vector2(
                //     rect.anchoredPosition.x,
                //     value);
                // rect.position = new Vector3(
                //     rect.position.x, 
                //     value, 
                //     rect.position.z
                // );
                
                var rect = (RectTransform)ConfigSheet.transform;
                rect.DOAnchorPos(new Vector2(rect.anchoredPosition.x, value), 0.5f, true);
                
                OnPropertyChanged(nameof(ConfigSheet_PosY));
            }
        }

        private float _configSheet_PosY;

        #endregion

        #region Binding Prop: InventorySheet_PosY

        /// <summary>
        /// InventorySheet_PosY
        /// </summary>
        [Binding]
        public float InventorySheet_PosY
        {
            get => _inventorySheet_PosY;
            set
            {
                if (Equals(_inventorySheet_PosY, value))
                {
                    return;
                }

                _inventorySheet_PosY = value;
                
                var rect = (RectTransform)InventorySheet.transform;
                rect.DOAnchorPos(new Vector2(rect.anchoredPosition.x, value), 0.5f, true);
                // rect.anchoredPosition = new Vector2(
                //     rect.anchoredPosition.x,
                //     value);
                // rect.position = new Vector3(
                //     rect.position.x, 
                //     value, 
                //     rect.position.z
                // );
                
                OnPropertyChanged(nameof(InventorySheet_PosY));
            }
        }

        private float _inventorySheet_PosY;

        #endregion

        [Binding]
        public bool IsConfigMode => _viewMode != ViewMode.NORMAL;
        
        [Binding]
        public void SetViewMode_Normal()
        {
            SetViewMode(ViewMode.NORMAL);
        }
        
        [Binding]
        public void SetViewMode_ConfigShip()
        {
            SetViewMode(ViewMode.CONFIG_SHIP);
        }
        
        [Binding]
        public void SetViewMode_ConfigStash()
        {
            SetViewMode(ViewMode.CONFIG_STASH);
        }

        [Binding]
        public void ToggleConfigMode_ShipVsStash()
        {
            if (_viewMode == ViewMode.CONFIG_SHIP)
            {
                SetViewMode(ViewMode.CONFIG_STASH);
            }
            else if (_viewMode == ViewMode.CONFIG_STASH)
            {
                SetViewMode(ViewMode.CONFIG_SHIP);
            }
        }
        #endregion

        public Transform InventorySheetDragPane;

        public DraggingItem CreateDragItem(InventoryItem item)
        {
            var prefab = GetDragItemPrefab(item);
            var instance = Instantiate(prefab, InventorySheetDragPane);

            return instance;
        }

        

        public DraggingItem GetDragItemPrefab(InventoryItem item)
        {
            var shapePath = $"SetupItems/SetupItem_{item.Type.ToString().ToLower()}_{item.OperationType}";
            var prefab = Resources.Load<DraggingItem>(shapePath);
            return prefab;
        }

        [Binding]
        public async void NavBack()
        {
            // await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true);
        }
        
        public override UniTask Initialize(Memory<object> args)
        {
            // _btnShipEdit.onValueChanged.AddListener(OnShipEditClick);
            // _btnRemoveAll.onClick.AddListener(OnRemoveAllClick);
            // _btnRemove.onClick.AddListener(OnRemoveClick);
            //
            // Initialize(_shipsConfig.currentShipId);
            InitializeShip("0003");
            SetViewMode_Normal();
            return UniTask.CompletedTask;
        }

        void InitializeShip(string shipID)
        {
            var shipPrefabs = Resources.Load($"Ships/Ship_{shipID}");
            var ship = Instantiate(shipPrefabs, ShipSpawnPoint);
            ShipConfigManager.Grid = ship.GetComponentInChildren<SlotGrid>();
            ShipConfigManager.PlacementPane = ship.GetComponentInChildren<PlacementPane>().transform;
            
            // ShipSpawnPoint.parent.gameObject.GetComponent<ShipConfigManager>()
        }
        
        public class InputData<T>
        {
            public T Value { get; }

            protected InputData(T data)
            {
                Value = data;
            }
        }
    }
}