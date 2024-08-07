using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopItemGachaReceived : SubViewModel
    {
        [Binding]
        public string IdItemGacha { get; set; }

        [Binding]
        public string Id { get; set; }

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
        public string Operation
        {
            get => _operation;
            set
            {
                if (Equals(_operation, value))
                {
                    return;
                }

                _operation = value;
                OnPropertyChanged(nameof(Operation));
            }
        }
        private string _operation;
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
            }
        }
        private bool _isSelected;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = GachaType == null || Operation == null || Rarity == null ? $"Images/Items/item_ammo_arrow_common" :
                 $"Images/Items/item_{GachaType.ToLower()}_{Operation.ToLower()}_{Rarity.ToLower()}";
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
                        return _Game.Scripts.DB.Database.GetSpriteCannon(Id);
                    case "ammo":
                        return _Game.Scripts.DB.Database.GetSpriteAmmo(Id);
                    default:
                        return null;
                }
            }
        }


    }
}