using UnityEngine;

namespace Demo.Scripts.Camera
{
    public class CameraRatio : MonoBehaviour
    {
        public SpriteRenderer backgroundSprite;

        void Start()
        {
            if (backgroundSprite != null)
            {
                AdjustCameraToSprite();
            }
            else
            {
                Debug.LogError("Background sprite is not assigned!");
            }
        }

        void AdjustCameraToSprite()
        {
            UnityEngine.Camera mainCamera = UnityEngine.Camera.main;

            float spriteWidth = backgroundSprite.bounds.size.x;
            float spriteHeight = backgroundSprite.bounds.size.y;

            float screenRatio = (float)Screen.width / Screen.height;
            float targetRatio = spriteWidth / spriteHeight;

            if (screenRatio >= targetRatio)
            {
                // Fit height
                mainCamera.orthographicSize = spriteHeight / 2;
            }
            else
            {
                // Fit width
                mainCamera.orthographicSize = spriteWidth / 2 / mainCamera.aspect;
            }
        }
    }
}
