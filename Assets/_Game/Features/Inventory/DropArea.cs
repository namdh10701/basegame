using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Features.Inventory
{
    public class DropArea : MonoBehaviour, IDropHandler
    {
        public DropHandler DropHandler;
        
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Item Dropped");
            if (eventData.pointerDrag == null) return;
            var draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();

            if (!draggableItem) return;
            
            var isDropCommitted = DropHandler.OnItemDrop(draggableItem);

            if (isDropCommitted.Cmd == ItemDroppedCallbackCommand.Command.COMMIT)
            {
                draggableItem.OnDropCommit(eventData, isDropCommitted.Data);
            }
        }
    }
}