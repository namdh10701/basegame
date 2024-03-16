using Slash.Unity.GestureInput.Devices;
using UnityEditor;
using UnityEngine;

namespace Slash.Unity.GestureInput.Editor.Devices.Implementations
{
    [CustomEditor(typeof (PointerDevice))]
    public class PointerDeviceEditor : UnityEditor.Editor
    {
        public override bool HasPreviewGUI()
        {
            return Application.isPlaying;
        }

        public override void OnPreviewGUI(Rect rect, GUIStyle background)
        {
            var device = (PointerDevice) this.target;
            GUILayout.Label(device.ToString());
        }

        public override bool RequiresConstantRepaint()
        {
            return Application.isPlaying;
        }
    }
}