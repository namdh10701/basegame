using _Game.Features.MyShip;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class InventoryDraggableItemPreviewProvider : DraggableItemPreviewProvider
    {
        public override Object GetPreviewItemPrefab()
        {
            var item = GetComponent<Template>().GetViewModel() as InventoryItem;
            var shapePath = $"SetupItems/SetupItem_{item.Type.ToString().ToLower()}_{item.OperationType}";
            var prefab = Resources.Load(shapePath);
            return prefab;
        }
    }
}