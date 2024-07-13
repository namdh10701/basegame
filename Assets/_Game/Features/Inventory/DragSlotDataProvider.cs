namespace _Game.Features.Inventory
{
    public class DragSlotDataProvider: DragDataProvider {
        public override object GetData()
        {
            var data = GetComponent<InventoryItem>();
            return data;
        }
    }
}