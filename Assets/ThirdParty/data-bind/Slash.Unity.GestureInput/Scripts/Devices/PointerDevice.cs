using UnityEngine;

namespace Slash.Unity.GestureInput.Devices
{
    public abstract class PointerDevice : MonoBehaviour
    {
        public abstract Vector2 GetPosition();

        public abstract bool IsDown();

        public override string ToString()
        {
            return string.Format("Position: {0}\nIs Down: {1}", this.GetPosition(), this.IsDown());
        }
    }
}