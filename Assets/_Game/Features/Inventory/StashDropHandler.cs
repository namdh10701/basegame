using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class StashDropHandler : DropHandler
    {
        public override bool OnItemDrop(DraggableItem droppedItem)
        {
            var data = droppedItem.DragDataProvider.GetData<InventoryItem>();

            if (GetComponent<Template>().GetViewModel() is MyShip.StashItem stashItem)
            {
                stashItem.InventoryItem = data;
                return true;
            }

            return false;
        }
    }
}