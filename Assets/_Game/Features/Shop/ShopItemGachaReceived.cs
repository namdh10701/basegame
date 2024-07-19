using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopItemGachaReceived : SubViewModel
    {
        [Binding]
        public string Id { get; set; }

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

        #region Binding Prop: GachaType
        /// <summary>
        /// GachaType
        /// </summary>
        [Binding]
        public string GachaType
        {
            get => _gachaType;
            set
            {
                if (Equals(_gachaType, value))
                {
                    return;
                }

                _gachaType = value;
                OnPropertyChanged(nameof(GachaType));
            }
        }
        private string _gachaType;
        #endregion

        #region Binding Prop: Name
        /// <summary>
        /// Name
        /// </summary>
        [Binding]
        public string Name
        {
            get => _name;
            set
            {
                if (Equals(_name, value))
                {
                    return;
                }

                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _name;
        #endregion

        #region Binding Prop: HP
        /// <summary>
        /// HP
        /// </summary>
        [Binding]
        public int HP
        {
            get => _hp;
            set
            {
                if (Equals(_hp, value))
                {
                    return;
                }

                _hp = value;
                OnPropertyChanged(nameof(HP));
            }
        }
        private int _hp;
        #endregion

        #region Binding Prop: Attack
        /// <summary>
        /// Attack
        /// </summary>
        [Binding]
        public int Attack
        {
            get => _attack;
            set
            {
                if (Equals(_attack, value))
                {
                    return;
                }

                _attack = value;
                OnPropertyChanged(nameof(Attack));
            }
        }
        private int _attack;
        #endregion

        #region Binding Prop: Speed
        /// <summary>
        /// Speed
        /// </summary>
        [Binding]
        public int Speed
        {
            get => _speed;
            set
            {
                if (Equals(_speed, value))
                {
                    return;
                }

                _speed = value;
                OnPropertyChanged(nameof(Speed));
            }
        }
        private int _speed;
        #endregion

        #region Binding Prop: Acurracy
        /// <summary>
        /// Acurracy
        /// </summary>
        [Binding]
        public int Acurracy
        {
            get => _acurracy;
            set
            {
                if (Equals(_acurracy, value))
                {
                    return;
                }

                _acurracy = value;
                OnPropertyChanged(nameof(Acurracy));
            }
        }
        private int _acurracy;
        #endregion

        #region Binding Prop: Range
        /// <summary>
        /// Range
        /// </summary>
        [Binding]
        public int Range
        {
            get => _range;
            set
            {
                if (Equals(_range, value))
                {
                    return;
                }

                _range = value;
                OnPropertyChanged(nameof(Range));
            }
        }
        private int _range;
        #endregion

        #region Binding Prop: CritChance
        /// <summary>
        /// CritChance
        /// </summary>
        [Binding]
        public int CritChance
        {
            get => _critChance;
            set
            {
                if (Equals(_critChance, value))
                {
                    return;
                }

                _critChance = value;
                OnPropertyChanged(nameof(CritChance));
            }
        }
        private int _critChance;
        #endregion

        #region Binding Prop: CritDamage
        /// <summary>
        /// CritDamage
        /// </summary>
        [Binding]
        public int CritDamage
        {
            get => _critDamage;
            set
            {
                if (Equals(_critDamage, value))
                {
                    return;
                }

                _critDamage = value;
                OnPropertyChanged(nameof(CritDamage));
            }
        }
        private int _critDamage;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = $"Items/item_{GachaType.ToLower()}_{Name.ToLower()}_{Rarity.ToLower()}";
                Debug.Log("[Thumbnail]: " + path);
                return Resources.Load<Sprite>(path);
            }
        }

        [Binding]
        public Sprite SpriteReview
        {
            get
            {
                switch (GachaType.ToLower())
                {
                    case "cannon":
                        return _Game.Scripts.DB.Database.GetCannonImage(Id);
                    case "ammo":
                        return _Game.Scripts.DB.Database.GetAmmoImage(Id);
                    default:
                        return null;
                }
            }
        }


    }
}