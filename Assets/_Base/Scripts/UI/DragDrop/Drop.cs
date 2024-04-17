using UnityEngine;
using UnityEngine.EventSystems;

namespace _Base.Scripts.UI.DragDrop
{
    public class Drop : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            // validate

            var draggingItem = eventData.pointerDrag.GetComponent<Drag>();

            if (draggingItem == null)
            {
                return;
            }

            draggingItem.ResetPosition(eventData.position);
            draggingItem.transform.SetParent(transform);
        }
    }
}
