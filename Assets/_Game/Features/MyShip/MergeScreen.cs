using System.Collections;
using System.Collections.Generic;
using _Game.Features.Inventory;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;
namespace _Game.Features.MergeScreen
{
    [Binding]
    public class MergeScreen : RootViewModel
    {

        #region Binding Prop: IsActiveSuccesFul

        /// <summary>
        /// IsActiveSuccesFul
        /// </summary>
        [Binding]
        public bool IsActiveSuccesFul
        {
            get => _isActiveSuccesFul
            ;
            set
            {
                if (Equals(_isActiveSuccesFul, value))
                {
                    return;
                }
                _isActiveSuccesFul = value;
                OnPropertyChanged(nameof(IsActiveSuccesFul));
            }
        }

        private bool _isActiveSuccesFul;
        #endregion

        #region Binding Prop: CanMerge

        /// <summary>
        /// CanMerge
        /// </summary>
        [Binding]
        public bool CanMerge
        {
            get => _canMerge
            ;
            set
            {
                if (Equals(_canMerge, value))
                {
                    return;
                }
                _canMerge = value;
                OnPropertyChanged(nameof(CanMerge));
            }
        }

        private bool _canMerge;
        #endregion

        #region Binding Prop: NumberItem

        /// <summary>
        /// NumberItem
        /// </summary>
        [Binding]
        public string NumberItems
        {
            get => _numberItems
            ;
            set
            {
                if (Equals(_numberItems, value))
                {
                    return;
                }
                _numberItems = value;
                OnPropertyChanged(nameof(NumberItems));
            }
        }

        private string _numberItems;
        #endregion

        public int NumberRequired;

        #region Binding Prop: NumberItemRequired

        /// <summary>
        /// NumberItem
        /// </summary>
        [Binding]
        public string NumberItemsRequired
        {
            get => _numberItemsRequired
            ;
            set
            {
                if (Equals(_numberItemsRequired, value))
                {
                    return;
                }
                _numberItemsRequired = value;
                OnPropertyChanged(nameof(NumberItemsRequired));
            }
        }

        private string _numberItemsRequired;
        #endregion

        [Binding]
        public string IdItemMerge { get; set; }

        [Binding]
        public ItemType TypeItemMerge { get; set; }

        #region Binding Prop: SpriteItemMerge
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite SpriteItemMerge
        {
            get
            {
                switch (TypeItemMerge)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetCannonImage(IdItemMerge);
                    case ItemType.CREW:
                        return _Game.Scripts.DB.Database.GetCrewImage(IdItemMerge);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetAmmoImage(IdItemMerge);
                    default:
                        return null;
                }
            }
        }
        #endregion

        [Binding]
        public string IdItemTarget { get; set; }

        [Binding]
        public ItemType TypeItemTarget { get; set; }

        #region Binding Prop: SpriteItemMerge
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite SpriteItemTarget
        {
            get
            {
                switch (TypeItemTarget)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetCannonImage(IdItemTarget);
                    case ItemType.CREW:
                        return _Game.Scripts.DB.Database.GetCrewImage(IdItemTarget);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetAmmoImage(IdItemTarget);
                    default:
                        return null;
                }
            }
        }
        #endregion

    }
}
