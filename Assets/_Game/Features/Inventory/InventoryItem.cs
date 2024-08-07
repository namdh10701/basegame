using System;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.DB;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{

    public enum ItemType
    {
        SHIP,
        CREW,
        CANNON,
        AMMO,
        CARPET,
        MISC,
        BLUEPRINT,
        GOLD,
        GEM,
        DIAMOND,
        TICKET,
        KEY,
        EXP,
        None,
    }

    public static class MiscItemId
    {
        public static readonly string blueprint_ship = "res_blueprint_ship";
        public static readonly string blueprint_cannon = "res_blueprint_cannon";
        public static readonly string blueprint_ammo = "res_blueprint_ammo";
        public static readonly string gold = "res_gold";
        public static readonly string gem = "res_gem";
        public static readonly string diamond = "res_diamond";
        public static readonly string exp = "res_exp";
        public static readonly string energy = "res_energy";
        public static readonly string ranking_ticket = "res_ranking_ticket";
        public static readonly string key = "res_vipkey";
    }

    [Binding]
    public class Star : SubViewModel
    {

    }



    [Binding]
    public class InventoryItem : SubViewModel
    {
        public InventoryViewModel InventoryViewModel { get; set; }

        public object BackedData;

        [Binding]
        public string Id { get; set; }

        [Binding]
        public string OwnItemId { get; set; }

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
                return Database.GetItemSprite(Type, Id);
                if (Type != ItemType.MISC)
                {
                    var itemType = Type.ToString().ToLower();
                    var itemOperationType = OperationType.ToLower();
                    var itemRarity = Rarity.ToString().ToLower();
                    var path = $"Images/Items/item_{itemType}_{itemOperationType}_{itemRarity}";
                    return Resources.Load<Sprite>(path);
                }
                else if (Type != ItemType.MISC)
                {
                    var path = $"Items/item_ship_{Id}";
                    return Resources.Load<Sprite>(path);
                }
                else
                {
                    var path = Id == null ? $"Images/Items/item_ammo_arrow_common" : $"Images/Items/item_misc_{Id.ToString().ToLower()}";
                    return Resources.Load<Sprite>(path);
                }

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
        private ObservableList<Star> stars = new ObservableList<Star>();

        [Binding]
        public ObservableList<Star> Stars => stars;
        #endregion

        public void LoadStarsItem()
        {
            if (Type == ItemType.CREW || Type == ItemType.MISC) return;

            if (!int.TryParse(RarityLevel, out var lv))
            {
                return;
            }

            for (int i = 0; i < lv; i++)
            {
                Stars.Add(new Star());
            }
        }

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

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not InventoryItem other) return false;

            return other.Type == Type && other.Id == Id;
        }
    }
}