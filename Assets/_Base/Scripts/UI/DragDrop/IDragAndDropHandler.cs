using UnityEngine.EventSystems;

namespace _Base.Scripts.UI.DragDrop
{
    public interface IDragAndDropHandler : IEventSystemHandler
    {
        void OnBeginDrag(DragEvent e);
        void OnEndDragWithoutDrop(DragEvent e);
        void OnDrop(DropEvent e);
    }
}