using System.Collections.Generic;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopSummonViewModel : RootViewModel
    {
        #region Binding: SummonItems
        private ObservableList<ShopSummonItem> summonItems = new ObservableList<ShopSummonItem>();

        [Binding]
        public ObservableList<ShopSummonItem> SummonItems => summonItems;
        #endregion

        #region Binding: SummonItems
        private ObservableList<ShopItemGachaReceived> itemsGachaReceived = new ObservableList<ShopItemGachaReceived>();

        [Binding]
        public ObservableList<ShopItemGachaReceived> ItemsGachaReceived => itemsGachaReceived;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = $"Items/item_{GachaTypeItemReview.ToLower()}_{NameItemReview.ToLower()}_{RarityItemReview.ToLower()}";
                Debug.Log("[Thumbnail]: " + path);
                return Resources.Load<Sprite>(path);
            }
        }

        #region Binding Prop: GachaType
        /// <summary>
        /// GachaType
        /// </summary>
        [Binding]
        public string GachaTypeItemReview
        {
            get => _gachaTypeItemReview;
            set
            {
                if (Equals(_gachaTypeItemReview, value))
                {
                    return;
                }

                _gachaTypeItemReview = value;
                OnPropertyChanged(nameof(GachaTypeItemReview));
            }
        }
        private string _gachaTypeItemReview;
        #endregion

        #region Binding Prop: NameItemReview
        /// <summary>
        /// NameItemReview
        /// </summary>
        [Binding]
        public string NameItemReview
        {
            get => _nameItemReview;
            set
            {
                if (Equals(_nameItemReview, value))
                {
                    return;
                }

                _nameItemReview = value;
                OnPropertyChanged(nameof(NameItemReview));
            }
        }
        private string _nameItemReview;
        #endregion

        #region Binding Prop: SlotItemReview
        /// <summary>
        /// SlotItemReview
        /// </summary>
        [Binding]
        public string SlotItemReview
        {
            get => _slotItemReview;
            set
            {
                if (Equals(_slotItemReview, value))
                {
                    return;
                }

                _slotItemReview = value;
                OnPropertyChanged(nameof(SlotItemReview));
            }
        }
        private string _slotItemReview;
        #endregion

        #region Binding Prop: RarityItemReview
        /// <summary>
        /// RarityItemReview
        /// </summary>
        [Binding]
        public string RarityItemReview
        {
            get => _rarityItemReview;
            set
            {
                if (Equals(_rarityItemReview, value))
                {
                    return;
                }

                _rarityItemReview = value;
                OnPropertyChanged(nameof(RarityItemReview));
            }
        }
        private string _rarityItemReview;
        #endregion

        #region Binding Prop: CurrentIndexItemReview
        /// <summary>
        /// CurrentIndexItemReview
        /// </summary>
        [Binding]
        public int CurrentIndexItemReview
        {
            get => _currentIndexItemReview;
            set
            {
                if (Equals(_currentIndexItemReview, value))
                {
                    return;
                }

                _currentIndexItemReview = value;
                OnPropertyChanged(nameof(CurrentIndexItemReview));
            }
        }
        private int _currentIndexItemReview = -1;
        #endregion

        #region Binding Prop: IsHighlight
        /// <summary>
        /// IsHighlight
        /// </summary>
        [Binding]
        public bool IsHighlight
        {
            get => _isHighLight;
            set
            {
                if (Equals(_isHighLight, value))
                {
                    return;
                }

                _isHighLight = value;
                OnPropertyChanged(nameof(IsHighlight));
            }
        }
        private bool _isHighLight;
        #endregion

        #region Binding Prop: IsActivePopupConfirm
        /// <summary>
        /// IsActivePopupConfirm
        /// </summary>
        [Binding]
        public bool IsActivePopupConfirm
        {
            get => _isActivePopupConfirm;
            set
            {
                if (Equals(_isActivePopupConfirm, value))
                {
                    return;
                }

                _isActivePopupConfirm = value;
                OnPropertyChanged(nameof(IsActivePopupConfirm));
            }
        }
        private bool _isActivePopupConfirm;
        #endregion

        #region Binding Prop: IsActivePopupLoading
        /// <summary>
        /// IsActivePopupLoading
        /// </summary>
        [Binding]
        public bool IsActivePopupLoading
        {
            get => _isActivePopupLoading;
            set
            {
                if (Equals(_isActivePopupLoading, value))
                {
                    return;
                }

                _isActivePopupLoading = value;
                OnPropertyChanged(nameof(IsActivePopupLoading));
            }
        }
        private bool _isActivePopupLoading;
        #endregion

        #region Binding Prop: IsActivePopupAnimOpenBox
        /// <summary>
        /// IsActivePopupAnimOpenBox
        /// </summary>
        [Binding]
        public bool IsActivePopupAnimOpenBox
        {
            get => _isActivePopupAnimOpenBox;
            set
            {
                if (Equals(_isActivePopupAnimOpenBox, value))
                {
                    return;
                }

                _isActivePopupAnimOpenBox = value;
                OnPropertyChanged(nameof(IsActivePopupAnimOpenBox));
            }
        }
        private bool _isActivePopupAnimOpenBox;
        #endregion

        #region Binding Prop: IsActivePopupReceived
        /// <summary>
        /// IsActivePopupReceived
        /// </summary>
        [Binding]
        public bool IsActivePopupReceived
        {
            get => _isActivePopupReceived;
            set
            {
                if (Equals(_isActivePopupReceived, value))
                {
                    return;
                }

                _isActivePopupReceived = value;
                OnPropertyChanged(nameof(IsActivePopupReceived));
            }
        }
        private bool _isActivePopupReceived;
        #endregion

        List<ShopListingTableRecord> _shopDataSummons = new List<ShopListingTableRecord>();
        public string IdSummonItemSelected;
        private void OnEnable()
        {
            LoadDataShop();
            InitializeShopSummon();
        }

        private void LoadDataShop()
        {
            _shopDataSummons = GameData.ShopListingTable.GetData(ShopType.Gacha);
        }

        private void InitializeShopSummon()
        {
            foreach (var item in _shopDataSummons)
            {
                ShopSummonItem shopSummonItem = new ShopSummonItem();
                shopSummonItem.Id = item.ItemId;
                shopSummonItem.Price = item.PriceAmount.ToString();
                shopSummonItem.GachaType = item.GachaType;
                shopSummonItem.Amount = GameData.ShopItemTable.GetAmountById(item.ItemId);

                shopSummonItem.SetUp(this);
                SummonItems.Add(shopSummonItem);
            }
        }

        [Binding]
        public void OnClickToCotinue()
        {
            if (CurrentIndexItemReview <= ItemsGachaReceived.Count)
            {
                CurrentIndexItemReview++;
                GachaTypeItemReview = ItemsGachaReceived[CurrentIndexItemReview].GachaType;
                NameItemReview = ItemsGachaReceived[CurrentIndexItemReview].Name;
                RarityItemReview = ItemsGachaReceived[CurrentIndexItemReview].Rarity;
                SlotItemReview = ItemsGachaReceived[CurrentIndexItemReview].Shape;
                IsHighlight = RarityItemReview == "Rare" || RarityItemReview == "Epic" ? true : false;
                OnPropertyChanged(nameof(Thumbnail));
            }
        }

        [Binding]
        public void OnClickStartGacha()
        {
            IsActivePopupConfirm = true;
        }

        [Binding]
        public async void OnClickConfirmGacha()
        {
            IsActivePopupConfirm = false;
            IsActivePopupLoading = true;
            await UniTask.Delay(3000);
            IsActivePopupLoading = false;
            // IsActivePopupAnimOpenBox = true;
            IsActivePopupReceived = true;

            foreach (var item in SummonItems)
            {
                if (item.Id == IdSummonItemSelected)
                {
                    item.GetIDItemGacha();
                    CurrentIndexItemReview = -1;
                }
            }
        }
    }
}