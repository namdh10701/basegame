using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Shop
{
    [Binding]
    public class ShopPackageSummonItem : SubViewModel
    {
        [Binding]
        public string PackageName { get; set; }
        #region Binding: SummonItems
        private ObservableList<ShopSummonItem> summonItems = new ObservableList<ShopSummonItem>();

        [Binding]
        public ObservableList<ShopSummonItem> SummonItems => summonItems;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = PackageName == null ? $"Images/ShopSummon/ammo_gacha_1" : $"Images/ShopSummon/{PackageName}";
                return Resources.Load<Sprite>(path);
            }
        }

    }
}