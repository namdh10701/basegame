namespace _Game.Features.Inventory
{
    public class ShipSetupItemDragDataProvider : DragDataProvider
    {
        public override T GetData<T>()
        {
            return GetComponent<ShipSetupItem>().InventoryItem as T;
        }
    }
}
