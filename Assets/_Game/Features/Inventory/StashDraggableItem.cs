using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class StashDraggableItem : DraggableItem
    {
        public override void OnDropCommit(PointerEventData eventData)
        {
            base.OnDropCommit(eventData);
            var stashItem = GetComponent<Template>().GetViewModel() as MyShip.StashItem;
            stashItem?.RemoveEquipment();
        }
    }
}