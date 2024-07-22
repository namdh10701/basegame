using UnityEngine;
using static CartoonFX.CFXR_Effect;

namespace _Game.Scripts.Utils
{
    public class CameraShake : MonoBehaviour
    {
        [Space]
        public CartoonFX.CFXR_Effect.CameraShake cameraShake;
        // Update is called once per frame
        float time;
        private void Awake()
        {
        }
        bool isActive;
        void Update()
        {

            if (cameraShake != null && cameraShake.enabled && !GlobalDisableCameraShake)
            {

                time += Time.deltaTime;

                if (!cameraShake.isShaking)
                {
                    cameraShake.fetchCameras();
                }

                cameraShake.animate(time);
                if (time > cameraShake.duration)
                {
                    time = 0;
                    cameraShake.enabled = false;
                }
            }
        }

        public void Shake(float duration, Vector3 strength)
        {
            time = 0;
            cameraShake.shakeStrength = strength;
            cameraShake.duration = duration;
            cameraShake.enabled = true;
        }
    }
}