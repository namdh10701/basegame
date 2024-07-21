using _Base.Scripts.UI.Managers;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Shop
{
    [Binding]
    public class PirateItem : SubViewModel
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
                OnPropertyChanged(nameof(PriceType));
            }
        }
        private string _priceType;
        #endregion

        #region Binding Prop: Price
        /// <summary>
        /// Price
        /// </summary>
        [Binding]
        public string Price
        {
            get => m_price;
            set
            {
                if (Equals(m_price, value))
                {
                    return;
                }

                m_price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        private string m_price;
        #endregion

        #region Binding Prop: Amount
        /// <summary>
        /// Amount
        /// </summary>
        [Binding]
        public int Amount
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
        private int _amount;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = Id == null ? $"Images/ShopPirate/Gems/energy_1" :
                $"Images/ShopPirate/Gems/{Id.ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }

        #region Binding Prop: IsActiveButAd
        /// <summary>
        /// IsActiveButAd
        /// </summary>
        [Binding]
        public bool IsActiveButAd
        {
            get => _isActiveButAd;
            set
            {
                if (Equals(_isActiveButAd, value))
                {
                    return;
                }

                _isActiveButAd = value;
                OnPropertyChanged(nameof(IsActiveButAd));
            }
        }
        private bool _isActiveButAd;
        #endregion

        [Binding]
        public void Buy()
        {
            Debug.Log("Buy");
            var options = new ViewOptions("FightNodeInfoModal", true);
            ModalContainer.Find(ContainerKey.Modals).Push(options);
        }
    }
}