using _Base.Scripts.Utils;
using _Game.Features.MyShip;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class StashDropHandler : DropHandler
    {
        public override bool OnItemDrop(DraggableItem droppedItem)
        {
            var data = droppedItem.DragDataProvider.GetData<InventoryItem>();

            if (GetComponent<Template>().GetViewModel() is not MyShip.StashItem stashItem) return false;
            
            stashItem.InventoryItem = data;
            
            return true;
        }
    }
}