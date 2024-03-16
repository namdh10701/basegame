using System;
using Slash.Unity.GestureInput.Gestures;
using UnityEngine;
using UnityEngine.Events;

namespace Slash.Unity.GestureInput.Sources
{
    using Slash.Unity.GestureInput.Gestures.Implementations;

    public class SwipeSource : MonoBehaviour, ISwipeHandler
    {
        public SwipeEvent Swipe;

        public bool OnSwipe(SwipeEventData eventData)
        {
            this.Swipe.Invoke(eventData);
            return true;
        }

        [Serializable]
        public class SwipeEvent : UnityEvent<SwipeEventData>
        {
        }
    }
}