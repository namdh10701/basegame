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
            
            DropHandler.OnItemDrop(eventData.pointerDrag);
        }
    }
}