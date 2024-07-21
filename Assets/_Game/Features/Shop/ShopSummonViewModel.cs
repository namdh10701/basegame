using System.Collections.Generic;
using System.Reflection;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.Utils;
using _Game.Scripts.Gameplay;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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

        #region Binding: ItemStats
        private ObservableList<ItemStat> itemStats = new ObservableList<ItemStat>();

        [Binding]
        public ObservableList<ItemStat> ItemStats => itemStats;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = GachaTypeItemReview == null || NameItemReview == null || RarityItemReview == null ? $"Items/item_ammo_arrow_common" :
                 $"Items/item_{GachaTypeItemReview.ToLower()}_{NameItemReview.ToLower()}_{RarityItemReview.ToLower()}";
                return Resources.Load<Sprite>(path);
            }
        }

        [Binding]
        public Sprite SpriteReview
        {
            get
            {
                switch (GachaTypeItemReview.ToLower())
                {
                    case "cannon":
                        return _Game.Scripts.DB.Database.GetCannonImage(IdItemReview);
                    case "ammo":
                        return _Game.Scripts.DB.Database.GetAmmoImage(IdItemReview);
                    default:
                        return null;
                }
            }
        }

        #region Binding Prop: IdItemReview
        /// <summary>
        /// IdItemReview
        /// </summary>
        [Binding]
        public string IdItemReview
        {
            get => _idItemReview;
            set
            {
                if (Equals(_idItemReview, value))
                {
                    return;
                }

                _idItemReview = value;
                OnPropertyChanged(nameof(IdItemReview));
            }
        }
        private string _idItemReview;
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
                OnChangeCurrentIndexItemReview(CurrentIndexItemReview);
            }
        }
        private int _currentIndexItemReview;
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

        #region Binding Prop: IsActivePopupReview
        /// <summary>
        /// IsActivePopupReview
        /// </summary>
        [Binding]
        public bool IsActivePopupReview
        {
            get => _isActivePopupReview;
            set
            {
                if (Equals(_isActivePopupReview, value))
                {
                    return;
                }

                _isActivePopupReview = value;
                OnPropertyChanged(nameof(IsActivePopupReview));
            }
        }
        private bool _isActivePopupReview;
        #endregion

        List<ShopListingTableRecord> _shopDataSummons = new List<ShopListingTableRecord>();
        public string IdSummonItemSelected;
        public string PriceTypeSummonItemSelected;
        public RectTransform Box;
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
                shopSummonItem.PriceType = item.PriceType;
                shopSummonItem.SetUp(this);
                SummonItems.Add(shopSummonItem);
            }
        }

        [Binding]
        public void OnClickToCotinue()
        {
            if (CurrentIndexItemReview <= ItemsGachaReceived.Count - 1)
            {
                CurrentIndexItemReview++;
            }
        }

        public void OnChangeCurrentIndexItemReview(int currentIndexItemReview)
        {
            IdItemReview = ItemsGachaReceived[currentIndexItemReview].IdItemGacha;
            GachaTypeItemReview = ItemsGachaReceived[currentIndexItemReview].GachaType;
            NameItemReview = ItemsGachaReceived[currentIndexItemReview].Name;
            RarityItemReview = ItemsGachaReceived[currentIndexItemReview].Rarity;
            IsHighlight = RarityItemReview == "Rare" || RarityItemReview == "Epic" ? true : false;
            OnPropertyChanged(nameof(SpriteReview));
            OnPropertyChanged(nameof(Thumbnail));
            SetDataItemStat();
        }

        private void SetDataItemStat()
        {
            itemStats.Clear();
            DataTableRecord dataTableRecord;
            if (GachaTypeItemReview == "cannon")
            {
                SlotItemReview = GameData.CannonTable.GetShapeByName(NameItemReview);
                dataTableRecord = GameData.CannonTable.GetDataTableRecord(NameItemReview, RarityItemReview);
            }
            else
            {
                SlotItemReview = GameData.AmmoTable.GetShapeByName(NameItemReview);
                dataTableRecord = GameData.AmmoTable.GetDataTableRecord(NameItemReview, RarityItemReview);
            }

            foreach (var item in dataTableRecord.GetType().GetProperties(BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance))
            {
                var stat = item.GetCustomAttribute<StatAttribute>();
                if (stat == null)
                    continue;

                ItemStat itemStat = new ItemStat();
                itemStat.NameProperties = stat.Name;
                itemStat.Value = item.GetValue(dataTableRecord).ToString();
                itemStats.Add(itemStat);
            }

        }

        [Binding]
        public void OnClickStartGacha()
        {
            IsActivePopupConfirm = true;
        }

        [Binding]
        public void OnClickOperPopupReview()
        {
            IsActivePopupReview = true;
            IsActivePopupReceived = false;
        }

        [Binding]
        public async void OnClickConfirmGacha()
        {
            IOC.Resolve<MainViewModel>().IsActiveBotNavBar = false;
            IsActivePopupConfirm = false;
            IsActivePopupLoading = true;
            await UniTask.Delay(1);
            IsActivePopupLoading = false;
            IsActivePopupAnimOpenBox = true;
            Box.anchoredPosition = new Vector2(0, 941);
            Box.DOAnchorPosY(-228, 1, false).OnComplete(() =>
            {
                IsActivePopupAnimOpenBox = false;
                IsActivePopupReceived = true;

                foreach (var item in SummonItems)
                {
                    if (item.Id == IdSummonItemSelected && item.PriceType == PriceTypeSummonItemSelected)
                    {
                        item.GetIDItemGacha();
                    }
                }
            });
        }

        [Binding]
        public void OnClickAgain()
        {
            ItemsGachaReceived.Clear();
            IsActivePopupReview = false;
            IsActivePopupReceived = false;
            OnClickConfirmGacha();
        }

        [Binding]
        public void OnClickConfirm()
        {
            IOC.Resolve<MainViewModel>().IsActiveBotNavBar = true;
            IsActivePopupConfirm = false;
            IsActivePopupLoading = false;
            IsActivePopupAnimOpenBox = false;
            IsActivePopupReview = false;
            IsActivePopupReceived = false;
        }


    }
}