using System;
using System.Collections.Generic;
using _Base.Scripts.Utils;
using _Game.Features.Inventory;
using _Game.Features.MyShip.GridSystem;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
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
    public class ShipItem: SubViewModel
    {
        private NewShipEditSheet _shipEditSheet;

        public ShipItem(NewShipEditSheet shipEditSheet)
        {
            _shipEditSheet = shipEditSheet;
        }
        
        #region Binding Prop: InventoryItem

        /// <summary>
        /// InventoryItem
        /// </summary>
        [Binding]
        public InventoryItem InventoryItem
        {
            get => _inventoryItem;
            set
            {
                if (Equals(_inventoryItem, value))
                {
                    return;
                }

                _inventoryItem = value;
                OnPropertyChanged(nameof(InventoryItem));
                OnPropertyChanged(nameof(IsEquipped));
                OnPropertyChanged(nameof(Thumbnail));
            }
        }

        private InventoryItem _inventoryItem;

        #endregion

        #region Binding Prop: IsEmpty

        /// <summary>
        /// IsEquipped
        /// </summary>
        [Binding]
        public bool IsEquipped => _inventoryItem != null;

        [Binding]
        public Sprite Thumbnail => IsEquipped ? _inventoryItem.Thumbnail : Resources.Load<Sprite>("Images/Group 248");

        #endregion

        [Binding]
        public void RemoveEquipment()
        {
            InventoryItem = null;
        }
    }

    [Binding]
    public class StashItem: SubViewModel
    {
        private NewShipEditSheet _shipEditSheet;

        public StashItem(NewShipEditSheet shipEditSheet)
        {
            _shipEditSheet = shipEditSheet;
        }
        
        #region Binding Prop: InventoryItem

        /// <summary>
        /// InventoryItem
        /// </summary>
        [Binding]
        public InventoryItem InventoryItem
        {
            get => _inventoryItem;
            set
            {
                if (Equals(_inventoryItem, value))
                {
                    return;
                }

                _inventoryItem = value;
                OnPropertyChanged(nameof(InventoryItem));
                OnPropertyChanged(nameof(IsEquipped));
                OnPropertyChanged(nameof(Thumbnail));
            }
        }

        private InventoryItem _inventoryItem;

        #endregion

        #region Binding Prop: IsEmpty

        /// <summary>
        /// IsEquipped
        /// </summary>
        [Binding]
        public bool IsEquipped => _inventoryItem != null;

        [Binding]
        public Sprite Thumbnail => IsEquipped ? _inventoryItem.Thumbnail : Resources.Load<Sprite>("Images/Group 248");

        #endregion

        [Binding]
        public void RemoveEquipment()
        {
            IOC.Resolve<InventorySheet>().RemoveIgnore(_inventoryItem);
            InventoryItem = null;
            
            _shipEditSheet.SaveSetupProfile();
        }
    }

    [Binding]
    public class NewShipEditSheet : SheetWithViewModel
    {
        public RectTransform ConfigSheet;
        public RectTransform ShipSpawnPoint;
        public RectTransform InventorySheet;
        public ShipConfigManager ShipConfigManager;
        
        // [Binding]
        // public ObservableList<ShipItem> ShipItems { get; } = new();
        
        [Binding]
        public ObservableList<StashItem> StashItems { get; } = new();

        // [Binding]
        // public Dictionary<Vector2Int, InventoryItem> xItemPositions { get; set; } = new();

        #region Binding Prop: ItemPositions

        /// <summary>
        /// ItemPositions
        /// </summary>
        [Binding]
        public Dictionary<Vector2Int, InventoryItem> ItemPositions
        {
            get => _itemPositions;
            set
            {
                if (Equals(_itemPositions, value))
                {
                    return;
                }

                _itemPositions = value;
                OnPropertyChanged(nameof(ItemPositions));
                
                SaveSetupProfile();
            }
        }

        private Dictionary<Vector2Int, InventoryItem> _itemPositions;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < 10; i++)
            {
                StashItems.Add(new StashItem(this));
            }
            
            IOC.Register(this);

            ShipSetupProfileIndex = (int)SaveSystem.GameSave.ShipSetupSaveData.CurrentProfile;
        }

        #region Binding Prop: ShipId

        /// <summary>
        /// ShipId
        /// </summary>
        [Binding]
        public string ShipId
        {
            get => _shipId;
            set
            {
                if (Equals(_shipId, value))
                {
                    return;
                }

                _shipId = value;
                OnPropertyChanged(nameof(ShipId));
                _shipSetupProfileIndex = (int)SetupProfile.Profile1;
                OnPropertyChanged(nameof(ShipSetupProfileIndex));
            }
        }

        private string _shipId;

        #endregion

        #region Binding Prop: ShipSetupProfileIndex

        /// <summary>
        /// ShipSetupProfileIndex
        /// </summary>
        [Binding]
        public int ShipSetupProfileIndex
        {
            get => _shipSetupProfileIndex;
            set
            {
                if (Equals(_shipSetupProfileIndex, value))
                {
                    return;
                }

                _shipSetupProfileIndex = value;
                OnPropertyChanged(nameof(ShipSetupProfileIndex));
                
                LoadShipSetup(ShipId, (SetupProfile)_shipSetupProfileIndex);
            }
        }

        private int _shipSetupProfileIndex = (int)SetupProfile.None;

        #endregion

        public SetupProfile ShipSetupProfile => (SetupProfile)ShipSetupProfileIndex;

        public void LoadShipSetup(string shipId, SetupProfile profile)
        {
            foreach (var stashItem in StashItems)
            {
                // stashItem.RemoveEquipment();
                stashItem.InventoryItem = null;
            }

            var inventorySheet = IOC.Resolve<InventorySheet>();
            inventorySheet.ClearIgnoredItems();
            
            // load data
            var shipSetupData = SaveSystem.GameSave.ShipSetupSaveData.GetShipSetup(shipId, profile);
            foreach (var (pos, itemData) in shipSetupData.StashData)
            {
                if (itemData == null) continue;
                var inventoryItem = new InventoryItem();
                if (itemData.ItemType == ItemType.CREW)
                {
                    var rec = GameData.CrewTable.FindById(itemData.ItemId);
                    if (rec == null) continue;

                    inventoryItem.Type = ItemType.CREW;
                    inventoryItem.BackedData = rec;
                    inventoryItem.Id = rec.Id;
                    inventoryItem.Rarity = rec.Rarity;
                    inventoryItem.OperationType = rec.OperationType;
                    inventoryItem.Name = rec.Name;
                    inventoryItem.Shape = rec.Shape;
                }
                
                else if (itemData.ItemType == ItemType.CANNON)
                {
                    var rec = GameData.CannonTable.FindById(itemData.ItemId);
                    if (rec == null) continue;
                    
                    inventoryItem.Type = ItemType.CANNON;
                    inventoryItem.BackedData = rec;
                    inventoryItem.Id = rec.Id;
                    inventoryItem.Rarity = rec.Rarity;
                    inventoryItem.OperationType = rec.OperationType;
                    inventoryItem.Name = rec.Name;
                    inventoryItem.Shape = rec.Shape;
                }
                
                else if (itemData.ItemType == ItemType.AMMO)
                {
                    var rec = GameData.AmmoTable.FindById(itemData.ItemId);
                    if (rec == null) continue;
                    
                    inventoryItem.Type = ItemType.AMMO;
                    inventoryItem.BackedData = rec;
                    inventoryItem.Id = rec.Id;
                    inventoryItem.Rarity = rec.Rarity;
                    inventoryItem.OperationType = rec.OperationType;
                    inventoryItem.Name = rec.Name;
                    inventoryItem.Shape = rec.Shape;
                }
                
                StashItems[pos].InventoryItem = inventoryItem;
                inventorySheet.AddIgnore(inventoryItem);
            }
            
            foreach (var (pos, itemData) in shipSetupData.ShipData)
            {
                if (itemData == null) continue;
                var inventoryItem = new InventoryItem();
                if (itemData.ItemType == ItemType.CREW)
                {
                    var rec = GameData.CrewTable.FindById(itemData.ItemId);
                    if (rec == null) continue;
                    
                    inventoryItem.Type = ItemType.CREW;
                    inventoryItem.BackedData = rec;
                    inventoryItem.Id = rec.Id;
                    inventoryItem.Rarity = rec.Rarity;
                    inventoryItem.OperationType = rec.OperationType;
                    inventoryItem.Name = rec.Name;
                    inventoryItem.Shape = rec.Shape;
                }
                
                else if (itemData.ItemType == ItemType.CANNON)
                {
                    var rec = GameData.CannonTable.FindById(itemData.ItemId);
                    if (rec == null) continue;
                    
                    inventoryItem.Type = ItemType.CANNON;
                    inventoryItem.BackedData = rec;
                    inventoryItem.Id = rec.Id;
                    inventoryItem.Rarity = rec.Rarity;
                    inventoryItem.OperationType = rec.OperationType;
                    inventoryItem.Name = rec.Name;
                    inventoryItem.Shape = rec.Shape;
                }
                
                else if (itemData.ItemType == ItemType.AMMO)
                {
                    var rec = GameData.AmmoTable.FindById(itemData.ItemId);
                    if (rec == null) continue;
                    
                    inventoryItem.Type = ItemType.AMMO;
                    inventoryItem.BackedData = rec;
                    inventoryItem.Id = rec.Id;
                    inventoryItem.Rarity = rec.Rarity;
                    inventoryItem.OperationType = rec.OperationType;
                    inventoryItem.Name = rec.Name;
                    inventoryItem.Shape = rec.Shape;
                }
                
                ItemPositions[pos] = inventoryItem;
                inventorySheet.AddIgnore(inventoryItem);
            }
        }

        public void SaveSetupProfile()
        {
            // stash
            for (var i = 0; i < StashItems.Count; i++)
            {
                var stash = StashItems[i];

                SaveSystem.GameSave.ShipSetupSaveData.GetShipSetup(ShipId, ShipSetupProfile).StashData[i] 
                    = stash.InventoryItem == null ? null : new ItemData()
                    {
                        ItemId = stash.InventoryItem.Id,
                        ItemType = stash.InventoryItem.Type,
                    };
            }
            
            // ship
            // UnityWeld.Binding.BoundObservableList<>
            SaveSystem.SaveGame();

        }

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

        public ShipSetupItem CreateDragItem(InventoryItem item)
        {
            var prefab = GetDragItemPrefab(item);
            var instance = Instantiate(prefab, InventorySheetDragPane);

            return instance;
        }

        

        public ShipSetupItem GetDragItemPrefab(InventoryItem item)
        {
            var shapePath = $"SetupItems/SetupItem_{item.Type.ToString().ToLower()}_{item.OperationType}";
            var prefab = Resources.Load<ShipSetupItem>(shapePath);
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

            ShipId = SaveSystem.GameSave.ShipSetupSaveData.CurrentShipId;
            InitializeShip(ShipId);
            SetViewMode_Normal();
            return UniTask.CompletedTask;
        }

        void InitializeShip(string shipID)
        {
            var shipPrefabs = Resources.Load<GameObject>($"Ships/Ship_{shipID}");
            var ship = Instantiate(shipPrefabs, ShipSpawnPoint);

            ship.GetComponentsInChildren<SlotCell>();

            // ShipConfigManager.Grid = ship.GetComponentInChildren<SlotGrid>();
            // ShipConfigManager.PlacementPane = ship.GetComponentInChildren<PlacementPane>().transform;

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