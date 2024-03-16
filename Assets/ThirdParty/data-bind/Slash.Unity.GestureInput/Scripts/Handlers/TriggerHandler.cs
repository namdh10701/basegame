using System;
using Slash.Unity.GestureInput.Sources;
using UnityEngine.Events;

namespace Slash.Unity.GestureInput.Handlers
{
    public class TriggerHandler : InputEventHandler<TriggerSource>
    {
        public CallbackHandler Callback = new CallbackHandler();

        protected override UnityEvent GetEvent(TriggerSource target)
        {
            return target.Trigger;
        }

        protected override void OnEvent()
        {
            this.Callback.Invoke();
        }

        [Serializable]
        public class CallbackHandler : UnityEvent
        {
        }
    }
}