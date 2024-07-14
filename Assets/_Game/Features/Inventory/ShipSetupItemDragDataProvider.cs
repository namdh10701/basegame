using _Game.Features.MyShip.GridSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Features.Inventory
{
    public class ShipSetupItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Vector2Int Position;
        public ItemShape Shape;
        public InventoryItem InventoryItem;
        private Vector2 currentMousePos;
        private Vector2 offsetMousePos;

        public void OnDrag(PointerEventData eventData)
        {
            // throw new System.NotImplementedException();
            Vector3 pos = Camera.main.ScreenToWorldPoint(
                eventData.position
            );
            pos.z = 0;
            var delta = ((Vector2)pos) - currentMousePos + offsetMousePos;
            
            transform.position = currentMousePos + delta;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(
                eventData.position
            );
            pos.z = 0;

            offsetMousePos = transform.position - pos;
            
            currentMousePos = Camera.main.ScreenToWorldPoint(
                eventData.position
            );
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }
    }
}
