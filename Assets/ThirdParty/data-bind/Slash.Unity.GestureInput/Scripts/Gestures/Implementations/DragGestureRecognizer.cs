namespace Slash.Unity.GestureInput.Gestures.Implementations
{
    using System;
    using System.Collections.Generic;

    using Slash.Unity.GestureInput.Devices;
    using Slash.Unity.GestureInput.Modules;
    using Slash.Unity.GestureInput.Sources;

    using UnityEngine;
    using UnityEngine.EventSystems;

    public class DragEventData : BaseEventData
    {
        public Vector2 Delta { get; set; }

        public DragEventData() : base(null)
        {
        }
    }

    public class BeginDragEventData : BaseEventData
    {
        public BeginDragEventData() : base(null)
        {
        }
    }

    public class EndDragEventData : BaseEventData
    {
        /// <summary>
        ///     Velocity the drag was performed with.
        /// </summary>
        public Vector2 DragVelocity { get; set; }

        public EndDragEventData() : base(null)
        {
        }
    }

    [Serializable]
    public class DragGestureRecognizer : GestureRecognizer<DragEventData>
    {
        public float PixelDragThreshold = 4;

        private Vector2 dragVelocity;

        private bool isDragging;

        private Vector2 lastDragPosition;

        private float lastDragTimestamp;

        private IPointerDevice pointerDevice;

        private Vector2 pressPosition;

        private bool wasPressed;

        public void Init(IPointerDevice pointerDevice)
        {
            this.pointerDevice = pointerDevice;
        }

        public override void Process()
        {
            if (!this.isDragging)
            {
                // Check if drag should begin.
                if (!this.pointerDevice.IsDown())
                {
                    this.wasPressed = false;
                    return;
                }

                var position = this.pointerDevice.GetPosition();
                if (!this.wasPressed)
                {
                    this.pressPosition = position;
                    this.wasPressed = true;
                }

                if (ShouldStartDrag(this.pressPosition, this.pointerDevice.GetPosition(), this.PixelDragThreshold))
                {
                    this.OnBeginDrag();
                }
            }
            else
            {
                // Update velocity.
                var timestamp = Time.realtimeSinceStartup;
                var elapsedDuration = timestamp - this.lastDragTimestamp;
                this.lastDragTimestamp = timestamp;
                var position = this.pointerDevice.GetPosition();
                var delta = position - this.lastDragPosition;
                this.lastDragPosition = position;

                if (elapsedDuration > 0)
                {
                    var velocity = delta/(elapsedDuration);
                    this.dragVelocity = velocity*0.8f + this.dragVelocity*0.2f;
                }

                if (delta.sqrMagnitude > 0)
                {
                    // Drag gesture continued.
                    this.OnGestureDetected(new DragEventData
                    {
                        Delta = delta
                    });
                }

                // Check if drag ended.
                if (!this.pointerDevice.IsDown())
                {
                    this.OnEndDrag();
                }
            }
        }

        private void OnBeginDrag()
        {
            Debug.Log("Drag begins");

            this.isDragging = true;

            this.dragVelocity = Vector2.zero;
            this.lastDragPosition = this.pointerDevice.GetPosition();

            this.OnGestureDetected(new BeginDragEventData());
        }

        private void OnEndDrag()
        {
            Debug.Log("Drag ended with velocity " + this.dragVelocity);

            this.isDragging = false;

            this.OnGestureDetected(new EndDragEventData {DragVelocity = this.dragVelocity});
        }

        private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold)
        {
            return (pressPos - currentPos).sqrMagnitude >= threshold*threshold;
        }

        public override IEnumerable<RegisteredInputSource> GetInputSources()
        {
            yield return new RegisteredInputSource<DragSource, DragEventData>((source, data) => source.OnDrag(data));
            yield return new RegisteredInputSource<DragSource, EndDragEventData>((source, data) => source.OnEndDrag(data));
        }
    }
}