using System.Collections;
using System.Collections.Generic;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.InventoryItemInfo
{
    [Binding]
    public class SkillInvetoryItem : SubViewModel
    {
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
                OnPropertyChanged(nameof(Rarity));
            }
        }
        private Rarity _rarity;
        #endregion

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

        #region Binding Prop: Details
        /// <summary>
        /// Details
        /// </summary>
        [Binding]
        public string Details
        {
            get => _details;
            set
            {
                if (Equals(_details, value))
                {
                    return;
                }

                _details = value;
                OnPropertyChanged(nameof(Details));
            }
        }
        private string _details;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                var itemType = Type.ToString().ToLower();
                var name = Name.ToLower();
                var itemRarity = Rarity.ToString().ToLower();
                var path = $"Items/item_{itemType}_{itemRarity}_{name}";
                return Resources.Load<Sprite>(path);
            }
        }

    }
}
