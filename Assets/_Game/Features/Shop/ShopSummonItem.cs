using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using Online;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{

    [Serializable]
    public class CountOfGacha
    {
        public int CountOfGacha_1 = 10;
        public int CountOfGacha_2 = 100;
    }
    [Binding]
    public class ShopSummonItem : SubViewModel
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
        public int PriceAmount { get; set; }

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

        #region Binding Prop: IsActiveButtonKey
        /// <summary>
        /// IsActiveButtonKey
        /// </summary>
        [Binding]
        public bool IsActiveButtonKey
        {
            get => _isActiveButtonKey;
            set
            {
                if (Equals(_isActiveButtonKey, value))
                {
                    return;
                }

                _isActiveButtonKey = value;
                OnPropertyChanged(nameof(IsActiveButtonKey));
            }
        }
        private bool _isActiveButtonKey;
        #endregion

        #region Binding Prop: Interactable
        /// <summary>
        /// Interactable
        /// </summary>
        [Binding]
        public bool Interactable
        {
            get => _interactable;
            set
            {
                if (Equals(_isActiveButtonKey, value))
                {
                    return;
                }

                _interactable = value;
                OnPropertyChanged(nameof(Interactable));
            }
        }
        private bool _interactable;
        #endregion

        public List<string> ListRarity = new List<string>();
        public List<int> ListWeightRarity = new List<int>();
        public List<string> ListNameItem = new List<string>();
        public List<int> ListWeightNameItem = new List<int>();

        public string CurentRarityItemGacha;
        public string CurentOperationItemGacha;
        public ShopSummonViewModel ShopSummonViewModel;
        int _countGacha;

        public void SetUp(ShopSummonViewModel shopSummonViewModel)
        {
            ShopSummonViewModel = shopSummonViewModel;
            IsActiveButtonKey = PriceType == "KE" ? true : false;
            if (IsActiveButtonKey)
                Interactable = PlayfabManager.Instance.Key >= PriceAmount ? true : false;
            else
                Interactable = PlayfabManager.Instance.Gem >= PriceAmount ? true : false;
        }

        [Binding]
        public void GetInfoSummonItem()
        {
            ShopSummonViewModel.IdSummonItemSelected = Id;
            ShopSummonViewModel.PriceAmountItemSelected = PriceAmount;
            ShopSummonViewModel.PriceSummonItemSelected = Price;
            ShopSummonViewModel.IsActiveButtonKey = IsActiveButtonKey;
            ShopSummonViewModel.IsHighlight = CurentRarityItemGacha == "Rare" || CurentRarityItemGacha == "Epic" ? true : false;
        }
    }
}