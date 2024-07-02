using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Base.Scripts.UI.DragDrop
{
    public class DragAndDropHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        // Don't forget to set this to TRUE or expose it to the Inspector else it will always be false and the script will not work
        public bool Draggable;

        private bool draggingSlot;

        [SerializeField] private ScrollRect scrollRect;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!Draggable)
            {
                return;
            }

            StartCoroutine(StartTimer());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopAllCoroutines();
        }

        private IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(0.5f);
            draggingSlot = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.beginDragHandler);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (draggingSlot)
            {
                //DO YOUR DRAGGING HERE
            } else
            {
                //OR DO THE SCROLLRECT'S
                ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.dragHandler);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ExecuteEvents.Execute(scrollRect.gameObject, eventData, ExecuteEvents.endDragHandler);
            if (draggingSlot)
            {
                //END YOUR DRAGGING HERE
                draggingSlot = false;
            }
        }
    }
}