using UnityEngine;

namespace _Game.Features
{
    public class PreviewDragPane : MonoBehaviour
    {
        public static PreviewDragPane Instance;
        private void Awake()
        {
            Instance = this;
        }
    }
}
