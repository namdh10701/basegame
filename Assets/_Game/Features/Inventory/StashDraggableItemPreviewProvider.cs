using UnityEngine;

namespace _Game.Features.Inventory
{
    public class StashDraggableItemPreviewProvider : DraggableItemPreviewProvider
    {
        public override Object GetPreviewItemPrefab()
        {
            var dragDataProvider = GetComponent<DraggableItem>().DragDataProvider;
            var item = dragDataProvider.GetData<InventoryItem>();
            var shapePath = $"SetupItems/SetupItem_{item.Type.ToString().ToLower()}_{item.OperationType}";
            var prefab = Resources.Load(shapePath);
            return prefab;
        }
    }
}