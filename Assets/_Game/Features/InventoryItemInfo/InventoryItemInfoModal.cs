using System;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.InventoryItemInfo
{
    [Binding]
    public class InventoryItemInfoModal : ModalWithViewModel
    {
        [SerializeField] GameObject _enhance;
        [Binding]
        public InventoryItem InventoryItem { get; set; }

        [Binding]
        public string Id { get; set; }
        #region Binding Prop: ItemName
        /// <summary>
        /// ItemName
        /// </summary>
        [Binding]
        public string ItemName
        {
            get => m_name;
            set
            {
                if (Equals(m_name, value))
                {
                    return;
                }

                m_name = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }
        private string m_name;
        #endregion


        #region Binding Prop: ItemType
        /// <summary>
        /// ItemType
        /// </summary>
        [Binding]
        public ItemType Type
        {
            get => m_type;
            set
            {
                if (Equals(m_type, value))
                {
                    return;
                }

                m_type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        private ItemType m_type;
        #endregion

        #region Binding Prop: ItemOperationType
        /// <summary>
        /// OperationType
        /// </summary>
        [Binding]
        public string OperationType
        {
            get => _operationType;
            set
            {
                if (Equals(_operationType, value))
                {
                    return;
                }

                _operationType = value;
                OnPropertyChanged(nameof(OperationType));
            }
        }
        private string _operationType;
        #endregion

        #region Binding Prop: Rarity
        /// <summary>
        /// Rarity
        /// </summary>
        [Binding]
        public Rarity Rarity
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
        private Rarity _rarity;
        #endregion

        #region Binding Prop: RarityLevel
        /// <summary>
        /// RarityLevel
        /// </summary>
        [Binding]
        public string RarityLevel
        {
            get => _rarityLevel;
            set
            {
                if (Equals(_rarityLevel, value))
                {
                    return;
                }

                _rarityLevel = value;
                OnPropertyChanged(nameof(RarityLevel));
            }
        }
        private string _rarityLevel;
        #endregion

        #region Binding Prop: Slot
        /// <summary>
        /// Slot
        /// </summary>
        [Binding]
        public int Slot
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
        private int _slot;
        #endregion

        #region Binding Prop: Level
        /// <summary>
        /// Slot
        /// </summary>
        [Binding]
        public int Level
        {
            get => _level;
            set
            {
                if (Equals(_level, value))
                {
                    return;
                }

                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        private int _level;
        #endregion

        #region Binding: Stars
        private ObservableList<GameObject> stars = new ObservableList<GameObject>();

        [Binding]
        public ObservableList<GameObject> Stars => stars;
        #endregion

        #region Binding: Stars
        private ObservableList<SkillInvetoryItem> skill = new ObservableList<SkillInvetoryItem>();

        [Binding]
        public ObservableList<SkillInvetoryItem> Skill => skill;
        #endregion

        #region Binding Prop: SpriteMainItem
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite SpriteMainItem
        {
            get
            {
                var path = Type == null || OperationType == null || Rarity == null ? $"Items/item_ammo_arrow_common" :
                 $"Items/item_{Type.ToString().ToLower()}_{OperationType.ToLower()}_{Rarity.ToString().ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }
        #endregion



        public override async UniTask Initialize(Memory<object> args)
        {
            InventoryItem = args.ToArray().FirstOrDefault() as InventoryItem;

            Id = InventoryItem.Id;
            ItemName = InventoryItem.Name;
            Type = InventoryItem.Type;
            OperationType = InventoryItem.OperationType;
            Rarity = InventoryItem.Rarity;
            RarityLevel = InventoryItem.RarityLevel;
            Slot = InventoryItem.Slot;
            Level = InventoryItem.Level;
        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }

    }
}