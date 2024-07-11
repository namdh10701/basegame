using _Game.Features.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityWeld.Binding;

namespace _Game.Features.MyShip
{
    public class InventorySheetItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector2 prevPos;
        public InventoryItem InventoryItem;
        public NewShipEditSheet NewShipEditSheet;
        public DraggingItem DraggingItem;

        private void Awake()
        {
            InventoryItem = GetComponent<Template>().GetViewModel() as InventoryItem;
            // _inventoryItem.
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // prevPos = _draggingItem.transform.position;
            
            DraggingItem = NewShipEditSheet.CreateDragItem(InventoryItem);
            // NewShipEditSheet.ShipConfigManager.Grid.shape = DraggingItem.Shape;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(
                eventData.position
            );
            pos.z = 0;
            
            DraggingItem.transform.position = pos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // eventData.
            if (DraggingItem)
            {
                Destroy(DraggingItem.gameObject);
            }
            // _draggingItem.transform.position = prevPos;
        }
    }
}
