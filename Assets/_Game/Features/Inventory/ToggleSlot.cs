using _Game.Features.Inventory;
using _Game.Scripts.DB;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;
namespace _Game.Features.InventoryCustomScreen
{
    [Binding]
    public class ToggleSlot : SubViewModel
    {
        public IInventoryCustomScreen IInventoryCustomScreen { get; set; }

        [Binding]
        public string Id { get; set; }

        #region Binding Prop: SlotName

        /// <summary>
        /// SlotName
        /// </summary>
        [Binding]
        public string SlotName
        {
            get => m_slotname;
            set
            {
                if (Equals(m_slotname, value))
                {
                    return;
                }

                m_slotname = value;
                OnPropertyChanged(nameof(SlotName));
            }
        }

        private string m_slotname;

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

    }
}
