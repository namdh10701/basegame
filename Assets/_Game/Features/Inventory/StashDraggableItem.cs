using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class StashDraggableItem : DraggableItem
    {
        public override void OnDropCommit(PointerEventData eventData, object data)
        {
            base.OnDropCommit(eventData, data);
            var stashItem = GetComponent<Template>().GetViewModel() as MyShip.StashItem;
            if (data != null)
            {
                stashItem.InventoryItem = (InventoryItem)data;
            }
            else
            {
                stashItem?.RemoveEquipment();
            }
        }
    }
}