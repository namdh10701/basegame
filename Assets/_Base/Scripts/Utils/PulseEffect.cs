using UnityEngine;

namespace _Base.Scripts.Utils
{
    public class PulseEffect : MonoBehaviour
    {
        public float pulseSpeed = 1f; // Speed of the pulsing effect
        public float minScale = 0.8f; // Minimum scale factor
        public float maxScale = 1.2f; // Maximum scale factor

        private RectTransform rectTransform;
        private float scaleFactor;
        private bool scalingUp = true;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.LogError("PulseEffect script must be attached to a GameObject with a RectTransform.");
            }

            scaleFactor = minScale; // Start at the minimum scale
        }

        private void Update()
        {
            if (rectTransform != null)
            {
                // Update the scale factor based on the pulse speed and direction
                if (scalingUp)
                {
                    scaleFactor += pulseSpeed * Time.deltaTime;
                    if (scaleFactor >= maxScale)
                    {
                        scaleFactor = maxScale;
                        scalingUp = false;
                    }
                }
                else
                {
                    scaleFactor -= pulseSpeed * Time.deltaTime;
                    if (scaleFactor <= minScale)
                    {
                        scaleFactor = minScale;
                        scalingUp = true;
                    }
                }

                // Apply the scale factor to the RectTransform
                rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
            }
        }
    }
}