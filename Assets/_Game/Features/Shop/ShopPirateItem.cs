using _Base.Scripts.UI.Managers;
using _Game.Features.Ads;
using _Game.Scripts.UI;
using Online;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopPirateItem : SubViewModel
    {
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private string _Reward_Energy_1 = "ca-app-pub-4412764125039323/4954821192";
        private string _Reward_Gold_1 = "ca-app-pub-4412764125039323/6143947655";
        private string _Reward_Daily_0 = "ca-app-pub-4412764125039323/3339892161";
        private string _Reward_Weekly_0 = "ca-app-pub-4412764125039323/1068952045";
#elif UNITY_IPHONE
        private string _Reward_Energy_1 = "ca-app-pub-4412764125039323/4954821192";
        private string _Reward_Gold_1 = "ca-app-pub-4412764125039323/6143947655";
        private string _Reward_Daily_0 = "ca-app-pub-4412764125039323/3339892161";
        private string _Reward_Weekly_0 = "ca-app-pub-4412764125039323/1068952045";
#else
        private string _Reward_Energy_1 = "ca-app-pub-4412764125039323/4954821192";
        private string _Reward_Gold_1 = "ca-app-pub-4412764125039323/6143947655";
        private string _Reward_Daily_0 = "ca-app-pub-4412764125039323/3339892161";
        private string _Reward_Weekly_0 = "ca-app-pub-4412764125039323/1068952045";
#endif
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

        #region Binding Prop: ShopType
        /// <summary>
        /// ShopType
        /// </summary>
        [Binding]
        public string ShopType
        {
            get => _shoptype;
            set
            {
                if (Equals(_shoptype, value))
                {
                    return;
                }

                _shoptype = value;
                OnPropertyChanged(nameof(ShopType));
            }
        }
        private string _shoptype;
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
                var path = Id == null || ShopType == null ? $"Images/ShopPirate/Gems/energy_1" : $"Images/ShopPirate/{ShopType.ToLower()}/{Id.ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }

        #region Binding: ItemReceived
        private ObservableList<ShopPirateItemReceived> itemsDailyReceived = new ObservableList<ShopPirateItemReceived>();
        [Binding]
        public ObservableList<ShopPirateItemReceived> ItemsDailyReceived => itemsDailyReceived;
        #endregion

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
        public async void OnClickBuy()
        {
            await PlayfabManager.Instance.BuyStoreItem(Id);
        }

        [Binding]
        public void OnClickAd()
        {
            string adUnitId = "";
            switch (Id)
            {
                case "com.pirate.ship.energy1":
                    adUnitId = _Reward_Energy_1;
                    break;
                case "com.pirate.ship.gold1":
                    adUnitId = _Reward_Gold_1;
                    break;
                case "com.pirate.ship.daily0":
                    adUnitId = _Reward_Daily_0;
                    break;
                case "com.pirate.ship.weekly0":
                    adUnitId = _Reward_Weekly_0;
                    break;
            }

            AdsManager.Instance.LoadRewardedAd(adUnitId, async () =>
            {
                await PlayfabManager.Instance.BuyStoreItem(Id);
            });
        }
    }
}