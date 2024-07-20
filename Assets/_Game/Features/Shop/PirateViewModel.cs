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
    public class PirateViewModel : ModalWithViewModel
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

            }
        }
        #endregion

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
            }
        }
        #endregion

        #region Binding: ItemsGem
        private ObservableList<PirateItemGem> itemsGem = new ObservableList<PirateItemGem>();
        [Binding]
        public ObservableList<PirateItemGem> ItemsGem => itemsGem;
        #endregion


        // #region Binding: ItemsDaily
        // private ObservableList<PirateItem> itemDaily = new ObservableList<PirateItem>();
        // [Binding]
        // public ObservableList<PirateItem> ItemsDaily => itemDaily;
        // #endregion


        List<ShopListingTableRecord> _shopDataItemGem = new List<ShopListingTableRecord>();
        List<ShopListingTableRecord> _shopDataItemDaily = new List<ShopListingTableRecord>();
        public override async UniTask Initialize(Memory<object> args)
        {
            _shopDataItemGem = GameData.ShopListingTable.GetData(ShopType.Pirate);
            _shopDataItemDaily = GameData.ShopListingTable.GetData(ShopType.Other);
            InitializeInternal();
        }

        protected void InitializeInternal()
        {
            foreach (var item in itemsGem)
            {

            }
        }


        [Binding]
        public void OnEquipSelectedItems()
        {
            // foreach (var item in Items.Where(v => v.IsSelected))
            // {
            //     item.IsEquipped = true;
            // }
        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }
    }
}