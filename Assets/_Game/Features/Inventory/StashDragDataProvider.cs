namespace _Game.Features.Inventory
{
    public class DragSlotDataProvider: DragDataProvider {
        public override T GetData<T>()
        {
            var data = GetComponent<InventoryItem>();
            return data as T;
        }
    }
}