using _Game.Features.Inventory;
using _Game.Scripts.DB;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;
namespace _Game.Features.InventoryCustomScreen
{
    [Binding]
    public class SkinInfoItem : SubViewModel
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

        #region Binding Prop: Details

        /// <summary>
        /// Details
        /// </summary>
        [Binding]
        public string Details
        {
            get => m_details;
            set
            {
                if (Equals(m_details, value))
                {
                    return;
                }

                m_details = value;
                OnPropertyChanged(nameof(Details));
            }
        }

        private string m_details;

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

        #region Binding Prop: IsUse
        private bool _isUse;

        /// <summary>
        /// IsActiveBG
        /// </summary>
        [Binding]
        public bool IsUse
        {
            get => _isUse;
            set
            {
                if (Equals(_isUse
                , value))
                {
                    return;
                }
                _isUse
                 = value;
                OnPropertyChanged(nameof(IsUse));

            }
        }
        #endregion

    }
}
