using Unity.VisualScripting;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class ShipDropHandler : DropHandler
    {
        public RectTransform PlacementPane;
        public override void OnItemDrop(Object item)
        {
            var droppedItem = item.GetComponent<DraggableItem>();
            var data = droppedItem.DragDataProvider.GetData<InventoryItem>();

            
        }
    }
}