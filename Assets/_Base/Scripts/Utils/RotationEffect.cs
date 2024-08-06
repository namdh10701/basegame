using UnityEngine;

namespace _Base.Scripts.Utils
{
    public class RotationEffect : MonoBehaviour
    {
        public float rotationSpeed = 100f; // Speed of the rotation in degrees per second

        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.LogError("InfiniteRotation script must be attached to a GameObject with a RectTransform.");
            }
        }

        private void Update()
        {
            if (rectTransform != null)
            {
                // Rotate the image around its center
                rectTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }
        }
    }
}