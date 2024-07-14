using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class InventoryItemDragDataProvider: DragDataProvider {
        public override object GetData()
        {
            var data = GetComponent<Template>().GetViewModel();
            return data;
        }
    }
}