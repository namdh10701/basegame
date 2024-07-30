using System;
using _Game.Features.Inventory;
using _Game.Features.InventoryItemInfo;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.InventoryCustomScreen
{
    [Binding]
    public class CannonCustomScreen : ModalWithViewModel
    {
        #region Binding Prop: Slot
        /// <summary>
        /// Slot
        /// </summary>
        [Binding]
        public string Slot
        {
            get => _slot;
            set
            {
                if (Equals(_slot, value))
                {
                    return;
                }

                _slot = value;
                OnPropertyChanged(nameof(Slot));
            }
        }
        private string _slot;
        #endregion

        #region Binding Prop: ItemName
        /// <summary>
        /// ItemName
        /// </summary>
        [Binding]
        public string ItemName
        {
            get => _itemName;
            set
            {
                if (Equals(_itemName, value))
                {
                    return;
                }

                _itemName = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }
        private string _itemName;
        #endregion

        #region Binding Prop: Rarity
        /// <summary>
        /// Rarity
        /// </summary>
        [Binding]
        public string Rarity
        {
            get => _rarity;
            set
            {
                if (Equals(_rarity, value))
                {
                    return;
                }

                _rarity = value;
                OnPropertyChanged(nameof(Rarity));
            }
        }
        private string _rarity;
        #endregion

        [Binding]
        public Sprite SpriteReview
        {
            get
            {
                switch (InventoryItem.Type)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetCannonImage(InventoryItem.Id);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetAmmoImage(InventoryItem.Id);
                    default:
                        return null;
                }
            }
        }

        #region Binding: Skills
        private ObservableList<SkillInvetoryItem> skills = new ObservableList<SkillInvetoryItem>();

        [Binding]
        public ObservableList<SkillInvetoryItem> Skills => skills;
        #endregion

        #region Binding: ItemStats
        private ObservableList<ItemStat> itemStats = new ObservableList<ItemStat>();

        [Binding]
        public ObservableList<ItemStat> ItemStats => itemStats;
        #endregion

        public InventoryItem InventoryItem { get; set; }


        public override async UniTask Initialize(Memory<object> args)
        {
            InventoryItem = args.Span[0] as InventoryItem;
            skills = args.Span[1] as ObservableList<SkillInvetoryItem>;
            itemStats = args.Span[2] as ObservableList<ItemStat>;
            LoadData();
        }

        private void LoadData()
        {
            ItemName = InventoryItem.Name;
            Slot = InventoryItem.Slot;
            Rarity = $"[{InventoryItem.Rarity.ToString()}]";
        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }
    }
}
