using UnityEngine;

namespace Slash.Unity.GestureInput.Devices.Implementations
{
    public class MousePointerDevice : PointerDevice
    {
        public override Vector2 GetPosition()
        {
            return Input.mousePosition;
        }

        public override bool IsDown()
        {
            return Input.GetMouseButton(0);
        }
    }
}