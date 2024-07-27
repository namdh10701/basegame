using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
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
                if (ItemMerge.Type != ItemType.MISC)
                {
                    var itemType = ItemMerge.Type.ToString().ToLower();
                    var itemOperationType = ItemMerge.OperationType.ToLower();
                    var itemRarity = ItemMerge.Rarity.ToString().ToLower();
                    var path = $"Items/item_{itemType}_{itemOperationType}_{itemRarity}";
                    return Resources.Load<Sprite>(path);
                }
                else
                {
                    var path = ItemMerge.Id == null ? $"Items/item_ammo_arrow_common" : $"Items/item_misc_{ItemMerge.Id.ToString().ToLower()}";
                    return Resources.Load<Sprite>(path);
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
                switch (TypeItemTarget)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetCannonImage(IdItemTarget);
                    case ItemType.CREW:
                        return _Game.Scripts.DB.Database.GetCrewImage(IdItemTarget);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetAmmoImage(IdItemTarget);
                    default:
                        return null;
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

                DoFilter();
            }
        }
        private int _filterItemTypeIndex = 0;
        #endregion

        #region Binding Prop: HighestLevelItem
        /// <summary>
        /// HighestLevelItem
        /// </summary>
        [Binding]
        public int LevelItemMerge
        {
            get => _highestLevelItem;
            set
            {
                if (_highestLevelItem == value)
                {
                    return;
                }

                _highestLevelItem = value;

                OnPropertyChanged(nameof(LevelItemMerge));

                DoFilter();
            }
        }
        private int _highestLevelItem;
        #endregion

        private List<InventoryItem> _dataSource = new List<InventoryItem>();
        private List<InventoryItem> _itemsSelected = new List<InventoryItem>();

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

        private void Awake()
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

        public void DoFilter(bool clearSelection = false)
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
                DoFilterItemMerge(item.Id);

                if (_itemsSelected.Count < NumberItemsRequired)
                    _itemsSelected.Add(item);
                else
                {
                    _itemsSelected.RemoveAt(0);
                    _itemsSelected.Add(item);
                }
                NumberItems++;
            }
            else
            {
                NumberItems--;
                _itemsSelected.Remove(item);
                DoFilter();
            }
            // IsActiveItemMerge = _itemsSelected.Count > 0;
            // ItemMerge = FindHighestLevelItem(_itemsSelected);
            // LevelItemMerge = ItemMerge.Level;
            // OnPropertyChanged(nameof(SpriteItemMerge));
            // LoadStarsItem(ItemMerge, starsItemMerge);
            CanMerge = NumberItems == NumberItemsRequired ? true : false;
        }

        public void LoadStarsItem(InventoryItem item, ObservableList<Star> stars)
        {
            if (item.Type == ItemType.CREW || item.Type == ItemType.MISC) return;

            for (int i = 0; i < int.Parse(item.RarityLevel); i++)
            {
                stars.Add(new Star());
            }
        }

        protected void DoFilterItemMerge(string idFilter)
        {
            Items.Clear();

            var itemList = _dataSource
                .Where(v =>
                    v.Id == idFilter
                    && IgnoreIdList.All(ignoredKey => ignoredKey != (v.Type + v.Id))
                    && !IgnoreItems.Contains(v)
                )
                .ToList();
            var pageItemList = itemList;
            Items.AddRange(pageItemList);
        }

        private InventoryItem FindHighestLevelItem(List<InventoryItem> items)
        {
            if (items == null || items.Count == 0)
            {
                return null;
            }

            InventoryItem highestLevelItem = items[0];
            foreach (var item in items)
            {
                if (item.Level > highestLevelItem.Level)
                {
                    highestLevelItem = item;
                }
            }

            return highestLevelItem;
        }
    }
}
