using Unity.VisualScripting;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class StashDropHandler : DropHandler
    {
        public override void OnItemDrop(Object item)
        {
            var droppedItem = item.GetComponent<DraggableItem>();
            var data = droppedItem.DragDataProvider.GetData<InventoryItem>();

            if (GetComponent<Template>().GetViewModel() is MyShip.StashItem stashItem)
            {
                stashItem.InventoryItem = data;
            }
        }
    }
}