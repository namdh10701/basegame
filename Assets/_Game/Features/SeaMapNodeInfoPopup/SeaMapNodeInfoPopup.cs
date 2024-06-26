using _Base.Scripts.UI;
using _Game.Features.Inventory;
using UnityWeld.Binding;
using InventoryItem = _Game.Features.Inventory.InventoryItem;

namespace _Game.Features.SeaMapNodeInfoPopup
{
    [Binding]
    public class SeaMapNodeInfoPopup : Popup
    {
        public enum Style
        {
            Normal,
            Boss
        }

        public Style style = Style.Normal;

        [Binding] 
        public bool IsBossStyle => style == Style.Boss;
        
        
        #region Binding: Items

        private ObservableList<InventoryItem> items = new ObservableList<InventoryItem>();

        [Binding]
        public ObservableList<InventoryItem> Items => items;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            
            Items.Add(new InventoryItem { Type = ItemType.CANNON, Id = "0001"});
            Items.Add(new InventoryItem {  Type = ItemType.CANNON, Id = "0012"});
            Items.Add(new InventoryItem {  Type = ItemType.CANNON, Id = "0012"});
            Items.Add(new InventoryItem {  Type = ItemType.CANNON, Id = "0012"});
            Items.Add(new InventoryItem {  Type = ItemType.CANNON, Id = "0012"});
        }
    }
}