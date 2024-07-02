using UnityEngine.EventSystems;

namespace Slash.Unity.GestureInput.Sources
{
    using Slash.Unity.GestureInput.Gestures.Implementations;

    public interface ISwipeHandler : IEventSystemHandler
    {
        bool OnSwipe(SwipeEventData eventData);
    }
}