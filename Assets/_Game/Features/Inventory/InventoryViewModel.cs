using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    [Binding]
    public class InventoryViewModel : RootViewModel
    {
        private List<InventoryItem> dataSource = new List<InventoryItem>();
        
        public List<string> IgnoreIdList = new List<string>();

        public List<InventoryItem> SelectedItems => dataSource.Where(v => v.IsSelected).ToList();
        
        #region Binding: Items

        private ObservableList<InventoryItem> items = new ObservableList<InventoryItem>();

        [Binding]
        public ObservableList<InventoryItem> Items => items;

        [Binding] 
        public ObservableList<InventoryItem> IgnoreItems { get; set; } = new();

        #endregion

        #region Binding Prop: IsMultiSelect

        /// <summary>
        /// IsMultiSelect
        /// </summary>
        [Binding]
        public bool IsMultiSelect
        {
            get => _isMultiSelect;
            set
            {
                if (Equals(_isMultiSelect, value))
                {
                    return;
                }

                _isMultiSelect = value;
                OnPropertyChanged(nameof(IsMultiSelect));

                OnMultiSelectModeChanged(value);
            }
        }

        private void OnMultiSelectModeChanged(bool isEnabled)
        {
            if (isEnabled)
            {
                return;
            }

            DeselectAll();
        }

        private void DeselectAll(InventoryItem ignoreItem = null)
        {
            foreach (var inventoryItem in Items.Where(v => v.IsSelected))
            {
                if (inventoryItem != ignoreItem)
                {
                    inventoryItem.IsSelected = false;
                }
            }
        }

        private bool _isMultiSelect;

        #endregion

        public InventoryItem HighlightItem
        {
            get => _highlightItem;
            set
            {
                if (_highlightItem != null)
                {
                    _highlightItem.IsHighLight = false;
                }

                _highlightItem = value;
                _highlightItem.IsHighLight = true;
            }
        }
        private InventoryItem _highlightItem;

        // #region Binding Prop: FilterItemType
        //
        // [Binding]
        // public InventoryItem.ItemType FilterItemType => (InventoryItem.ItemType)_filterItemTypeIndex;
        //
        // #endregion

        #region Binding Prop: FilterItemTypeIndex

        private int _filterItemTypeIndex = 0;

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
                // OnPropertyChanged(nameof(FilterItemType));

                Page = 0;
                DoFilter();
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

        private int _itemPerPage = 8;

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

        public void DoFilter(bool clearSelection = false)
        {
            if (clearSelection)
            {
                foreach (var inventoryItem in dataSource)
                {
                    inventoryItem.IsSelected = false;
                }
            }
            
            var itemType = (ItemType)_filterItemTypeIndex;
            Items.Clear();

            var itemList = dataSource
                .Where(v => 
                    v.Type == itemType 
                    && IgnoreIdList.All(ignoredKey => ignoredKey != (v.Type + v.Id))
                    && !IgnoreItems.Contains(v)
                )
                .ToList();
            var pageItemList = itemList
                .Skip(Page * ItemPerPage)
                .Take(ItemPerPage);
            
            TotalItemCount = itemList.Count();
            
            Items.AddRange(pageItemList);

            
            OnPropertyChanged(nameof(Page));
            OnPropertyChanged(nameof(IsPrevPageAvailable));
            OnPropertyChanged(nameof(IsNextPageAvailable));
        }

        [Binding]
        public int MaxPage => Math.Max(0, (int) Math.Ceiling(1f *TotalItemCount / ItemPerPage) - 1);

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

        [Binding]
        public void NextPage()
        {
            Page = Math.Min(MaxPage, Page + 1);
        }

        [Binding]
        public void PrevPage()
        {
            Page = Math.Max(0, Page - 1);
        }
        //
        // private void Awake()
        // {
        //     GDConfigLoader.Instance.OnLoaded += Init;
        //     GDConfigLoader.Instance.Load();
        // }
        //
        // private void Init()
        // {
        //     
        //     for (int i = 0; i < 3; i++)
        //     {
        //         itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CREW });
        //     }
        //     
        //     // for (int i = 0; i < 5; i++)
        //     // {
        //     //     itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0001"});
        //     // }
        //     // GDConfigLoader.Instance.Cannons["0001"];
        //     itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0001"});
        //     itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.CANNON, Id = "0012"});
        //     
        //     for (int i = 0; i < 30; i++)
        //     {
        //         itemSource.Add(new InventoryItem {  InventoryViewModel = this, Type = ItemType.AMMO });
        //     }
        //
        // }

        protected virtual void Awake()
        {
            InitializeInternal();
            IgnoreItems.CollectionChanged += (sender, args) =>
            {
                DoFilter();
            };
        }

        protected void InitializeInternal()
        {
            foreach (var (id, conf) in GDConfigLoader.Instance.Cannons)
            {
                Enum.TryParse(conf.rarity, true, out Rarity rarity);
                dataSource.Add(new InventoryItem
                {
                    InventoryViewModel = this, Type = ItemType.CANNON, Id = id, Rarity = rarity,
                    RarityLevel = conf.rarity_level, OperationType = conf.operation_type,
                    Shape = conf.shape
                });
            }
            
            foreach (var (id, conf) in GDConfigLoader.Instance.Ammos)
            {
                Enum.TryParse(conf.rarity, true, out  Rarity rarity);
                dataSource.Add(new InventoryItem
                {
                    InventoryViewModel = this, Type = ItemType.AMMO, Id = id, Rarity = rarity,
                    RarityLevel = conf.rarity_level, OperationType = conf.operation_type,
                    Shape = conf.shape
                });
            }
            
            foreach (var (id, conf) in GDConfigLoader.Instance.Crews)
            {
                Enum.TryParse(conf.rarity, true, out  Rarity rarity);
                dataSource.Add(new InventoryItem
                {
                    InventoryViewModel = this, Type = ItemType.CREW, Id = id, Rarity = rarity,
                    OperationType = conf.operation_type,
                    Shape = conf.shape
                });
            }
            //
            // var crewNo = 1;
            // for (int i = 1; i <= 2; i++)
            // {
            //     var rarities = Enum.GetValues(typeof(Rarity)).Cast<Rarity>();
            //     foreach (var rarity in rarities)
            //     {
            //         dataSource.Add(new InventoryItem
            //         {
            //             InventoryViewModel = this, Type = ItemType.CREW,
            //             Id = $"{(crewNo++).ToString().PadLeft(4, '0')}", Rarity = rarity, OperationType = $"{i}",
            //             Shape = conf.shape
            //         });
            //     }
            // }
            
            foreach (var inventoryItem in dataSource)
            {
                inventoryItem.SelectionStateChanged += OnSelectionStateChanged;
            }
            
            dataSource = dataSource.Where(v => v.Thumbnail != null).ToList();
            DoFilter();
        }

        private void OnSelectionStateChanged(InventoryItem inventoryItem)
        {
            if (!IsMultiSelect && inventoryItem.IsSelected)
            {
                DeselectAll(inventoryItem);
            }
        }


        [Binding]
        public void OnEquipSelectedItems()
        {
            foreach (var item in Items.Where(v => v.IsSelected))
            {
                item.IsEquipped = true;
            }
        }
        
        [Binding]
        public void ToggleMultiSelect()
        {
            IsMultiSelect = !IsMultiSelect;
        }

        public void AddIgnore(InventoryItem item)
        {
            if (IgnoreItems.Contains(item)) return;

            IgnoreItems.Add(item);
        }
        
        public void RemoveIgnore(InventoryItem item)
        {
            if (!IgnoreItems.Contains(item)) return;

            IgnoreItems.Remove(item);
        }
        
        public void ClearIgnoredItems()
        {
            IgnoreItems.Clear();
        }
    }
}