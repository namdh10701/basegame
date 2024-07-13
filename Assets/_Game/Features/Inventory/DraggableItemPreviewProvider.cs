using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Features.Inventory
{
    public abstract class DraggableItemPreviewProvider : MonoBehaviour
    {
        public abstract Object GetPreviewItemPrefab();
    }
}