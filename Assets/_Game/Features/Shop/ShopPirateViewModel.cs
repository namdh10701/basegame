using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
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
        private ObservableList<PirateItem> itemsGem = new ObservableList<PirateItem>();
        [Binding]
        public ObservableList<PirateItem> ItemsGem => itemsGem;
        #endregion

        List<ShopListingTableRecord> _shopDataPirate = new List<ShopListingTableRecord>();
        public override async UniTask Initialize(Memory<object> args)
        {
            OnPropertyChanged(nameof(ActiveNavIndex));
            // InitDataShopPirateGem(ActiveNavIndex);
        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }

        protected void InitDataShopPirateGem(int activeNavIndex)
        {
            var type = (ShopType)(activeNavIndex + 2);
            _shopDataPirate = GameData.ShopListingTable.GetData(type);
            if (_shopDataPirate.Count <= 0) return;
            foreach (var item in _shopDataPirate)
            {
                PirateItem itemGem = new PirateItem();
                itemGem.Id = item.ItemId;
                itemGem.Price = item.PriceAmount.ToString();
                itemGem.PriceType = item.PriceType;
                itemGem.Amount = GameData.ShopItemTable.GetAmountById(item.ItemId);
                itemGem.IsActiveButAd = item.PriceType == "ads" ? true : false;
                ItemsGem.Add(itemGem);
            }
        }
    }
}