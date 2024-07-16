using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopViewModel : RootViewModel
    {
        #region Binding Prop: ActiveNavIndex

        private int _activeNavIndex = 0;

        [Binding]
        public int ActiveNavIndex
        {
            get => _activeNavIndex;
            set
            {
                if (_activeNavIndex == value)
                {
                    return;
                }

                _activeNavIndex = value;

                OnPropertyChanged(nameof(ActiveNavIndex));

                // NavTo((Nav)value);
            }
        }

        #endregion

        private List<ShopItem> itemSource = new List<ShopItem>();

        #region Binding: Items

        private ObservableList<ShopItem> items = new ObservableList<ShopItem>();

        [Binding]
        public ObservableList<ShopItem> Items => items;

        #endregion

        #region Binding: BigGemItem
        private ObservableList<ShopGemItem> bigGemItem = new ObservableList<ShopGemItem>();

        [Binding]
        public ObservableList<ShopGemItem> BigGemItem => bigGemItem;
        #endregion

        #region Binding: MediumGemItem
        private ObservableList<ShopGemItem> mediumGemItem = new ObservableList<ShopGemItem>();

        [Binding]
        public ObservableList<ShopGemItem> MediumGemItem => mediumGemItem;
        #endregion

        #region Binding: SmallGemItem
        private ObservableList<ShopGemItem> smallGemItem = new ObservableList<ShopGemItem>();

        [Binding]
        public ObservableList<ShopGemItem> SmallGemItem => smallGemItem;
        #endregion

        #region Binding: SummonItems
        private ObservableList<ShopSummonItem> summonItems = new ObservableList<ShopSummonItem>();

        [Binding]
        public ObservableList<ShopSummonItem> SummonItems => summonItems;
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
            }
        }

        private bool _isMultiSelect;

        #endregion

        public ShopItem HighlightItem
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
        private ShopItem _highlightItem;

        // #region Binding Prop: FilterItemType
        //
        // [Binding]
        // public ShopItem.ItemType FilterItemType => (ShopItem.ItemType)_filterItemTypeIndex;
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

                DoFilter();
            }
        }

        #endregion

        private void DoFilter()
        {
            var itemType = (ItemType)_filterItemTypeIndex;
            Items.Clear();
            Items.AddRange(itemSource.Where(v => v.Type == itemType));
        }

        List<ShopItemTableRecord> _shopDataItemRecords = new List<ShopItemTableRecord>();
        List<ShopListingTableRecord> _shopDataSummons = new List<ShopListingTableRecord>();
        List<ShopRarityTableRecord> _shopDataRarityRecord = new List<ShopRarityTableRecord>();
        //
        private void Awake()
        {
            LoadDataShop();
            InitializeShopGem();
            InitializeShopCommon();
        }


        //
        private void LoadDataShop()
        {
            _shopDataItemRecords = GameData.ShopItemTable.GetData();
            _shopDataSummons = GameData.ShopListingTable.GetData(ShopType.Gacha);
            _shopDataRarityRecord = GameData.ShopRarityTable.GetData();
        }

        protected void InitializeShopGem()
        {
            var gemItems = _shopDataItemRecords.Where(item => item.Type == "gem").Select(item =>
            {
                var (price, packSize) = GameData.ShopListingTable.GetPriceById(ShopType.Gem, item.ItemId);
                return new ShopGemItem
                {
                    Id = item.ItemId,
                    Price = price,
                    Amount = item.ItemAmount.ToString(),
                    PackSize = packSize
                };
            });

            foreach (var shopGemItem in gemItems)
            {
                switch (shopGemItem.PackSize)
                {
                    case PackSize.Big:
                        BigGemItem.Add(shopGemItem);
                        break;
                    case PackSize.Medium:
                        MediumGemItem.Add(shopGemItem);
                        break;
                    case PackSize.Small:
                        SmallGemItem.Add(shopGemItem);
                        break;
                }
            }
        }

        private void InitializeShopCommon()
        {
            foreach (var item in _shopDataSummons)
            {
                ShopSummonItem shopSummonItem = new ShopSummonItem();

                shopSummonItem.Id = item.ItemId;
                shopSummonItem.Price = item.PriceAmount.ToString();
                shopSummonItem.GachaType = item.GachaType;
                shopSummonItem.ListRarity = GameData.ShopItemTable.GetRarityById(item.ItemId);
                shopSummonItem.ListWeightRarity = GameData.ShopItemTable.GetWeightRarityById(item.ItemId);
                shopSummonItem.ListNameItem = GameData.ShopRarityTable.GetDataNames(item.GachaType);
                shopSummonItem.ListWeightNameItem = GameData.ShopRarityTable.GetWeights(item.ItemId, shopSummonItem.CurentRarityItemGacha);
                SummonItems.Add(shopSummonItem);
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
    }
}