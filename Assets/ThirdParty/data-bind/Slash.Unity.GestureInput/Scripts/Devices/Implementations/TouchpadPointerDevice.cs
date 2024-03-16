using System;
using UnityEngine;

namespace Slash.Unity.GestureInput.Devices.Implementations
{
    [Serializable]
    public class TouchPadPointerDevice : IPointerDevice
    {
        private bool isDown;

        private Vector2 position;

        public Vector2 GetPosition()
        {
            return this.position;
        }

        public bool IsDown()
        {
            return this.isDown;
        }

        public void Update()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                var newTouchPadPointerPosition = (Vector2) UnityEngine.Input.mousePosition;

                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    this.isDown = true;
                }

                this.position = newTouchPadPointerPosition;
            }
            else
            {
                this.isDown = false;
            }
        }
    }
}