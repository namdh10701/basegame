using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Slash.Unity.GestureInput.Handlers
{
    /// <summary>
    ///     Base class for a command which is called on a Unity event.
    /// </summary>
    /// <typeparam name="TSource">Type of mono behaviour to observe for event.</typeparam>
    /// <typeparam name="TEventData">Data provided by input event.</typeparam>
    public abstract class InputEventHandler<TSource, TEventData> : InputEventHandlerBase<TSource>
        where TSource : MonoBehaviour
    {
        /// <summary>
        ///     Returns the event from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Event from the specified target to observe.</returns>
        protected virtual UnityEvent<TEventData> GetEvent(TSource target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns the events from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Events from the specified target to observe.</returns>
        protected virtual IEnumerable<UnityEvent<TEventData>> GetEvents(TSource target)
        {
            yield return this.GetEvent(target);
        }

        protected abstract void OnEvent(TEventData eventData);

        protected override void RegisterListeners(TSource target)
        {
            var unityEvents = this.GetEvents(target);
            foreach (var unityEvent in unityEvents)
            {
                unityEvent.AddListener(this.OnEvent);
            }
        }

        protected override void RemoveListeners(TSource target)
        {
            var unityEvents = this.GetEvents(target);
            foreach (var unityEvent in unityEvents)
            {
                unityEvent.RemoveListener(this.OnEvent);
            }
        }
    }

    public abstract class InputEventHandlerBase<TSource> : MonoBehaviour
    {
        #region Fields

        /// <summary>
        ///     Target to work with.
        /// </summary>
        public TSource Target;

        #endregion

        #region Methods

        /// <summary>
        ///     Unity callback.
        /// </summary>
        protected virtual void OnDisable()
        {
            if (this.Target == null)
            {
                return;
            }

            this.RemoveListeners(this.Target);
        }

        /// <summary>
        ///     Unity callback.
        /// </summary>
        protected virtual void OnEnable()
        {
            if (this.Target == null)
            {
                return;
            }

            this.RegisterListeners(this.Target);
        }

        /// <summary>
        ///     Called when the observed event occured.
        /// </summary>
        protected virtual void OnEvent()
        {
        }

        protected abstract void RegisterListeners(TSource target);

        protected abstract void RemoveListeners(TSource target);

        /// <summary>
        ///     Unity callback.
        /// </summary>
        protected virtual void Reset()
        {
            if (this.Target == null)
            {
                this.Target = this.GetComponent<TSource>();
            }
        }

        #endregion
    }

    /// <summary>
    ///     Base class for a handler which is called on a Unity event.
    /// </summary>
    /// <typeparam name="TSource">Type of mono behaviour to observe for event.</typeparam>
    public abstract class InputEventHandler<TSource> : InputEventHandlerBase<TSource>
        where TSource : MonoBehaviour
    {
        /// <summary>
        ///     Returns the event from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Event from the specified target to observe.</returns>
        protected abstract UnityEvent GetEvent(TSource target);

        /// <summary>
        ///     Returns the events from the specified target to observe.
        /// </summary>
        /// <param name="target">Target behaviour to get event from.</param>
        /// <returns>Events from the specified target to observe.</returns>
        protected virtual IEnumerable<UnityEvent> GetEvents(TSource target)
        {
            yield return this.GetEvent(target);
        }

        protected override void RegisterListeners(TSource target)
        {
            var unityEvents = this.GetEvents(target);
            foreach (var unityEvent in unityEvents)
            {
                unityEvent.AddListener(this.OnEvent);
            }
        }

        protected override void RemoveListeners(TSource target)
        {
            var unityEvents = this.GetEvents(target);
            foreach (var unityEvent in unityEvents)
            {
                unityEvent.RemoveListener(this.OnEvent);
            }
        }
    }
}