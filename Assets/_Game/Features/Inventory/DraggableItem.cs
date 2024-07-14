using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace _Game.Features.Inventory
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Canvas _canvas;

        private Canvas Canvas
        {
            get
            {
                if (!_canvas)
                {
                    _canvas = GetComponentInParent<Canvas>(); // Get the Canvas component from the parent hierarchy
                }

                return _canvas;
            }
        }

        public DragDataProvider DragDataProvider;
        public RectTransform PreviewDragArea;
        public DraggableItemPreviewProvider DraggableItemPreviewProvider;

        private RectTransform _previewDragItem;

        private void Awake()
        {
            // rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();

            if (!PreviewDragArea)
            {
                PreviewDragArea = PreviewDragPane.Instance.transform as RectTransform;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (PreviewDragArea)
            {
                var previewPrefab = DraggableItemPreviewProvider
                    ? DraggableItemPreviewProvider.GetPreviewItemPrefab()
                    : this;

                if (!previewPrefab)
                {
                    Debug.LogError("Preview prefab not found");
                    return;
                }
                
                _previewDragItem = Instantiate(
                    previewPrefab,
                    PreviewDragArea
                ).GetComponent<RectTransform>();

                RectTransformUtility.ScreenPointToLocalPointInRectangle(PreviewDragArea, eventData.position,
                    eventData.pressEventCamera, out var localMousePos);
                _previewDragItem.anchorMin = Vector2.zero;
                _previewDragItem.anchorMax = Vector2.zero;
                _previewDragItem.pivot = new(0.5f, 0.5f);
                _previewDragItem.anchoredPosition = localMousePos;
            }
            
            
            if (canvasGroup)
            {
                canvasGroup.alpha = 0.6f; // Make it semi-transparent
                canvasGroup.blocksRaycasts = false; // Disable raycast blocking so it can be dropped
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_previewDragItem)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(PreviewDragArea, eventData.position,
                    eventData.pressEventCamera, out var localMousePos);

                _previewDragItem.anchoredPosition += eventData.delta / Canvas.scaleFactor;
                Debug.Log("anchoredPosition " + _previewDragItem.anchoredPosition);
                Debug.Log("mousePos " + localMousePos);
            }
            // else
            // {
            //     rectTransform.anchoredPosition += eventData.delta / Canvas.scaleFactor;
            // }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (canvasGroup)
            {
                canvasGroup.alpha = 1.0f; // Reset transparency
                canvasGroup.blocksRaycasts = true; // Enable raycast blocking
            }

            if (_previewDragItem)
            {
                Destroy(_previewDragItem.gameObject);
            }
        }

        public virtual void OnDropCommit(PointerEventData eventData)
        {
            // TODO
        }
    }
}