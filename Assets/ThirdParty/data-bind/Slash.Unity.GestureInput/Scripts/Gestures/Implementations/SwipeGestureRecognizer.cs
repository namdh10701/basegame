// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwipeGestureRecognizer.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.GestureInput.Gestures.Implementations
{
    using System;
    using System.Collections.Generic;

    using Slash.Unity.GestureInput.Modules;
    using Slash.Unity.GestureInput.Sources;

    using UnityEngine;
    using UnityEngine.EventSystems;

    [Flags]
    public enum SwipeDirection
    {
        None = 0,

        Up = 1 << 1,

        Down = 1 << 2,

        Left = 1 << 3,

        Right = 1 << 4
    };

    public class SwipeEventData : BaseEventData
    {
        public SwipeEventData(EventSystem eventSystem)
            : base(eventSystem)
        {
        }

        public SwipeDirection Direction { get; set; }
    }

    [Serializable]
    public class SwipeGestureRecognizer : GestureRecognizer<SwipeEventData>
    {
        /// <summary>
        ///   Minimum distance to move pointer to count as swipe.
        /// </summary>
        [SerializeField]
        protected float MinSwipeDistance = 0.3f;

        private Vector2 pointerDownPosition;

        private Vector2 pointerUpPosition;

        [SerializeField]
        protected string SimulateSwipeDownButton;

        [SerializeField]
        protected string SimulateSwipeLeftButton;

        [SerializeField]
        protected string SimulateSwipeRightButton;

        [SerializeField]
        protected string SimulateSwipeUpButton;

        public override IEnumerable<RegisteredInputSource> GetInputSources()
        {
            yield return new RegisteredInputSource<SwipeSource, SwipeEventData>((source, data) => source.OnSwipe(data));
        }

        public override void Process()
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.OnPointerDown(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                this.OnPointerUp(Input.mousePosition);
            }

            // Process simulation.
            if (!string.IsNullOrEmpty(this.SimulateSwipeLeftButton) && Input.GetButtonDown(this.SimulateSwipeLeftButton))
            {
                this.OnSwipe(SwipeDirection.Left);
            }
            if (!string.IsNullOrEmpty(this.SimulateSwipeRightButton)
                && Input.GetButtonDown(this.SimulateSwipeRightButton))
            {
                this.OnSwipe(SwipeDirection.Right);
            }
            if (!string.IsNullOrEmpty(this.SimulateSwipeUpButton) && Input.GetButtonDown(this.SimulateSwipeUpButton))
            {
                this.OnSwipe(SwipeDirection.Up);
            }
            if (!string.IsNullOrEmpty(this.SimulateSwipeDownButton) && Input.GetButtonDown(this.SimulateSwipeDownButton))
            {
                this.OnSwipe(SwipeDirection.Down);
            }
        }

        private SwipeDirection DetectSwipe()
        {
            // Get the direction from the mouse position when Fire1 is pressed to when it is released.
            var swipeData = (this.pointerUpPosition - this.pointerDownPosition).normalized;

            // If the direction of the swipe has a small width it is vertical.
            var swipeIsVertical = Mathf.Abs(swipeData.x) < this.MinSwipeDistance;

            // If the direction of the swipe has a small height it is horizontal.
            var swipeIsHorizontal = Mathf.Abs(swipeData.y) < this.MinSwipeDistance;

            // If the swipe has a positive y component and is vertical the swipe is up.
            if (swipeData.y > 0f && swipeIsVertical)
            {
                return SwipeDirection.Up;
            }

            // If the swipe has a negative y component and is vertical the swipe is down.
            if (swipeData.y < 0f && swipeIsVertical)
            {
                return SwipeDirection.Down;
            }

            // If the swipe has a positive x component and is horizontal the swipe is right.
            if (swipeData.x > 0f && swipeIsHorizontal)
            {
                return SwipeDirection.Right;
            }

            // If the swipe has a negative x component and is vertical the swipe is left.
            if (swipeData.x < 0f && swipeIsHorizontal)
            {
                return SwipeDirection.Left;
            }

            // If the swipe meets none of these requirements there is no swipe.
            return SwipeDirection.None;
        }

        private void OnPointerDown(Vector2 position)
        {
            this.pointerDownPosition = position;
        }

        private void OnPointerUp(Vector2 position)
        {
            this.pointerUpPosition = position;

            var swipeDirection = this.DetectSwipe();
            if (swipeDirection != SwipeDirection.None)
            {
                this.OnSwipe(swipeDirection);
            }
        }

        private void OnSwipe(SwipeDirection swipeDirection)
        {
            Debug.Log("Swipe detected: " + swipeDirection);

            var swipeEventData = new SwipeEventData(null) { Direction = swipeDirection };
            this.OnGestureDetected(swipeEventData);
        }
    }
}