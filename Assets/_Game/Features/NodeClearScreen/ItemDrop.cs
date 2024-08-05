using _Game.Features.Inventory;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;
namespace _Game.Features.NodeClear
{
    [Binding]
    public class ItemDrop : SubViewModel
    {
        [Binding]
        public InventoryItem InventoryItem { get; set; }

        #region Binding Prop: IsActiveItem
        /// <summary>
        /// IsActiveItem
        /// </summary>
        [Binding]
        public bool IsActiveItem
        {
            get => _isActiveItem;
            set
            {
                if (Equals(_isActiveItem, value))
                {
                    return;
                }

                _isActiveItem = value;
                OnPropertyChanged(nameof(IsActiveItem));
            }
        }
        private bool _isActiveItem;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                if (InventoryItem.Type != null && InventoryItem.OperationType != null)
                {
                    var itemType = InventoryItem.Type.ToString().ToLower();
                    var name = InventoryItem.OperationType.ToLower();
                    var path = $"Images/Drop/{itemType}_{name}";
                    return Resources.Load<Sprite>(path);
                }
                else
                {
                    return Resources.Load<Sprite>($"Items/Drop_ammo_arrow");
                }

            }
        }

        NodeClearScreen _nodeClearScreen;

        public void Setup(NodeClearScreen nodeClearScreen)
        {
            _nodeClearScreen = nodeClearScreen;
            IsActiveItem = InventoryItem == null ? true : false;
            OnPropertyChanged(nameof(Thumbnail));

        }

        [Binding]
        public void OnChangeValueToggle()
        {
            _nodeClearScreen.SetDropItemInventory(InventoryItem);
        }
    }
}
