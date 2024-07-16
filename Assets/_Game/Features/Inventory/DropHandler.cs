using UnityEngine;

namespace _Game.Features.Inventory
{
    public abstract class DropHandler : MonoBehaviour
    {
        public abstract bool OnItemDrop(DraggableItem droppedItem);
    }
}