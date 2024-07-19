using UnityEngine;

namespace _Game.Features.Inventory
{
    public abstract class DropHandler : MonoBehaviour
    {
        public abstract ItemDroppedCallbackCommand OnItemDrop(DraggableItem droppedItem);
    }
}