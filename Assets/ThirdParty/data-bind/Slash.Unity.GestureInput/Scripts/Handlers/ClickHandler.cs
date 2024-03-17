using System;
using Slash.Unity.GestureInput.Sources;
using UnityEngine.Events;

namespace Slash.Unity.GestureInput.Handlers
{
    public class ClickHandler : InputEventHandler<ClickSource, ClickSource.ClickEventData>
    {
        public CallbackHandler Callback = new CallbackHandler();

        protected override UnityEvent<ClickSource.ClickEventData> GetEvent(ClickSource target)
        {
            return target.Click;
        }

        protected override void OnEvent(ClickSource.ClickEventData eventData)
        {
            this.Callback.Invoke(eventData);
        }

        [Serializable]
        public class CallbackHandler : UnityEvent<ClickSource.ClickEventData>
        {
        }
    }
}