using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class StashDragDataProvider: DragDataProvider {
        public override T GetData<T>()
        {
            var stashItem = GetComponent<Template>().GetViewModel() as MyShip.StashItem;
            return stashItem?.InventoryItem as T;
        }
    }
}