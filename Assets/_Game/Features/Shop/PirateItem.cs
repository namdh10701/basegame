using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Shop
{

    public enum PirateItemType
    {
        GOLD,
        GEM,
    }
    public class InventoryPirateItemDto
    {
        public string Id;
        public string Name;
        public PirateItemType ItemType;
    }
    [Binding]
    public class PirateItem : SubViewModel
    {
        public PirateViewModel PirateViewModel { get; set; }

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

        #region Binding Prop: Price

        /// <summary>
        /// Price
        /// </summary>
        [Binding]
        public float Price
        {
            get => m_price;
            set
            {
                if (Equals(m_price, value))
                {
                    return;
                }

                m_price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private float m_price;

        #endregion

        #region Binding Prop: ItemType

        /// <summary>
        /// ItemType
        /// </summary>
        [Binding]
        public PirateItemType Type
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

        private PirateItemType m_type;

        #endregion

        [Binding]
        public Sprite Thumbnail => Resources.Load<Sprite>($"Images/Cannon/cannon_{Id}");

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

        #region Binding Prop: IsEquipped

        /// <summary>
        /// IsEquipped
        /// </summary>
        [Binding]
        public bool IsEquipped
        {
            get => _isEquipped;
            set
            {
                if (Equals(_isEquipped, value))
                {
                    return;
                }

                _isEquipped = value;
                OnPropertyChanged(nameof(IsEquipped));
            }
        }

        private bool _isEquipped;

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

        [Binding]
        public void ShowItemDetail()
        {
            PopupManager.Instance.ShowPopup<GearInfoPopup>();
        }

        [Binding]
        public void SetAsHighLightItem()
        {
            PirateViewModel.HighlightItem = this;
        }

        [Binding]
        public void Buy()
        {
            Debug.Log("Buy");
            var options = new ViewOptions("FightNodeInfoModal", true);
            ModalContainer.Find(ContainerKey.Modals).Push(options);
        }
    }
}