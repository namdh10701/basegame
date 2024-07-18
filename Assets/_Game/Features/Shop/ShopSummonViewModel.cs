using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Unity.VisualScripting;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopSummonViewModel : RootViewModel
    {
        #region Binding: SummonItems
        private ObservableList<ShopSummonItem> summonItems = new ObservableList<ShopSummonItem>();

        [Binding]
        public ObservableList<ShopSummonItem> SummonItems => summonItems;
        #endregion

        #region Binding: SummonItems
        private ObservableList<ShopItemGachaReceived> itemsGachaReceived = new ObservableList<ShopItemGachaReceived>();

        [Binding]
        public ObservableList<ShopItemGachaReceived> ItemsGachaReceived => itemsGachaReceived;
        #endregion

        #region Binding Prop: IsActiveItemGachaReceived

        /// <summary>
        /// IsActiveItemGachaReceived
        /// </summary>
        [Binding]
        public bool IsActiveItemGachaReceived
        {
            get => _isActiveItemGachaReceived;
            set
            {
                if (Equals(_isActiveItemGachaReceived, value))
                {
                    return;
                }

                _isActiveItemGachaReceived = value;
                OnPropertyChanged(nameof(IsActiveItemGachaReceived));
            }
        }

        private bool _isActiveItemGachaReceived;

        #endregion
        List<ShopListingTableRecord> _shopDataSummons = new List<ShopListingTableRecord>();
        private void OnEnable()
        {
            LoadDataShop();
            InitializeShopSummon();
        }

        private void LoadDataShop()
        {
            _shopDataSummons = GameData.ShopListingTable.GetData(ShopType.Gacha);
        }

        private void InitializeShopSummon()
        {
            foreach (var item in _shopDataSummons)
            {
                ShopSummonItem shopSummonItem = new ShopSummonItem();

                shopSummonItem.Id = item.ItemId;
                shopSummonItem.Price = item.PriceAmount.ToString();
                shopSummonItem.GachaType = item.GachaType;
                shopSummonItem.Amount = GameData.ShopItemTable.GetAmountById(item.ItemId);
                shopSummonItem.SetUp(this);
                SummonItems.Add(shopSummonItem);
            }
        }
    }
}