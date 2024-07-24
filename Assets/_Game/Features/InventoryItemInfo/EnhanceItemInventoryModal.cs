using System;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace _Game.Features.InventoryItemInfo
{
    [Binding]
    public class EnhanceItemInventoryModal : ModalWithViewModel
    {
        [Binding]
        public InventoryItem InventoryItem { get; set; }

        [Binding]
        public string Id { get; set; }

        [Binding]
        public string IdIngredients { get; set; }


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

        #region Binding: Stars
        private ObservableList<GameObject> stars = new ObservableList<GameObject>();

        [Binding]
        public ObservableList<GameObject> Stars => stars;
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
                var path = Type == null || OperationType == null || Rarity == null ? $"Items/item_misc_ship" :
                 $"Items/item_{Type.ToString().ToLower()}_{OperationType.ToLower()}_{Rarity.ToString().ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }
        #endregion

        #region Binding Prop: Ingredients
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite Ingredients
        {
            get
            {
                var path = IdIngredients == null ? $"Items/item_ammo_arrow_common" : $"Items/item_misc_{IdIngredients.ToString().ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }
        #endregion

        #region Binding Prop: PreviousLevel
        /// <summary>
        /// PreviousLevel
        /// </summary>
        [Binding]
        public int CurrentLevel
        {
            get => _currentLevel;
            set
            {
                if (Equals(_currentLevel, value))
                {
                    return;
                }

                _currentLevel = value;
                OnPropertyChanged(nameof(CurrentLevel));
            }
        }
        private int _currentLevel;
        #endregion

        #region Binding Prop: PreviousLevel
        /// <summary>
        /// PreviousLevel
        /// </summary>
        [Binding]
        public int NextLevel
        {
            get => _nextLevel;
            set
            {
                if (Equals(_nextLevel, value))
                {
                    return;
                }

                _nextLevel = value;
                OnPropertyChanged(nameof(NextLevel));
            }
        }
        private int _nextLevel;
        #endregion

        #region Binding Prop: NumbMiscItemOwner
        /// <summary>
        /// NumbMiscItemOwner
        /// </summary>
        [Binding]
        public int NumbMiscItemOwner
        {
            get => _numbMiscItemOwner;
            set
            {
                if (Equals(_numbMiscItemOwner, value))
                {
                    return;
                }

                _numbMiscItemOwner = value;
                OnPropertyChanged(nameof(NumbMiscItemOwner));
            }
        }
        private int _numbMiscItemOwner;
        #endregion

        #region Binding Prop: NumbGoldOwner
        /// <summary>
        /// NumbGoldOwner
        /// </summary>
        [Binding]
        public int NumbGoldOwner
        {
            get => _numbGoldOwner;
            set
            {
                if (Equals(_numbGoldOwner, value))
                {
                    return;
                }

                _numbGoldOwner = value;
                OnPropertyChanged(nameof(NumbGoldOwner));
            }
        }
        private int _numbGoldOwner;
        #endregion

        public override async UniTask Initialize(Memory<object> args)
        {
            InventoryItem = args.ToArray().FirstOrDefault() as InventoryItem;
            SetDataInventoryItem(InventoryItem);
            GetResourcesOwner(Type);
        }

        protected void SetDataInventoryItem(InventoryItem inventoryItem)
        {
            Id = inventoryItem.Id;
            Type = inventoryItem.Type;
            OperationType = inventoryItem.OperationType;
            Rarity = inventoryItem.Rarity;
            RarityLevel = inventoryItem.RarityLevel;
            Slot = inventoryItem.Slot;
            CurrentLevel = inventoryItem.Level;
            NextLevel = CurrentLevel + 1;
            OnPropertyChanged(nameof(SpriteMainItem));
            LoadStarsItem();
        }

        protected void GetResourcesOwner(ItemType itemType)
        {
            NumbMiscItemOwner = 0;
            foreach (var item in SaveSystem.GameSave.OwnedItems)
            {
                if (item.ItemId == itemType.ToString().ToLower())
                {
                    IdIngredients = item.ItemId;
                    OnPropertyChanged(nameof(Ingredients));
                    NumbMiscItemOwner++;
                }
            }

            NumbGoldOwner = SaveSystem.GameSave.gold;
        }

        protected void LoadStarsItem()
        {
            if (Type == ItemType.CREW || Type == ItemType.AMMO) return;

            for (int i = 0; i < int.Parse(RarityLevel); i++)
            {
                Stars.Add(new Star());
            }
        }
    }
}