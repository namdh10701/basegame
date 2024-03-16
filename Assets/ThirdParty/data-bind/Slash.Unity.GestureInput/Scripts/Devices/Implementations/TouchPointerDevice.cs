using UnityEngine;

namespace Slash.Unity.GestureInput.Devices.Implementations
{
    public class TouchPointerDevice : PointerDevice
    {
        public int TouchIndex;

        public override Vector2 GetPosition()
        {
            return this.TouchIndex < Input.touchCount ? Input.touches[this.TouchIndex].position : Vector2.zero;
        }

        public override bool IsDown()
        {
            return this.TouchIndex < Input.touchCount;
        }

        public override string ToString()
        {
            return string.Format("{0}, TouchIndex: {1}", base.ToString(), this.TouchIndex);
        }
    }
}