// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GestureRecognizer.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.GestureInput.Gestures
{
    using System;
    using System.Collections.Generic;

    using Slash.Unity.GestureInput.Modules;

    using UnityEngine.EventSystems;

    public abstract class GestureRecognizer<TEventData> : GestureRecognizer
        where TEventData : BaseEventData
    {
        protected void OnGestureDetected(TEventData eventData)
        {
            base.OnGestureDetected(eventData);
        }
    }

    public abstract class GestureRecognizer
    {
        public event Action<BaseEventData> GestureDetected;

        public abstract void Process();

        public abstract IEnumerable<RegisteredInputSource> GetInputSources();

        protected void OnGestureDetected(BaseEventData eventData)
        {
            var handler = this.GestureDetected;
            if (handler != null)
            {
                handler(eventData);
            }
        }
    }
}