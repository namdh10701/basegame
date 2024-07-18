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
    public class ShopItemGachaReceived : SubViewModel
    {
        [Binding]
        public string Id { get; set; }

        #region Binding Prop: Rarity
        /// <summary>
        /// Rarity
        /// </summary>
        [Binding]
        public string Rarity
        {
            get => _rarity;
            set
            {
                if (Equals(_rarity, value))
                {
                    return;
                }

                _rarity = value;
                OnPropertyChanged(nameof(Rarity));
            }
        }
        private string _rarity;
        #endregion


        #region Binding Prop: Rarity
        /// <summary>
        /// Rarity
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

        #region Binding Prop: Name
        /// <summary>
        /// Name
        /// </summary>
        [Binding]
        public string Name
        {
            get => _name;
            set
            {
                if (Equals(_name, value))
                {
                    return;
                }

                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _name;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = $"Items/item_{GachaType.ToLower()}_{Name.ToLower()}_{Rarity.ToLower()}";
                Debug.Log("[Thumbnail]: " + path);
                return Resources.Load<Sprite>(path);
            }
        }

    }
}