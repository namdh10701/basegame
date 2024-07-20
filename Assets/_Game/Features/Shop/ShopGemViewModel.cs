using System;
using System.Collections.Generic;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopGemViewModel : ModalWithViewModel
    {
        List<ShopListingTableRecord> _shopDataItemGem = new List<ShopListingTableRecord>();
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

        public override async UniTask Initialize(Memory<object> args)
        {
            _shopDataItemGem = GameData.ShopListingTable.GetData(ShopType.Gem);
            InitializeShopGem();
        }
        void InitializeShopGem()
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

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }
    }
}
