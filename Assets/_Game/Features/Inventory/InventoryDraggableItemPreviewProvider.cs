using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class InventoryDraggableItemPreviewProvider : DraggableItemPreviewProvider
    {
        public override Object GetPreviewItemPrefab()
        {
            var item = GetComponent<Template>().GetViewModel() as InventoryItem;
            return ShipSetupUtils.GetShipSetupItemPrefab(item);
        }
    }
}