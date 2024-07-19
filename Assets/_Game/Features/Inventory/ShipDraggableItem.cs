using UnityEngine.EventSystems;

namespace _Game.Features.Inventory
{
    public class ShipDraggableItem : DraggableItem
    {
        public override void OnDropCommit(PointerEventData eventData, object data)
        {
            base.OnDropCommit(eventData, data);
            var draggableItem = gameObject.GetComponent<DraggableItem>();
            if (draggableItem)
            {
                draggableItem.OnEndDrag(eventData);
            }
            Destroy(gameObject);
        }
    }
}