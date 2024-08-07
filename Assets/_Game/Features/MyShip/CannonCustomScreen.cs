using System;
using _Game.Features.Inventory;
using _Game.Features.InventoryItemInfo;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
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

        #region Binding Prop: RarityItem
        /// <summary>
        /// RarityItem
        /// </summary>
        [Binding]
        public string RarityItem
        {
            get => _rarityItem;
            set
            {
                if (Equals(_rarityItem, value))
                {
                    return;
                }

                _rarityItem = value;
                OnPropertyChanged(nameof(RarityItem));
            }
        }
        private string _rarityItem;
        #endregion

        #region Binding Prop: ColorRarity
        /// <summary>
        /// ColorRarity
        /// </summary>
        [Binding]
        public Color ColorRarity
        {
            get => _colorRarity;
            set
            {
                if (Equals(_colorRarity, value))
                {
                    return;
                }

                _colorRarity = value;
                OnPropertyChanged(nameof(ColorRarity));
            }
        }
        private Color _colorRarity;
        #endregion

        #region Binding Prop: IsSetActiveSprite
        /// <summary>
        /// IsSetActiveSprite
        /// </summary>
        [Binding]
        public bool IsSetActiveSprite
        {
            get => _isSetActiveSprite;
            set
            {
                if (Equals(_isSetActiveSprite, value))
                {
                    return;
                }

                _isSetActiveSprite = value;
                OnPropertyChanged(nameof(IsSetActiveSprite));
            }
        }
        private bool _isSetActiveSprite;
        #endregion

        [Binding]
        public Sprite SpriteReview
        {
            get
            {
                switch (InventoryItem.Type)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetSpriteCannon(InventoryItem.Id);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetSpriteAmmo(InventoryItem.Id);
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
        private ObservableList<ItemStat> stats = new ObservableList<ItemStat>();

        [Binding]
        public ObservableList<ItemStat> Stats => stats;
        #endregion

        public InventoryItem InventoryItem { get; set; }
        public Image MainItem;


        public override async UniTask Initialize(Memory<object> args)
        {
            Skills.Clear();
            Stats.Clear();
            InventoryItem = args.Span[0] as InventoryItem;
            var itemSkill = args.Span[1] as ObservableList<SkillInvetoryItem>;
            foreach (var item in itemSkill)
            {
                Skills.Add(item);
            }

            var itemStats = args.Span[2] as ObservableList<ItemStat>;
            foreach (var item in itemStats)
            {
                Stats.Add(item);
            }

            LoadData();
        }

        private void LoadData()
        {
            ItemName = InventoryItem.Name;
            Slot = InventoryItem.Slot;
            RarityItem = $"[{InventoryItem.Rarity.ToString()}]";
            SetColorRarity();
            OnPropertyChanged(nameof(SpriteReview));
            MainItem.SetNativeSize();
        }

        private void SetColorRarity()
        {
            switch (InventoryItem.Rarity)
            {
                case Rarity.Common:
                    ColorRarity = Color.grey;
                    break;
                case Rarity.Good:
                    ColorRarity = Color.green;
                    break;
                case Rarity.Rare:
                    ColorRarity = Color.cyan;
                    break;
                case Rarity.Epic:
                    ColorRarity = new Color(194, 115, 241, 255);
                    break;
                case Rarity.Legend:
                    ColorRarity = Color.yellow;
                    break;
            }
        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }
    }
}
