using _Base.Scripts.UI.Managers;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopGemItem : SubViewModel
    {
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

        #region Binding Prop: PackSize

        /// <summary>
        /// PackSize
        /// </summary>
        [Binding]
        public PackSize PackSize
        {
            get => m_PackSize;
            set
            {
                if (Equals(m_PackSize, value))
                {
                    return;
                }

                m_PackSize = value;
                OnPropertyChanged(nameof(PackSize));
            }
        }

        private PackSize m_PackSize;

        #endregion

        [Binding]
        public Sprite Thumbnail => Resources.Load<Sprite>($"Images/Cannon/cannon_{Id}");

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

        #region Binding Prop: Price
        /// <summary>
        /// Price
        /// </summary>
        [Binding]
        public string Price
        {
            get => _price;
            set
            {
                if (Equals(_price, value))
                {
                    return;
                }

                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        private string _price;
        #endregion


        #region Binding Prop: Amount
        /// <summary>
        /// Amount
        /// </summary>
        [Binding]
        public string Amount
        {
            get => _amount;
            set
            {
                if (Equals(_amount, value))
                {
                    return;
                }

                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        private string _amount;
        #endregion

        #region Binding Prop: PriceType
        /// <summary>
        /// PriceType
        /// </summary>
        [Binding]
        public string PriceType
        {
            get => _priceType;
            set
            {
                if (Equals(_priceType, value))
                {
                    return;
                }

                _priceType = value;
                IsActiveButAds = _priceType == "ads" ? true : false;
                OnPropertyChanged(nameof(PriceType));
            }
        }
        private string _priceType;
        #endregion


        #region Binding Prop: IsActiveButAds
        /// <summary>
        /// IsActiveButAds
        /// </summary>
        [Binding]
        public bool IsActiveButAds
        {
            get => _isActiveButAds;
            set
            {
                if (Equals(_isActiveButAds, value))
                {
                    return;
                }

                _isActiveButAds = value;
                OnPropertyChanged(nameof(IsActiveButAds));
            }
        }
        private bool _isActiveButAds;
        #endregion


        void OnEnable()
        {
        }


        [Binding]
        public void ShowItemDetail()
        {
            PopupManager.Instance.ShowPopup<GearInfoPopup>();
        }

        [Binding]
        public void SetAsHighLightItem()
        {
            // InventoryViewModel.HighlightItem = this;
        }
    }
}