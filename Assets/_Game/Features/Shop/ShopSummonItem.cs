using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using _Game.Scripts.UI;
using ExitGames.Client.Photon.StructWrapping;
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

        // [Binding]
        // public Sprite Thumbnail
        // {
        //     get
        //     {
        //         switch (_gachaType)
        //         {
        //             case "cannon":
        //                 return _Game.Scripts.DB.Database.GetCannonImage(IdGachaItem);
        //             case "ammo":
        //                 return _Game.Scripts.DB.Database.GetCrewImage(IdGachaItem);
        //             default:
        //                 Debug.LogWarning("null image");
        //                 return Resources.Load<Sprite>("Images/Common/icon_plus");
        //         }
        //     }
        // }

        public List<string> ListRarity = new List<string>();
        public List<int> ListWeightRarity = new List<int>();
        public List<string> ListNameItem = new List<string>();
        public List<int> ListWeightNameItem = new List<int>();

        public string CurentRarityItemGacha;
        public string CurentNameItemGacha;
        public ShopSummonViewModel ShopSummonViewModel;
        int _countGacha;

        public void SetUp(ShopSummonViewModel shopSummonViewModel)
        {
            ShopSummonViewModel = shopSummonViewModel;
        }

        private int GetNumberGacha()
        {
            if (Id == "gacha_cannon_1" || Id == "gacha_ammo_1")
                return SaveSystem.GameSave.CountOfGacha.CountOfGacha_1;
            else
                return SaveSystem.GameSave.CountOfGacha.CountOfGacha_2;
        }
        private void SaveCountGacha()
        {
            if (Id == "gacha_cannon_1" || Id == "gacha_ammo_1")
                SaveSystem.GameSave.CountOfGacha.CountOfGacha_1 = _countGacha;
            else
                SaveSystem.GameSave.CountOfGacha.CountOfGacha_2 = _countGacha;
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

                    if (CurentRarityItemGacha == "Rare" || CurentRarityItemGacha == "Epic")
                    {
                        _countGacha = GetNumberGacha();
                    }
                    else if (_countGacha == 1)
                    {
                        if (CurentRarityItemGacha == "Common" || CurentRarityItemGacha == "Good" || CurentRarityItemGacha == "Rare")
                        {
                            CurentRarityItemGacha = "Rare";
                            _countGacha = GetNumberGacha();
                        }
                    }
                    else
                        _countGacha--;

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
                    CurentNameItemGacha = ListNameItem[i];
                    return ListNameItem[i];
                }
                randomNumber -= ListWeightNameItem[i];
            }

            return null;
        }

        [Binding]
        public void GetIDItemGacha()
        {
            _countGacha = GetNumberGacha();
            ShopSummonViewModel.ItemsGachaReceived.Clear();
            for (int i = 0; i < Amount; i++)
            {
                ListRarity = GameData.ShopItemTable.GetRarityById(Id);
                ListWeightRarity = GameData.ShopItemTable.GetWeightRarityById(Id);
                ListNameItem = GameData.ShopRarityTable.GetDataNames(GachaType, GetRandomRarityByWeight());
                ListWeightNameItem = GameData.ShopRarityTable.GetWeights(GachaType, CurentRarityItemGacha);

                GetRandomNameByWeight();

                var IdGachaItem = GameData.ShopRarityTable.GetIdByNameAndRarity(CurentNameItemGacha, CurentRarityItemGacha);

                ShopItemGachaReceived shopItemGachaReceived = new ShopItemGachaReceived();
                shopItemGachaReceived.IdItemGacha = IdGachaItem;
                shopItemGachaReceived.Name = CurentNameItemGacha;
                shopItemGachaReceived.GachaType = GachaType;
                shopItemGachaReceived.Rarity = CurentRarityItemGacha;
                ShopSummonViewModel.ItemsGachaReceived.Add(shopItemGachaReceived);

            }

            ShopSummonViewModel.OnClickToCotinue();
            SaveCountGacha();
            SaveSystem.SaveGame();
        }

        [Binding]
        public void SetIdSummonItem()
        {
            ShopSummonViewModel.IdSummonItemSelected = Id;
        }
    }
}