using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Scripts.Bootstrap;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Online;
using Unity.VisualScripting;
using UnityEngine;
using UnityWeld.Binding;
namespace _Game.Features.MergeScreen
{
    [Binding]
    public class MergeScreenViewModel : ScreenWithViewModel
    {
        #region Binding Prop: IsActiveSuccesFul
        /// <summary>
        /// IsActiveSuccesFul
        /// </summary>
        [Binding]
        public bool IsActiveSuccesFul
        {
            get => _isActiveSuccesFul
            ;
            set
            {
                if (Equals(_isActiveSuccesFul, value))
                {
                    return;
                }
                _isActiveSuccesFul = value;
                OnPropertyChanged(nameof(IsActiveSuccesFul));
            }
        }
        private bool _isActiveSuccesFul;
        #endregion

        #region Binding Prop: IsActiveItemMerge
        /// <summary>
        /// IsActiveItemMerge
        /// </summary>
        [Binding]
        public bool IsActiveItemMerge
        {
            get => _isActiveItemMerge
            ;
            set
            {
                if (Equals(_isActiveItemMerge, value))
                {
                    return;
                }
                _isActiveItemMerge = value;
                OnPropertyChanged(nameof(IsActiveItemMerge));
            }
        }
        private bool _isActiveItemMerge;
        #endregion

        #region Binding Prop: CanMerge
        /// <summary>
        /// CanMerge
        /// </summary>
        [Binding]
        public bool CanMerge
        {
            get => _canMerge
            ;
            set
            {
                if (Equals(_canMerge, value))
                {
                    return;
                }
                _canMerge = value;
                OnPropertyChanged(nameof(CanMerge));
            }
        }
        private bool _canMerge;
        #endregion

        #region Binding Prop: NumberItems
        /// <summary>
        /// NumberItems
        /// </summary>
        [Binding]
        public int NumberItems
        {
            get => _numberItems
            ;
            set
            {
                if (Equals(_numberItems, value))
                {
                    return;
                }
                _numberItems = value;
                OnPropertyChanged(nameof(NumberItems));
            }
        }
        private int _numberItems;
        #endregion

        #region Binding Prop: NumberItemRequired
        /// <summary>
        /// NumberItem
        /// </summary>
        [Binding]
        public int NumberItemsRequired
        {
            get => _numberItemsRequired
            ;
            set
            {
                if (Equals(_numberItemsRequired, value))
                {
                    return;
                }
                _numberItemsRequired = value;
                OnPropertyChanged(nameof(NumberItemsRequired));
            }
        }
        private int _numberItemsRequired = 3;
        #endregion

        #region Binding Prop: SpriteItemMerge
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite SpriteItemMerge
        {
            get
            {
                if (ItemMerge == null)
                    return Resources.Load<Sprite>($"Images/Items/item_ammo_arrow_common");
                else
                {
                    if (ItemMerge.Type != ItemType.MISC)
                    {
                        var itemType = ItemMerge.Type.ToString().ToLower();
                        var itemOperationType = ItemMerge.OperationType.ToLower();
                        var itemRarity = ItemMerge.Rarity.ToString().ToLower();
                        var path = $"Images/Items/item_{itemType}_{itemOperationType}_{itemRarity}";
                        return Resources.Load<Sprite>(path);
                    }
                    else
                    {
                        var path = ItemMerge.Id == null ? $"Images/Items/item_ammo_arrow_common" : $"Images/Items/item_misc_{ItemMerge.Id.ToString().ToLower()}";
                        return Resources.Load<Sprite>(path);
                    }
                }

            }
        }
        #endregion

        [Binding]
        public string IdItemTarget { get; set; }

        [Binding]
        public ItemType TypeItemTarget { get; set; }

        #region Binding Prop: SpriteItemMerge
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite SpriteItemTarget
        {
            get
            {
                if (ItemTarget == null)
                    return Resources.Load<Sprite>($"Images/Items/item_ammo_arrow_common");
                else
                {
                    if (ItemTarget.Type != ItemType.MISC)
                    {
                        var itemType = ItemTarget.Type.ToString().ToLower();
                        var itemOperationType = ItemTarget.OperationType.ToLower();
                        var itemRarity = ItemTarget.Rarity.ToString().ToLower();
                        var path = $"Images/Items/item_{itemType}_{itemOperationType}_{itemRarity}";
                        return Resources.Load<Sprite>(path);
                    }
                    else
                    {
                        var path = ItemTarget.Id == null ? $"Images/Items/item_ammo_arrow_common" : $"Images/Items/item_misc_{ItemMerge.Id.ToString().ToLower()}";
                        return Resources.Load<Sprite>(path);
                    }
                }

            }
        }
        #endregion

        #region Binding Prop: ItemPerPage
        /// <summary>
        /// ItemPerPage
        /// </summary>
        [Binding]
        public int ItemPerPage
        {
            get => _itemPerPage;
            set
            {
                if (Equals(_itemPerPage, value))
                {
                    return;
                }

                _itemPerPage = value;
                OnPropertyChanged(nameof(ItemPerPage));
                DoFilter();
            }
        }
        private int _itemPerPage = 0;
        #endregion

        #region Binding Prop: Page
        /// <summary>
        /// Page
        /// </summary>
        [Binding]
        public int Page
        {
            get => _page;
            set
            {
                if (Equals(_page, value))
                {
                    return;
                }

                _page = value;
                OnPropertyChanged(nameof(Page));
                OnPropertyChanged(nameof(PageInfo));
                OnPropertyChanged(nameof(IsPrevPageAvailable));
                OnPropertyChanged(nameof(IsNextPageAvailable));
                DoFilter();
            }
        }
        private int _page;
        #endregion

        [Binding]
        public int MaxPage => Math.Max(0, (int)Math.Ceiling(1f * TotalItemCount / ItemPerPage) - 1);

        [Binding]
        public string PageInfo => $"{(Page + 1).ToString().PadLeft(2, '0')}/{(MaxPage + 1).ToString().PadLeft(2, '0')}";

        [Binding]
        public bool IsPrevPageAvailable => Page > 0 && MaxPage > 1;

        [Binding]
        public bool IsNextPageAvailable => Page < MaxPage;

        #region Binding Prop: TotalItemCount
        /// <summary>
        /// TotalItemCount
        /// </summary>
        [Binding]
        public int TotalItemCount
        {
            get => _totalItemCount;
            set
            {
                if (Equals(_totalItemCount, value))
                {
                    return;
                }

                _totalItemCount = value;
                OnPropertyChanged(nameof(TotalItemCount));
                OnPropertyChanged(nameof(MaxPage));
                OnPropertyChanged(nameof(PageInfo));
            }
        }
        private int _totalItemCount;
        #endregion

        #region Binding Prop: FilterItemTypeIndex
        /// <summary>
        /// FilterItemTypeIndex
        /// </summary>
        [Binding]
        public int FilterItemTypeIndex
        {
            get => _filterItemTypeIndex;
            set
            {
                if (_filterItemTypeIndex == value)
                {
                    return;
                }

                _filterItemTypeIndex = value;

                OnPropertyChanged(nameof(FilterItemTypeIndex));
                if (ItemMerge != null && ItemTarget != null)
                {
                    _itemsSelected.Clear();
                    ItemMerge = null;
                    ItemTarget = null;
                }
                DoFilter(true);
            }
        }
        private int _filterItemTypeIndex = 0;
        #endregion

        #region Binding Prop: LevelItemMerge
        /// <summary>
        /// LevelItemMerge
        /// </summary>
        [Binding]
        public int LevelItemMerge
        {
            get => _levelItemMerge;
            set
            {
                if (_levelItemMerge == value)
                {
                    return;
                }

                _levelItemMerge = value;

                OnPropertyChanged(nameof(LevelItemMerge));

                DoFilter();
            }
        }
        private int _levelItemMerge;
        #endregion

        #region Binding Prop: SlotItemMerge
        /// <summary>
        /// SlotItemMerge
        /// </summary>
        [Binding]
        public string SlotItemMerge
        {
            get => _slotItemMerge;
            set
            {
                if (_slotItemMerge == value)
                {
                    return;
                }

                _slotItemMerge = value;

                OnPropertyChanged(nameof(SlotItemMerge));

                DoFilter();
            }
        }
        private string _slotItemMerge;
        #endregion

        #region Binding Prop: PreviousRarity
        /// <summary>
        /// PreviousRarity
        /// </summary>
        [Binding]
        public string PreviousRarity
        {
            get => _previousRarity;
            set
            {
                if (_previousRarity == value)
                {
                    return;
                }

                _previousRarity = value;

                OnPropertyChanged(nameof(PreviousRarity));

                DoFilter();
            }
        }
        private string _previousRarity;
        #endregion

        #region Binding Prop: NextRarity
        /// <summary>
        /// NextRarity
        /// </summary>
        [Binding]
        public string NextRarity
        {
            get => _nextRarity;
            set
            {
                if (_nextRarity == value)
                {
                    return;
                }

                _nextRarity = value;

                OnPropertyChanged(nameof(NextRarity));

                DoFilter();
            }
        }
        private string _nextRarity;
        #endregion

        #region Binding Prop: ColorNextRarity
        /// <summary>
        /// ColorNextRarity
        /// </summary>
        [Binding]
        public Color ColorNextRarity
        {
            get => _colorNextRarity;
            set
            {
                if (Equals(_colorNextRarity, value))
                {
                    return;
                }

                _colorNextRarity = value;
                OnPropertyChanged(nameof(ColorNextRarity));
            }
        }
        private Color _colorNextRarity;
        #endregion

        #region Binding Prop: ColorPreviousRarity
        /// <summary>
        /// ColorPreviousRarity
        /// </summary>
        [Binding]
        public Color ColorPreviousRarity
        {
            get => _colorPreviousRarity;
            set
            {
                if (Equals(_colorPreviousRarity, value))
                {
                    return;
                }

                _colorPreviousRarity = value;
                OnPropertyChanged(nameof(ColorPreviousRarity));
            }
        }
        private Color _colorPreviousRarity;
        #endregion

        private List<InventoryItem> _dataSource = new List<InventoryItem>();
        private List<string> _itemsSelected = new List<string>();

        #region Binding: InventoryItems
        private ObservableList<InventoryItem> items = new ObservableList<InventoryItem>();
        [Binding]
        public ObservableList<InventoryItem> Items => items;
        #endregion

        public List<string> IgnoreIdList = new List<string>();

        [Binding]
        public ObservableList<InventoryItem> IgnoreItems { get; set; } = new();

        public InventoryItem ItemMerge;

        #region Binding: StarsItemMerge
        private ObservableList<Star> starsItemMerge = new ObservableList<Star>();
        [Binding]
        public ObservableList<Star> StarsItemMerge => starsItemMerge;
        #endregion

        public InventoryItem ItemTarget;
        #region Binding: StarsItemMerge
        private ObservableList<Star> starsItemTarget = new ObservableList<Star>();
        [Binding]
        public ObservableList<Star> StarsItemTarget => starsItemTarget;
        #endregion

        public RectTransform HightlightPopupRecieved;

        void OnEnable()
        {
            LoadData();
        }

        private void LoadData()
        {
            InitializeInternal();
            IgnoreItems.CollectionChanged += (sender, args) =>
           {
               DoFilter();
           };
        }

        protected void InitializeInternal()
        {
            foreach (var item in SaveSystem.GameSave.OwnedItems)
            {
                InventoryItem inventoryItem = null;

                // cannon
                if (item.ItemType == ItemType.CANNON)
                {
                    var info = GameData.CannonTable.FindById(item.ItemId);
                    if (info.Enable)
                    {
                        inventoryItem = new InventoryItem
                        {
                            Type = ItemType.CANNON,
                            Id = info.Id,
                            OwnItemId = item.OwnItemId,
                            Name = info.Name,
                            Rarity = info.Rarity,

                            RarityLevel = item.RarityLevel.ToString(),
                            Level = item.Level,

                            OperationType = info.OperationType,
                            Shape = info.Shape,
                            Slot = info.Slot
                        };
                        inventoryItem.LoadStarsItem();
                    }
                }

                // ammo
                else if (item.ItemType == ItemType.AMMO)
                {
                    var info = GameData.AmmoTable.FindById(item.ItemId);
                    if (info.Enable)
                    {
                        inventoryItem = new InventoryItem
                        {
                            Type = ItemType.AMMO,
                            Id = info.Id,
                            Name = info.Name,
                            OwnItemId = item.OwnItemId,
                            Rarity = info.Rarity,
                            RarityLevel = info.RarityLevel.ToString(),

                            Level = item.Level,

                            OperationType = info.OperationType,
                            Shape = info.Shape,
                            Slot = info.Slot,
                        };
                    }
                }

                if (inventoryItem == null) continue;

                _dataSource.Add(inventoryItem);
            }

            foreach (var inventoryItem in _dataSource)
            {
                inventoryItem.SelectionStateChanged += OnSelectionStateChanged;
            }

            DoFilter();
        }

        private void DoFilter(bool clearSelection = false)
        {
            if (clearSelection)
            {
                foreach (var inventoryItem in _dataSource)
                {
                    inventoryItem.IsSelected = false;
                }
            }

            var itemType = _filterItemTypeIndex == 0 ? ItemType.CANNON : ItemType.AMMO;
            Items.Clear();

            var itemList = _dataSource
                .Where(v =>
                    v.Type == itemType
                    && IgnoreIdList.All(ignoredKey => ignoredKey != (v.Type + v.Id))
                    && !IgnoreItems.Contains(v)
                )
                .ToList();
            var pageItemList = itemList;

            if (ItemPerPage > 0)
            {
                pageItemList = pageItemList.Skip(Page * ItemPerPage).Take(ItemPerPage).ToList();
            }

            TotalItemCount = itemList.Count();

            Items.AddRange(pageItemList);


            OnPropertyChanged(nameof(Page));
            OnPropertyChanged(nameof(IsPrevPageAvailable));
            OnPropertyChanged(nameof(IsNextPageAvailable));
        }


        private void OnSelectionStateChanged(InventoryItem item)
        {
            if (item.IsSelected)
            {
                ItemMerge = item;
                if (_itemsSelected.Count < NumberItemsRequired)
                    _itemsSelected.Add(item.OwnItemId);
                else
                {
                    _itemsSelected.RemoveAt(0);
                    _itemsSelected.Add(item.OwnItemId);
                }
                NumberItems++;
                OnPropertyChanged(nameof(SpriteItemMerge));
                SlotItemMerge = ItemMerge.Slot;
                LoadStarsItem(ItemMerge, StarsItemMerge);
                DoFilterItemMerge(item);

                if (ItemTarget == null)
                {
                    LevelItemMerge = ItemMerge.Level;
                    LoadDataItemTarget();
                    LoadStarsItem(ItemTarget, StarsItemTarget);
                }

            }
            else
            {
                NumberItems--;
                item.IsSelected = false;
                _itemsSelected.Remove(item.OwnItemId);
                if (NumberItems == 0)
                {
                    DoFilter();
                    ItemTarget = null;
                }
            }
            IsActiveItemMerge = _itemsSelected.Count > 0;
            CanMerge = NumberItems == NumberItemsRequired ? true : false;
        }

        private void LoadStarsItem(InventoryItem item, ObservableList<Star> stars)
        {
            if (item.Type == ItemType.CREW || item.Type == ItemType.MISC) return;

            for (int i = 0; i < int.Parse(item.RarityLevel); i++)
            {
                stars.Add(new Star());
            }
        }

        private void DoFilterItemMerge(InventoryItem item)
        {
            Items.Clear();

            var itemList = _dataSource
                .Where(v =>
                    v.Id == item.Id && v.RarityLevel == item.RarityLevel
                    && IgnoreIdList.All(ignoredKey => ignoredKey != (v.Type + v.Id))
                    && !IgnoreItems.Contains(v)
                )
                .ToList();
            var pageItemList = itemList;
            Items.AddRange(pageItemList);
        }

        private DataTableRecord GetNextRarityItem(InventoryItem inventoryItem)
        {
            switch (inventoryItem.Type)
            {
                case ItemType.CANNON:
                    return GameData.CannonTable.GetNextTableRecord(inventoryItem.Rarity, inventoryItem.OperationType, inventoryItem.RarityLevel);
                case ItemType.AMMO:
                    return GameData.AmmoTable.GetNextTableRecord(inventoryItem.Rarity, inventoryItem.Id);

            }
            return null;
        }

        private void LoadDataItemTarget()
        {
            var record = GetNextRarityItem(ItemMerge);
            ItemTarget = new InventoryItem();

            switch (ItemMerge.Type)
            {
                case ItemType.CANNON:
                    var cannonRecord = record as CannonTableRecord;

                    ItemTarget.Id = cannonRecord.Id;
                    ItemTarget.OwnItemId = Game.IDGenerator.Next();
                    ItemTarget.Name = cannonRecord.Name;
                    ItemTarget.Type = ItemMerge.Type;
                    ItemTarget.OperationType = cannonRecord.OperationType;
                    ItemTarget.Rarity = cannonRecord.Rarity;
                    ItemTarget.RarityLevel = cannonRecord.RarityLevel.ToString();
                    ItemTarget.Shape = cannonRecord.Shape;
                    ItemTarget.Slot = cannonRecord.Slot;
                    break;
                case ItemType.AMMO:
                    var ammoRecord = record as AmmoTableRecord;

                    ItemTarget.Id = ammoRecord.Id;
                    ItemTarget.OwnItemId = Game.IDGenerator.Next();
                    ItemTarget.Name = ammoRecord.Name;
                    ItemTarget.Type = ItemMerge.Type;
                    ItemTarget.OperationType = ammoRecord.OperationType;
                    ItemTarget.Rarity = ammoRecord.Rarity;
                    ItemTarget.RarityLevel = ammoRecord.RarityLevel.ToString();
                    ItemTarget.Shape = ammoRecord.Shape;
                    ItemTarget.Slot = ammoRecord.Slot;
                    break;
            }

        }

        [Binding]
        public async void OnClickConfirm()
        {
            await PlayfabManager.Instance.CombineItems(_itemsSelected);

            IsActiveSuccesFul = true;
            if (ItemTarget.RarityLevel == "0")
                NextRarity = $"{ItemTarget.Rarity}";
            else
                NextRarity = $"{ItemTarget.Rarity} {ItemTarget.RarityLevel}";

            if (ItemMerge.RarityLevel == "0")
                PreviousRarity = $"{ItemMerge.Rarity}";
            else
                PreviousRarity = $"{ItemMerge.Rarity} {ItemMerge.RarityLevel}";

            SetColorRarity(ItemTarget.Rarity, ColorNextRarity);
            SetColorRarity(ItemMerge.Rarity, ColorPreviousRarity);
            HightlightPopupRecieved.DORotate(new Vector3(0, 0, 360), 4f, DG.Tweening.RotateMode.FastBeyond360)
                                    .SetLoops(-1, LoopType.Restart)
                                    .SetEase(Ease.Linear);
            await UniTask.Delay(2000);
            IsActiveSuccesFul = false;
            LoadData();

        }

        private void SetColorRarity(Rarity rarity, Color ColorRarity)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    ColorRarity = Color.grey;
                    break;
                case Rarity.Good:
                    ColorRarity = Color.green;
                    break;
                case Rarity.Rare:
                    ColorRarity = Color.cyan;
                    break;
                case Rarity.Epic:
                    ColorRarity = new Color(194, 115, 241, 255);
                    break;
                case Rarity.Legend:
                    ColorRarity = Color.yellow;
                    break;
            }
        }
    }
}
