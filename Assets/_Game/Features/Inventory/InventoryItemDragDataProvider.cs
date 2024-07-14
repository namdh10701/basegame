using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class InventoryItemDragDataProvider: DragDataProvider {
        public override T GetData<T>()
        {
            var data = GetComponent<Template>().GetViewModel();
            return data as T;
        }
    }
}