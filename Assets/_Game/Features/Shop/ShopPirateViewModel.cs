using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using Online;
using Unity.VisualScripting;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopPirateViewModel : ModalWithViewModel
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
                InitDataShopPirateGem(ActiveNavIndex);
            }
        }
        #endregion

        #region Binding: ItemsGem
        private ObservableList<ShopPirateItem> itemsGem = new ObservableList<ShopPirateItem>();
        [Binding]
        public ObservableList<ShopPirateItem> ItemsGem => itemsGem;
        #endregion

        List<ShopListingTableRecord> _shopDataPirate = new List<ShopListingTableRecord>();
        public override async UniTask Initialize(Memory<object> args)
        {
            InitDataShopPirateGem(ActiveNavIndex);
        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }

        protected void InitDataShopPirateGem(int activeNavIndex)
        {
            ItemsGem.Clear();
            var type = (ShopType)(activeNavIndex + 2);
            _shopDataPirate = GameData.ShopListingTable.GetData(type);
            if (_shopDataPirate.Count <= 0) return;
            foreach (var item in _shopDataPirate)
            {
                ShopPirateItem itemGem = new ShopPirateItem();
                itemGem.Id = item.ItemId;
                itemGem.Name = item.Name;
                itemGem.Price = item.PriceAmount.ToString();
                itemGem.Amount = $"x{GameData.ShopItemTable.GetAmountById(item.ItemId).Item1[0]}";
                itemGem.PriceType = item.PriceType;
                itemGem.ShopType = item.ShopType.ToString();

                var amounts = GameData.ShopItemTable.GetAmountById(item.ItemId).Item1;
                var types = GameData.ShopItemTable.GetAmountById(item.ItemId).Item2;
                for (int i = 0; i < amounts.Count; i++)
                {
                    ShopPirateItemReceived shopPirateItemReceived = new ShopPirateItemReceived();
                    shopPirateItemReceived.Type = types[i];

                    if (shopPirateItemReceived.Type == "gold")
                    {
                        shopPirateItemReceived.Amount = (amounts[i] * PlayfabManager.Instance.Level).ToString();
                    }
                    else if (shopPirateItemReceived.Type == "random_blueprint")
                    {
                        string[] blueprints = { "blueprint0", "blueprint1", "blueprint2" };
                        System.Random random = new System.Random();
                        int randomIndex = random.Next(blueprints.Length);

                        shopPirateItemReceived.Type = blueprints[randomIndex];
                        shopPirateItemReceived.Amount = amounts[i].ToString();
                    }
                    else
                        shopPirateItemReceived.Amount = amounts[i].ToString();

                    itemGem.ItemsDailyReceived.Add(shopPirateItemReceived);
                }

                itemGem.IsActiveButAd = item.PriceType == "ads" ? true : false;
                ItemsGem.Add(itemGem);
            }
        }
    }
}