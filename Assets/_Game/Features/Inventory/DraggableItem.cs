using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Features.Inventory
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Canvas canvas;

        public DragDataProvider dragDataProvider;
        // public RectTransform previewDragArea;
        public DraggableItemPreviewProvider DraggableItemPreviewProvider;

        private RectTransform _previewDragItem;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = GetComponentInParent<Canvas>(); // Get the Canvas component from the parent hierarchy
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 0.6f; // Make it semi-transparent
            canvasGroup.blocksRaycasts = false; // Disable raycast blocking so it can be dropped

            _previewDragItem = Instantiate(DraggableItemPreviewProvider.GetPreviewItemPrefab()).GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _previewDragItem.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1.0f; // Reset transparency
            canvasGroup.blocksRaycasts = true; // Enable raycast blocking
            Destroy(_previewDragItem.gameObject);
        }
    }
}