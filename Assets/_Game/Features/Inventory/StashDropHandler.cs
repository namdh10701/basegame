using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Features.Inventory
{
    public class StashDropHandler : DropHandler
    {
        public override void OnItemDrop(Object item)
        {
            var droppedItem = item.GetComponent<DraggableItem>();
            var data = droppedItem.dragDataProvider.GetData();
            // droppedItem.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}