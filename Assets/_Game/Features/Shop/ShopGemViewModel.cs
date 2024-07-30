using System;
using System.Collections.Generic;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using Online;
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
            InitializeShopGem();
        }

        void InitializeShopGem()
        {
            BigGemItem.Clear();
            MediumGemItem.Clear();
            SmallGemItem.Clear();
            _shopDataItemGem = GameData.ShopListingTable.GetData(ShopType.Gem);

            var gemItems = new List<ShopGemItem>();
            foreach (var item in _shopDataItemGem)
            {
                var shopGemItem = new ShopGemItem
                {
                    Id = item.ItemId,
                    Price = item.PriceAmount.ToString(),
                    Amount = GameData.ShopItemTable.GetAmountById(item.ItemId).Item1[0].ToString(),
                    PackSize = item.PackSize,
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
