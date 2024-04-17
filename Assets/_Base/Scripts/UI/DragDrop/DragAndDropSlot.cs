using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Base.Scripts.UI.DragDrop
{
    public class DragAndDropSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IInitializePotentialDragHandler//, IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField] private bool activeScrolling = false;
        private ScrollRect scrollRect;
        private Button button;
        private bool isScroll = false;
        private DragAndDropManager manager;

        private void OnEnable()
        {
            scrollRect = GetComponentInParent<ScrollRect>();
            button = GetComponent<Button>();
            manager = GetComponentInParent<DragAndDropManager>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (button) button.interactable = false;
            
            if (activeScrolling && WhetherScrolling(scrollRect, eventData.delta))
            {
                scrollRect.OnBeginDrag(eventData);
                isScroll = true;
                
                return;
            }

            if (manager) manager.OnBeginDrag(this, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (activeScrolling && scrollRect && isScroll)
            {
                scrollRect.OnDrag(eventData);
                return;
            }

            if (manager) manager.OnDrag(this, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (button) button.interactable = true;

            if (activeScrolling && scrollRect && isScroll)
            {
                scrollRect.OnEndDrag(eventData);
                isScroll = false;

                return;
            }

            if (manager) 
            {
                if (eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<DragAndDropSlot>() == this)
                {
                    OnDrop(eventData);
                    return;
                }

                manager.OnEndDrag(this, eventData);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (manager) manager.OnDrop(this, eventData);
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (scrollRect) scrollRect.OnInitializePotentialDrag(eventData);
        }

        public void StopDrag()
        {
            if (manager) manager.StopDrag();
        }

        private static bool WhetherScrolling(ScrollRect scroll, Vector2 delta)
        {
            if (!scroll)
            {
                return false;
            }

            if (scroll.horizontal && scroll.vertical)
            {
                return true;
            }

            if (scroll.horizontal && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                return true;
            }

            if (scroll.vertical && Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
            {
                return true;
            }

            return false;
        }
    }
}