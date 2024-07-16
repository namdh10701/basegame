using _Base.Scripts.Utils;
using _Game.Features.MyShip;
using UnityEngine.EventSystems;

namespace _Game.Features.Inventory
{
    public class InventoryDraggableItem : DraggableItem
    {
        public override void OnDropCommit(PointerEventData eventData)
        {
            base.OnDropCommit(eventData);
            IOC.Resolve<InventorySheet>().AddIgnore(DragDataProvider.GetData<InventoryItem>());
        }
    }
}