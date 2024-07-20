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

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = GachaType == null || Name == null || Rarity == null ? $"Items/item_ammo_arrow_common" :
                 $"Items/item_{GachaType.ToLower()}_{Name.ToLower()}_{Rarity.ToLower()}";
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