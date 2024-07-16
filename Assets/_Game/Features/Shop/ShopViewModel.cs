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

        public event Action OnClickGacha;
        List<ShopItemTableRecord> _shopDataItemRecords = new List<ShopItemTableRecord>();
        List<ShopListingTableRecord> _shopDataItemGem = new List<ShopListingTableRecord>();
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
            _shopDataItemRecords = GameData.ShopItemTable.GetRecords();
            _shopDataSummons = GameData.ShopListingTable.GetData(ShopType.Gacha);
            _shopDataItemGem = GameData.ShopListingTable.GetData(ShopType.Gem);
            _shopDataRarityRecord = GameData.ShopRarityTable.GetRecords();
        }

        protected void InitializeShopGem()
        {
            var gemItems = new List<ShopGemItem>();

            foreach (var item in _shopDataItemGem)
            {
                var shopGemItem = new ShopGemItem
                {
                    Id = item.ItemId,
                    Price = item.PriceAmount.ToString(),
                    Amount = GameData.ShopItemTable.GetAmountById(item.ItemId).ToString(),
                    PackSize = item.PackSize,
                    PriceType = item.PriceType
                };
                gemItems.Add(shopGemItem);

            }

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
                shopSummonItem.OnClickGacha += OnClickGacha;
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

        [Binding]
        public void ClickGacha()
        {
            OnClickGacha?.Invoke();
        }
    }
}