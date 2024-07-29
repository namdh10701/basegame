using _Base.Scripts.UI.Managers;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Online;
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

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = Id == null ? $"Images/ShopGem/ammo_gacha_1" : $"Images/ShopGem/{Id.ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }

        [Binding]
        public void ShowItemDetail()
        {
            PopupManager.Instance.ShowPopup<GearInfoPopup>();
        }

        [Binding]
        public void OnClickBuy()
        {
            PlayfabManager.Instance.BuyStoreItem(Id, (success) =>
            {

            });
        }
    }
}