using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Shake(2);
            }

            if (cameraShake != null && cameraShake.enabled && !GlobalDisableCameraShake)
            {

                time += Time.deltaTime;
#if UNITY_EDITOR
                if (!cameraShake.isShaking)
                {
                    cameraShake.fetchCameras();
                }
#endif
                cameraShake.animate(time);
                if (time > cameraShake.duration)
                {
                    time = 0;
                    cameraShake.enabled = false;
                }
            }
        }

        public void Shake(float duration)
        {
            time = 0;
            cameraShake.duration = duration;
            cameraShake.enabled = true;
        }
    }
}