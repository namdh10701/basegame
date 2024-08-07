using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using _Game.Features.Inventory;
using _Game.Features.Inventory.Core;
using _Game.Features.MyShip.GridSystem;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using InventoryItem = _Game.Features.Inventory.InventoryItem;

namespace _Game.Features.MyShip
{
    public enum ViewMode
    {
        VIEW,
        CONFIG_NORMAL,
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

        private Dictionary<Vector2Int, InventoryItem> _itemPositions = new();

        #endregion

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
                SaveSetupProfile();
            }
        }

        private int _shipSetupProfileIndex = (int)SetupProfile.Profile1;

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
            inventorySheet?.ClearIgnoredItems();
            
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
                
                StashItems[int.Parse(pos)].InventoryItem = inventoryItem;
                inventorySheet.AddIgnore(inventoryItem);
            }
            
            ItemPositions.Clear();
            foreach (var (rawPos, itemData) in shipSetupData.ShipData)
            {
                var rawPosParts = rawPos.Split(',');
                var pos = new Vector2Int(int.Parse(rawPosParts[0]), int.Parse(rawPosParts[1]));
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

            GetComponentInChildren<ShipConfigManager>().ClearPlacement();

            HashSet<string> ignoreItemId = new();
            Dictionary<string, Tuple<List<Vector2Int>, InventoryItem>> tmp = new(); 
            foreach (var (pos, inventoryItem) in ItemPositions)
            {
                if (!tmp.ContainsKey(inventoryItem.Type + inventoryItem.Id))
                {
                    tmp[inventoryItem.Type + inventoryItem.Id] = new(new List<Vector2Int>(), inventoryItem);
                }
                
                tmp[inventoryItem.Type + inventoryItem.Id].Item1.Add(pos);
            }
            
            foreach (var (itemId, slots) in tmp)
            {
                // draw UI
                GetComponentInChildren<ShipConfigManager>().PlaceInventoryItem(slots.Item2, FindTopLeftPoint(slots.Item1));
            }

            UpdateRemovable();
        }

        public void SaveSetupProfile()
        {
            var setup = SaveSystem.GameSave.ShipSetupSaveData.GetShipSetup(ShipId, ShipSetupProfile);
            SaveSystem.GameSave.ShipSetupSaveData.CurrentShip.ItemId = ShipId;
            SaveSystem.GameSave.ShipSetupSaveData.CurrentProfile = ShipSetupProfile;
            
            // stash
            for (var i = 0; i < StashItems.Count; i++)
            {
                var stash = StashItems[i];

                setup.StashData[i + ""] 
                    = stash.InventoryItem == null ? null : new ItemData()
                    {
                        ItemId = stash.InventoryItem.Id,
                        ItemType = stash.InventoryItem.Type,
                    };
            }
            

            // ship
            setup.ShipData.Clear();
            foreach (var (pos, item) in ItemPositions)
            {
                
                setup.ShipData[$"{pos.x},{pos.y}"] = new ItemData()
                {
                    ItemId = item.Id,
                    ItemType = item.Type,
                };
            }
            SaveSystem.SaveGame();

        }

        #region Binding Prop: IsViewMode

        /// <summary>
        /// IsViewMode
        /// </summary>
        [Binding]
        public bool IsViewMode
        {
            get => _isViewMode;
            set
            {
                if (Equals(_isViewMode, value))
                {
                    return;
                }

                _isViewMode = value;
                OnPropertyChanged(nameof(IsViewMode));
            }
        }

        private bool _isViewMode;

        #endregion

        #region ViewMode
        public ViewMode _viewMode = ViewMode.CONFIG_NORMAL;
        
        private void SetViewMode(ViewMode viewMode)
        {
            _viewMode = viewMode;

            if (viewMode == ViewMode.CONFIG_NORMAL)
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

            IsViewMode = viewMode == ViewMode.VIEW;

            OnPropertyChanged(nameof(IsConfigMode));
            OnPropertyChanged(nameof(CanSwitchStashShip));
            OnPropertyChanged(nameof(ShouldShowProfile));
            
            UpdateRemovable();
        }

        private void UpdateRemovable()
        {
            var placementPane = GetComponentInChildren<PlacementPane>(false).transform;
            foreach (var shipItem in placementPane.GetComponentsInChildren<ShipSetupItem>(false))
            {
                shipItem.Removable = !IsViewMode;
            }
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
        public bool IsConfigMode => _viewMode != ViewMode.CONFIG_NORMAL;
        
        [Binding]
        public bool ShouldShowProfile => _viewMode == ViewMode.CONFIG_NORMAL || IsViewMode;
        
        [Binding]
        public bool CanSwitchStashShip => _viewMode == ViewMode.CONFIG_STASH || _viewMode == ViewMode.CONFIG_SHIP;
        
        
        [Binding]
        public void SetViewMode_View()
        {
            SetViewMode(ViewMode.VIEW);
        }
        
        [Binding]
        public void SetViewMode_Normal()
        {
            SetViewMode(ViewMode.CONFIG_NORMAL);
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
        
        public override async UniTask Initialize(Memory<object> args)
        {
            IOC.Register(this);
            for (int i = 0; i < 10; i++)
            {
                StashItems.Add(new StashItem(this));
            }

            _shipId = SaveSystem.GameSave.ShipSetupSaveData.CurrentShip.ItemId;
            _shipSetupProfileIndex = (int)SaveSystem.GameSave.ShipSetupSaveData.CurrentProfile;
            OnPropertyChanged(nameof(ShipSetupProfileIndex));
            InitializeShip(ShipId);
            
            OnPropertyChanged(nameof(ShipPageInfo));

            SetViewMode_View();
        }

        [Binding]
        public async void NextShip()
        {
            var currentShipIdx = SaveSystem.GameSave.OwnedShips.IndexOf(SaveSystem.GameSave.ShipSetupSaveData.CurrentShip);
            var nextShipIdx = Math.Min(SaveSystem.GameSave.OwnedShips.Count - 1, currentShipIdx + 1);
            _shipId = SaveSystem.GameSave.OwnedShips[nextShipIdx].ItemId;
            _shipSetupProfileIndex = 0;
            OnPropertyChanged(nameof(ShipSetupProfileIndex));
            SaveSystem.GameSave.ShipSetupSaveData.CurrentProfile = (SetupProfile)_shipSetupProfileIndex;
            SaveSystem.GameSave.ShipSetupSaveData.CurrentShip.ItemId = _shipId;
            
            InitializeShip(_shipId);
            
            await UniTask.NextFrame();
            LoadShipSetup(ShipId, (SetupProfile)_shipSetupProfileIndex);

            OnPropertyChanged(nameof(ShipPageInfo));
        }

        [Binding]
        public async void PrevShip()
        {
            var currentShipIdx = SaveSystem.GameSave.OwnedShips.IndexOf(SaveSystem.GameSave.ShipSetupSaveData.CurrentShip);
            var nextShipIdx = Math.Max(0, currentShipIdx - 1);
            _shipId = SaveSystem.GameSave.OwnedShips[nextShipIdx].ItemId;
            _shipSetupProfileIndex = 0;
            OnPropertyChanged(nameof(ShipSetupProfileIndex));
            SaveSystem.GameSave.ShipSetupSaveData.CurrentProfile = (SetupProfile)_shipSetupProfileIndex;
            SaveSystem.GameSave.ShipSetupSaveData.CurrentShip.ItemId = _shipId;
            
            InitializeShip(_shipId);
            
            await UniTask.NextFrame();
            LoadShipSetup(ShipId, (SetupProfile)_shipSetupProfileIndex);
            
            OnPropertyChanged(nameof(ShipPageInfo));
        }

        [Binding] public string ShipPageInfo 
            => $"{(SaveSystem.GameSave.OwnedShips.IndexOf(SaveSystem.GameSave.ShipSetupSaveData.CurrentShip)+1).ToString().PadLeft(2, '0')}/{SaveSystem.GameSave.OwnedShips.Count.ToString().PadLeft(2, '0')}";

        public override async void DidEnter(Memory<object> args)
        {
            // wait for grid initialized
            await UniTask.NextFrame();
            LoadShipSetup(ShipId, (SetupProfile)_shipSetupProfileIndex);
        }

        private InventoryManager inventoryManager;
        private GameObject ship;

        void InitializeShip(string shipID)
        {
            DestroyCurrentShip();
            
            var shipPrefabs = Resources.Load<GameObject>($"Ships/Ship_{shipID}");
            ship = Instantiate(shipPrefabs, ShipSpawnPoint);
        }

        void DestroyCurrentShip()
        {
            foreach (Transform child in ShipSpawnPoint)
            {
                Destroy(child.gameObject);
            }
        }
        
        public class InputData<T>
        {
            public T Value { get; }

            protected InputData(T data)
            {
                Value = data;
            }
        }
        
        public static Vector2Int FindBottomLeftPoint(List<Vector2Int> points)
        {
            if (points == null || points.Count == 0)
            {
                Debug.LogError("The list of points is null or empty.");
                return Vector2Int.zero;
            }

            Vector2Int bottomLeft = points[0];

            foreach (var point in points)
            {
                if (point.y < bottomLeft.y || (point.y == bottomLeft.y && point.x < bottomLeft.x))
                {
                    bottomLeft = point;
                }
            }

            return bottomLeft;
        }
        
        public static Vector2Int FindTopLeftPoint(List<Vector2Int> points)
        {
            if (points == null || points.Count == 0)
            {
                Debug.LogError("The list of points is null or empty.");
                return Vector2Int.zero;
            }

            Vector2Int topLeft = points[0];

            foreach (var point in points)
            {
                if (point.y > topLeft.y || (point.y == topLeft.y && point.x < topLeft.x))
                {
                    topLeft = point;
                }
            }

            return topLeft;
        }

        ///
        ///
        ///
        ///
        ///
        public void Stash_SwapSlot(int posA, int posB)
        {
            (StashItems[posA].InventoryItem, StashItems[posB].InventoryItem) = (StashItems[posB].InventoryItem, StashItems[posA].InventoryItem);
        }

        public void Stash_SetSlot(int pos, InventoryItem data)
        {
            StashItems[pos].InventoryItem = data;
        }
        
        public void Stash_RemoveSlot(int pos, InventoryItem data)
        {
            StashItems[pos].InventoryItem = null;
        }
        
        

        public void Ship_SetSlot(Vector2Int pos, InventoryItem inventoryItem)
        {
            var gridLayoutGroup = GetComponentInChildren<SlotGrid>().GetComponent<GridLayoutGroup>();
            var placementPane = GetComponentInChildren<PlacementPane>(false).transform;
            
            foreach (var shipItem in placementPane.GetComponentsInChildren<ShipSetupItem>(false))
            {
                if (shipItem.InventoryItem == inventoryItem)
                {
                    Destroy(shipItem.gameObject);
                }
            }
            
            // Remove backed data
            var pairs = ItemPositions.Where(v => v.Value == inventoryItem).ToList();
            for (var i = 0; i < pairs.Count; i++)
            {
                ItemPositions.Remove(pairs[i].Key);
            }
            
            var shipSetupItemPrefab = ShipSetupUtils.GetShipSetupItemPrefab(inventoryItem);
            var shipSetupItem = Instantiate(shipSetupItemPrefab, placementPane);
            shipSetupItem.Removable = true;
            shipSetupItem.InventoryItem = inventoryItem;
            shipSetupItem.Pos = pos;
            
            var uiCell = GridLayoutGroupUtils.GetCellAtPosition(gridLayoutGroup, pos);
            var rect = shipSetupItem.transform as RectTransform;
            rect.anchorMin = Vector2.up;
            rect.anchorMax = Vector2.up;
            rect.pivot = Vector2.zero;
            rect.anchoredPosition = uiCell.anchoredPosition;
            
            ItemPositions[pos] = inventoryItem;
            
            SaveSetupProfile();
            
            // hide from inventory sheet
            IOC.Resolve<InventorySheet>().AddIgnore(inventoryItem);
        }

        public void Ship_RemoveItem(InventoryItem item)
        {
            var placementPane = GetComponentInChildren<PlacementPane>(false).transform;
            foreach (var shipItem in placementPane.GetComponentsInChildren<ShipSetupItem>(false))
            {
                if (shipItem.InventoryItem == item)
                {
                    Destroy(shipItem.gameObject);
                }
            }
            
            // Remove backed data
            var pairs = ItemPositions.Where(v => v.Value == item).ToList();
            for (var i = 0; i < pairs.Count; i++)
            {
                ItemPositions.Remove(pairs[i].Key);
            }
            
            // back to stash
            StashItem firstEmptyStashItem = null;
            foreach (var stashItem in StashItems)
            {
                if (stashItem.InventoryItem == null)
                {
                    firstEmptyStashItem = stashItem;
                    break;
                }
            }

            if (firstEmptyStashItem == null)
            {
                return;
            }
            
            firstEmptyStashItem.InventoryItem = item;
            
            SaveSetupProfile();
        }
    }
}