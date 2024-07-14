using System;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{

    public enum ItemType
    {
        CREW,
        CANNON,
        AMMO,
        None,
    }

    public enum Rarity
    {
        Common,
        Good,
        Rare,
        Epic,
        Legend,
    }
    [Binding]
    public class Star : SubViewModel
    {

    }

    [Binding]
    public class InventoryItem : SubViewModel
    {
        public InventoryViewModel InventoryViewModel { get; set; }

        [Binding]
        public string Id { get; set; }

        #region Binding Prop: Name
        /// <summary>
        /// Name
        /// </summary>
        [Binding]
        public string Name
        {
            get => m_name;
            set
            {
                if (Equals(m_name, value))
                {
                    return;
                }

                m_name = value;
                OnPropertyChanged(nameof(Name));
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

        #region Binding Prop: Shape

        /// <summary>
        /// Shape
        /// </summary>
        [Binding]
        public string Shape
        {
            get => _shape;
            set
            {
                if (Equals(_shape, value))
                {
                    return;
                }

                _shape = value;
                OnPropertyChanged(nameof(Shape));
            }
        }

        private string _shape;

        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var itemType = Type.ToString().ToLower();
                var itemOperationType = OperationType.ToLower();
                var itemRarity = Rarity.ToString().ToLower();
                var path = $"Items/item_{itemType}_{itemOperationType}_{itemRarity}";
                return Resources.Load<Sprite>(path);
            }
        }

        public event Action<InventoryItem> SelectionStateChanged;


        #region Binding Prop: IsSelected
        /// <summary>
        /// IsSelected
        /// </summary>
        [Binding]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (Equals(_isSelected, value))
                {
                    return;
                }

                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));

                SelectionStateChanged?.Invoke(this);
            }
        }

        private bool _isSelected;
        #endregion

        #region Binding Prop: IsEquipped
        /// <summary>
        /// IsEquipped
        /// </summary>
        [Binding]
        public bool IsEquipped
        {
            get => _isEquipped;
            set
            {
                if (Equals(_isEquipped, value))
                {
                    return;
                }

                _isEquipped = value;
                OnPropertyChanged(nameof(IsEquipped));
            }
        }
        private bool _isEquipped;
        #endregion

        #region Binding Prop: IsHighLight
        /// <summary>
        /// IsHighLight
        /// </summary>
        [Binding]
        public bool IsHighLight
        {
            get => _isHighLight;
            set
            {
                if (Equals(_isHighLight, value))
                {
                    return;
                }

                _isHighLight = value;
                OnPropertyChanged(nameof(IsHighLight));
            }
        }
        private bool _isHighLight;
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

        #region Binding Prop: NumbStars
        /// <summary>
        /// NumbStars
        /// </summary>
        [Binding]
        public int NumbStars
        {
            get => _numbStars;
            set
            {
                if (Equals(_numbStars, value))
                {
                    return;
                }

                _numbStars = value;
                OnPropertyChanged(nameof(NumbStars));
            }
        }
        private int _numbStars;
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

        [Binding]
        public void ShowItemDetail()
        {
            // PopupManager.Instance.ShowPopup<GearInfoPopup>();
        }

        [Binding]
        public void SetAsHighLightItem()
        {
            InventoryViewModel.HighlightItem = this;
        }

        void Awake()
        {
            for (int i = 0; i < NumbStars; i++)
            {
                Stars.Add(new Star());
            }
        }
    }
}