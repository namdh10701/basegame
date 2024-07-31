using System;
using _Game.Features.Inventory;
using _Game.Scripts.DB;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;
namespace _Game.Features.InventoryCustomScreen
{
    [Binding]
    public class AttachInfoItem : SubViewModel
    {
        [Binding]
        public string Id { get; set; }

        #region Binding Prop: ItemType
        /// <summary>
        /// ItemType
        /// </summary>
        [Binding]
        public ItemType Type
        {
            get => m_type;
            set
            {
                if (Equals(m_type, value))
                {
                    return;
                }

                m_type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        private ItemType m_type;
        #endregion

        #region Binding Prop: Rarity
        /// <summary>
        /// Rarity
        /// </summary>
        [Binding]
        public Rarity Rarity
        {
            get => _rarity;
            set
            {
                if (Equals(_rarity, value))
                {
                    return;
                }

                _rarity = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        private Rarity _rarity;
        #endregion

        #region Binding Prop: Thumbnail
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var path = Id == null ? $"Items/item_misc_eq2_com" : $"Items/item_misc_{Id.ToString().ToLower()}_{Rarity.ToString().ToLower()}";
                // var path = Id == null ? $"Items/item_misc_eq2_com" : $"Items/item_misc_eq2_com";
                return Resources.Load<Sprite>(path);
            }
        }
        #endregion

        #region Binding Prop: IsHighlight
        private bool _isHighlight;

        /// <summary>
        /// IsHighlight
        /// </summary>
        [Binding]
        public bool IsHighlight
        {
            get => _isHighlight;
            set
            {
                if (Equals(_isHighlight
                , value))
                {
                    return;
                }
                _isHighlight
                 = value;
                OnPropertyChanged(nameof(IsHighlight));

            }
        }

        internal void Setup()
        {
            OnPropertyChanged(nameof(Thumbnail));

        }
        #endregion

    }
}
