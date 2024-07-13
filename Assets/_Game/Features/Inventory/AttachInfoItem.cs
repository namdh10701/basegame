using _Game.Features.Inventory;
using _Game.Scripts.DB;
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

        #region Binding Prop: Thumbnail
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                switch (Type)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetCannonImage(Id);
                    case ItemType.CREW:
                        return _Game.Scripts.DB.Database.GetCrewImage(Id);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetAmmoImage(Id);
                    default:
                        return null;
                }
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
        #endregion

    }
}
