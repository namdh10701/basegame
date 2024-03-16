using Slash.Unity.GestureInput.Devices;
using UnityEngine;

namespace Slash.Unity.GestureInput.Examples.TriggerExample
{
    public class MousePointerDevice : IPointerDevice
    {
        public Vector2 GetPosition()
        {
            return Input.mousePosition;
        }

        public bool IsDown()
        {
            return Input.GetMouseButton(0);
        }
    }
}