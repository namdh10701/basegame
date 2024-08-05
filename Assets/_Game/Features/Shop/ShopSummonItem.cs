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
            IsActiveButtonKey = PriceType == "vip_key" ? true : false;
        }

        public string GetRandomRarityByWeight()
        {
            if (ListRarity.Count != ListWeightRarity.Count || ListRarity.Count == 0)
                throw new InvalidOperationException("Lists must have the same number of elements and cannot be empty.");


            int totalWeight = ListWeightRarity.Sum();

            System.Random random = new System.Random();
            int randomNumber = random.Next(totalWeight);

            for (int i = 0; i < ListWeightRarity.Count; i++)
            {
                if (randomNumber < ListWeightRarity[i])
                {
                    CurentRarityItemGacha = ListRarity[i];
                    return ListRarity[i];
                }
                randomNumber -= ListWeightRarity[i];
            }
            return null;
        }

        public string GetRandomNameByWeight()
        {
            if (ListNameItem.Count != ListWeightNameItem.Count || ListNameItem.Count == 0)
                throw new InvalidOperationException("Lists must have the same number of elements and cannot be empty.");

            int totalWeight = ListWeightNameItem.Sum();

            System.Random random = new System.Random();
            int randomNumber = random.Next(totalWeight);

            for (int i = 0; i < ListWeightNameItem.Count; i++)
            {
                if (randomNumber < ListWeightNameItem[i])
                {
                    CurentOperationItemGacha = ListNameItem[i];
                    return ListNameItem[i];
                }
                randomNumber -= ListWeightNameItem[i];
            }

            return null;
        }

        [Binding]
        public async void GetIDItemGacha()
        {
            ShopSummonViewModel.ItemsGachaReceived.Clear();
            // var amount = items.Items
            // for (int i = 0; i < Amount; i++)
            // {
            //     ListRarity = GameData.ShopItemTable.GetRarityById(Id);
            //     ListWeightRarity = GameData.ShopItemTable.GetWeightRarityById(Id);
            //     ListNameItem = GameData.ShopRarityTable.GetDataNames(GachaType, GetRandomRarityByWeight());
            //     ListWeightNameItem = GameData.ShopRarityTable.GetWeights(GachaType, CurentRarityItemGacha);

            //     GetRandomNameByWeight();

            //     var IdGachaItem = GameData.ShopRarityTable.GetIdByNameAndRarity(CurentOperationItemGacha, CurentRarityItemGacha);

            //     ShopItemGachaReceived shopItemGachaReceived = new ShopItemGachaReceived();
            //     shopItemGachaReceived.IdItemGacha = IdGachaItem;
            //     shopItemGachaReceived.Operation = CurentOperationItemGacha;
            //     shopItemGachaReceived.Slot = GameData.CannonTable.GetSlotByName(CurentOperationItemGacha);
            //     shopItemGachaReceived.GachaType = GachaType;
            //     shopItemGachaReceived.Rarity = CurentRarityItemGacha;
            //     shopItemGachaReceived.IsHighLight = CurentRarityItemGacha == "Rare" || CurentRarityItemGacha == "Epic" ? true : false;
            //     ShopSummonViewModel.ItemsGachaReceived.Add(shopItemGachaReceived);
            //     await UniTask.Delay(300);

            // }
            ShopSummonViewModel.CurrentIndexItemReview = 0;
            ShopSummonViewModel.OnChangeCurrentIndexItemReview(0);
            SetIdSummonItem();
        }

        [Binding]
        public void SetIdSummonItem()
        {
            ShopSummonViewModel.IdSummonItemSelected = Id;
            ShopSummonViewModel.PriceTypeSummonItemSelected = PriceType;
            ShopSummonViewModel.PriceSummonItemSelected = Price;
            ShopSummonViewModel.IsActiveButtonKey = IsActiveButtonKey;
            ShopSummonViewModel.IsHighlight = CurentRarityItemGacha == "Rare" || CurentRarityItemGacha == "Epic" ? true : false;
            ShopSummonViewModel.IsHighlight = true;
        }
    }
}