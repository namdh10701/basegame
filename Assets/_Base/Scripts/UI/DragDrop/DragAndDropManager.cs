using UnityEngine;
using UnityEngine.EventSystems;

namespace _Base.Scripts.UI.DragDrop
{
    public class DragAndDropManager : MonoBehaviour
    {
        [SerializeField] private GameObject dragObject;
        [SerializeField] private Vector2 dragOffset;
        [SerializeField] private Canvas canvas;
        
        private DragAndDropSlot pressedDragSlot;

        private RectTransform rectTransform;
        private RectTransform dragObjectTransform;
        private Camera canvasCamera;

        // private Subject<DragEvent> onBeginDrag = new Subject<DragEvent>();
        // private Subject<DropEvent> onDrop = new Subject<DropEvent>();
        // private Subject<DragEvent> onEndDragWithoutDrop = new Subject<DragEvent>();
        //
        // public IObservable<DragEvent> BeginDragAsObservable() => onBeginDrag;
        // public IObservable<DropEvent> DropAsObservable() => onDrop;
        // public IObservable<DragEvent> EndDragWithoutDropAsObservable() => onEndDragWithoutDrop;

        private void Awake()
        {
            // Set variables
            rectTransform = transform as RectTransform;

            dragObject.SetActive(false);
            dragObject.transform.SetParent(transform);

            dragObjectTransform = dragObject.transform as RectTransform;

            canvasCamera = canvas.worldCamera;
            if (canvasCamera == null)
            {
                canvasCamera = Camera.main;
            }
        }

        internal void OnBeginDrag(DragAndDropSlot slot, PointerEventData eventData)
        {
            pressedDragSlot = slot;
            dragObject.SetActive(true);

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, 
                eventData.position, 
                canvasCamera, 
                out var localPoint))
            {
                dragObjectTransform.anchoredPosition = localPoint + dragOffset;
            }
            
            DragEvent dragEvent = new DragEvent(this, pressedDragSlot, dragObject);

            // onBeginDrag.OnNext(dragEvent);
            // ExecuteEvents.Execute<IDragAndDropHandler>(pressedDragSlot.gameObject, null, (o, a) => o.OnBeginDrag(dragEvent));
        }

        internal void OnDrag(DragAndDropSlot slot, PointerEventData eventData)
        {
            if (pressedDragSlot == null)
            {
                return;
            }
            
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, 
                eventData.position, 
                canvasCamera, 
                out var localPoint))
            {
                dragObjectTransform.anchoredPosition = localPoint + dragOffset;
            }
        }

        internal void OnEndDrag(DragAndDropSlot slot, PointerEventData eventData)
        {
            if (pressedDragSlot == null)
            {
                return;
            }

            DragEvent dragEvent = new DragEvent(this, pressedDragSlot, dragObject);
            
            // onEndDragWithoutDrop.OnNext(dragEvent);
            // ExecuteEvents.Execute<IDragAndDropHandler>(pressedDragSlot.gameObject, null, (o, a) => o.OnEndDragWithoutDrop(dragEvent));

            pressedDragSlot = null;
            dragObject.SetActive(false);
        }

        internal void OnDrop(DragAndDropSlot slot, PointerEventData eventData)
        {
            if (pressedDragSlot == null)
            {
                return;
            }

            DropEvent dropEvent = new DropEvent(this, pressedDragSlot, slot, dragObject);
            
            // onDrop.OnNext(dropEvent);
            // ExecuteEvents.Execute<IDragAndDropHandler>(pressedDragSlot.gameObject, null, (o, a) => o.OnDrop(dropEvent));
            // ExecuteEvents.Execute<IDragAndDropHandler>(slot.gameObject, null, (o, a) => o.OnDrop(dropEvent));

            pressedDragSlot = null;
            dragObject.SetActive(false);
        }

        public void StopDrag()
        {
            DragEvent dragEvent = new DragEvent(this, pressedDragSlot, dragObject);
            
            // onEndDragWithoutDrop.OnNext(dragEvent);
            // ExecuteEvents.Execute<IDragAndDropHandler>(pressedDragSlot.gameObject, null, (o, a) => o.OnEndDragWithoutDrop(dragEvent));

            pressedDragSlot = null;
            dragObject.SetActive(false);
        }
    }

    public struct DropEvent
    {
        public DragAndDropSlot dragSlot;
        public DragAndDropSlot dropSlot;
        public GameObject dragObject;

        public DropEvent(DragAndDropManager manager, DragAndDropSlot dragSlot, DragAndDropSlot dropSlot, GameObject dragObject)
        {
            this.dragSlot = dragSlot;
            this.dropSlot = dropSlot;
            this.dragObject = dragObject;
        }
    }

    public struct DragEvent
    {
        private DragAndDropManager manager;
        public DragAndDropSlot dragSlot;
        public GameObject dragObject;

        public DragEvent(DragAndDropManager manager, DragAndDropSlot dragSlot, GameObject dragObject)
        {
            this.manager = manager;
            this.dragSlot = dragSlot;
            this.dragObject = dragObject;
        }

        public void StopDrag()
        {
            manager.StopDrag();
        }
    }
}