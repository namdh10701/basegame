using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.UI.Managers;
using _Game.Features.Inventory;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{
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

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                switch (_gachaType)
                {
                    case "cannon":
                        return _Game.Scripts.DB.Database.GetCannonImage(IdGachaItem);
                    case "ammo":
                        return _Game.Scripts.DB.Database.GetCrewImage(IdGachaItem);
                    default:
                        Debug.LogWarning("null image");
                        return Resources.Load<Sprite>("Images/Common/icon_plus");
                }
            }
        }

        #region Binding Prop: IdGachaItem
        /// <summary>
        /// IdGachaItem
        /// </summary>
        [Binding]
        public string IdGachaItem
        {
            get => _idGachaItem;
            set
            {
                if (Equals(_idGachaItem, value))
                {
                    return;
                }

                _idGachaItem = value;
                OnPropertyChanged(nameof(IdGachaItem));
            }
        }
        private string _idGachaItem;
        #endregion

        public List<string> ListRarity = new List<string>();
        public List<int> ListWeightRarity = new List<int>();
        public List<string> ListNameItem = new List<string>();
        public List<int> ListWeightNameItem = new List<int>();

        public string CurentRarityItemGacha;
        public string CurentNameItemGacha;

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
                    CurentNameItemGacha = ListRarity[i];
                    return ListRarity[i];
                }
                randomNumber -= ListWeightNameItem[i];
            }

            return null;
        }

        public void GetIDItemGacha()
        {
            IdGachaItem = ShopDataRarity.Instance.GetIdByNameAndRarity(CurentNameItemGacha, CurentRarityItemGacha);
        }
    }
}