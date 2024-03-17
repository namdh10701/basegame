using System;
using System.Collections.Generic;
using Slash.Unity.GestureInput.Gestures;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Slash.Unity.GestureInput.Sources
{
    using Slash.Unity.GestureInput.Gestures.Implementations;

    public class TriggerSource : MonoBehaviour, IEventSystemHandler
    {
        public TriggerEvent Trigger = new TriggerEvent();

        /// <summary>
        ///     Buttons which will fire the trigger.
        /// </summary>
        [Tooltip("Buttons which will fire the trigger")]
        public List<string> ValidButtons;

        public bool OnTrigger(TriggerEventData eventData)
        {
            if (this.ValidButtons.Count != 0 && !this.ValidButtons.Contains(eventData.Button))
            {
                return false;
            }

            this.Trigger.Invoke();
            return true;
        }

        [Serializable]
        public class TriggerEvent : UnityEvent
        {
        }
    }
}