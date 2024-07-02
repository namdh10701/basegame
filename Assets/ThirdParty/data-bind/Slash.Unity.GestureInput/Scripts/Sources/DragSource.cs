using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Slash.Unity.GestureInput.Sources
{
    using Slash.Unity.GestureInput.Gestures.Implementations;

    public class DragSource : MonoBehaviour, IEventSystemHandler
    {
        public DragEvent Drag = new DragEvent();

        public EndDragEvent EndDrag = new EndDragEvent();

        public bool OnDrag(DragEventData eventData)
        {
            this.Drag.Invoke(eventData);
            return true;
        }

        public bool OnEndDrag(EndDragEventData eventData)
        {
            this.EndDrag.Invoke(eventData);
            return true;
        }

        public class DragEvent : UnityEvent<DragEventData>
        {
        }

        public class EndDragEvent : UnityEvent<EndDragEventData>
        {
        }
    }
}