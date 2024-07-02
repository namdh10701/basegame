using UnityEngine;
using UnityEngine.EventSystems;

namespace _Base.Scripts.UI.DragDrop
{
    public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Transform self;
        public Vector3 beforeDragPosition;

        private void Start()
        {
            beforeDragPosition = transform.position;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            //eventData.pointerClick.gameObject.GetComponent<Image>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetPosition();
        }

        public void ResetPosition(Vector3? position = null)
        {
            if (position != null)
            {
                beforeDragPosition = position.Value;
            }

            transform.position = beforeDragPosition;
        }
    }
}