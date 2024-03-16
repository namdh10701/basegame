namespace Slash.Unity.GestureInput.Gestures.Implementations
{
    using System;
    using System.Collections.Generic;

    using Slash.Unity.GestureInput.Devices;
    using Slash.Unity.GestureInput.Modules;
    using Slash.Unity.GestureInput.Raycasts;
    using Slash.Unity.GestureInput.Sources;
    using Slash.Unity.GestureInput.Utils;

    using UnityEngine;
    using UnityEngine.EventSystems;

    [Serializable]
    public class TriggerEventData : BaseEventData
    {
        /// <summary>
        ///     Button which was triggered.
        /// </summary>
        public string Button;

        public TriggerEventData(EventSystem eventSystem) : base(eventSystem)
        {
        }
    }

    [Serializable]
    public class TriggerGestureRecognizer : GestureRecognizer<TriggerEventData>
    {
        /// <summary>
        ///     Threshold the pointer is allowed to move without canceling the gesture (in screen distance).
        /// </summary>
        [Tooltip("Threshold the pointer is allowed to move without canceling the gesture (in screen distance)")]
        public double MoveThreshold = 0.1f;

        private IPointerDevice pointerDevice;

        private IPointerTouchDetection pointerTouchDetection;

        /// <summary>
        ///     Named trigger buttons.
        /// </summary>
        public List<Trigger> Triggers = new List<Trigger> {new Trigger {Name = "Submit", TriggerButton = "Submit"}};

        public void Init(IPointerDevice pointer, IPointerTouchDetection touchDetection)
        {
            this.pointerDevice = pointer;
            this.pointerTouchDetection = touchDetection;
        }

        public override void Process()
        {
            foreach (var trigger in this.Triggers)
            {
                this.ProcessTrigger(trigger);
            }
        }

        public override IEnumerable<RegisteredInputSource> GetInputSources()
        {
            yield return
                new RegisteredInputSource<TriggerSource, TriggerEventData>((source, data) => source.OnTrigger(data));
        }

        private void ProcessTrigger(Trigger trigger)
        {
            if (Input.GetButtonDown(trigger.TriggerButton))
            {
                if (trigger.Mode == Trigger.TriggerMode.Push)
                {
                    this.OnGestureDetected(trigger);
                }
                else
                {
                    trigger.PressPosition = this.pointerDevice.GetPosition();
                    trigger.PressedGameObject = this.pointerTouchDetection.GetTouchedGameObject(trigger.PressPosition);
                }
            }

            if (Input.GetButtonUp(trigger.TriggerButton))
            {
                if (trigger.Mode == Trigger.TriggerMode.Release &&
                    this.ShouldTrigger(trigger))
                {
                    this.OnGestureDetected(trigger);
                }

                trigger.PressedGameObject = null;
            }
        }

        private void OnGestureDetected(Trigger trigger)
        {
            this.OnGestureDetected(new TriggerEventData(null) {Button = trigger.TriggerButton});
        }

        private bool ShouldTrigger(Trigger trigger)
        {
            // Check if button was released on pressed game object.
            var pointerPosition = this.pointerDevice.GetPosition();
            if (this.pointerTouchDetection.GetTouchedGameObject(pointerPosition) != trigger.PressedGameObject)
            {
                return false;
            }

            // Check if moved too much.
            var deltaMove = pointerPosition - trigger.PressPosition;
            if (deltaMove.magnitude > this.MoveThreshold)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("MoveThreshold: {0}\n\nTriggers:\n{1}", this.MoveThreshold, this.Triggers.Implode("\n\n"));
        }

        [Serializable]
        public class Trigger
        {
            public enum TriggerMode
            {
                Release,

                Push
            }

            /// <summary>
            ///     Defines when trigger is fired.
            /// </summary>
            [Tooltip("Defines when trigger is fired")]
            public TriggerMode Mode;

            /// <summary>
            ///     Name of trigger. Some sources may be only valid for specific triggers.
            /// </summary>
            [Tooltip("Name of trigger. Some sources may be only valid for specific triggers")]
            public string Name = string.Empty;

            internal GameObject PressedGameObject;

            internal Vector2 PressPosition;

            /// <summary>
            ///     Name of input which triggers the gesture (from Input settings).
            /// </summary>
            [Tooltip("Button which triggers the gesture (from Input settings)")]
            public string TriggerButton = "Submit";

            public override string ToString()
            {
                return
                    string.Format(
                        "Mode: {0}, Name: {1}, TriggerButton: {2}, PressedGameObject: {3}, PressPosition: {4}",
                        this.Mode, this.Name, this.TriggerButton, this.PressedGameObject, this.PressPosition);
            }
        }
    }
}