using _Game.Features.Inventory;
using _Game.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
namespace _Game.Features.InventoryCustomScreen
{
    [Binding]
    public class ButtonSlot : SubViewModel
    {

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
                    case ItemType.MISC:
                        var path = Id == null ? $"Items/item_misc_eq2_com" : $"Items/item_misc_eq2_com";
                        return Resources.Load<Sprite>(path);
                    default:
                        Debug.LogWarning("Images/Common/icon_plus");
                        return Resources.Load<Sprite>("Images/Common/icon_plus");
                }
            }
        }
        #endregion

        #region Binding Prop: Interactable

        private bool _interactable = true;
        /// <summary>
        /// Interactable
        /// </summary>
        [Binding]
        public bool Interactable
        {
            get => _interactable;
            set
            {
                if (_interactable == value)
                {
                    return;
                }

                _interactable = value;

                OnPropertyChanged(nameof(Interactable));
            }
        }

        #endregion


        public void UpdateData(string id, ItemType itemType)
        {
            Id = id;
            Type = itemType;
            OnPropertyChanged(nameof(Thumbnail));

        }
    }
}
