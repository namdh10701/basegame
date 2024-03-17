using UnityEngine;

namespace Slash.Unity.GestureInput.Devices
{
    public interface IPointerDevice
    {
        Vector2 GetPosition();
        bool IsDown();
    }
}