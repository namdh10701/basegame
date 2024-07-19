using _Base.Scripts.Utils;
using _Game.Features.MyShip;
using UnityEngine.EventSystems;

namespace _Game.Features.Inventory
{
    public class InventoryDraggableItem : DraggableItem
    {
        public override void OnDropCommit(PointerEventData eventData, object data)
        {
            base.OnDropCommit(eventData, data);
            IOC.Resolve<InventorySheet>().AddIgnore(DragDataProvider.GetData<InventoryItem>());
            IOC.Resolve<NewShipEditSheet>().SaveSetupProfile();
        }
    }
}