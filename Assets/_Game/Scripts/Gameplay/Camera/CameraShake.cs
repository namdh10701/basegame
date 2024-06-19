using _Base.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public static class CameraShake
    {
        public static void Shake(Camera camera, float duration, float magnitude)
        {
            Coroutines.StartCoroutine(ShakeCoroutine(camera, duration, magnitude));
        }

        public static IEnumerator ShakeCoroutine(Camera camera, float duration, float magnitude)
        {
            Vector3 originalPos = new Vector3(0, 0, -10);
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                float xOffset = Random.Range(-.5f, .5f) * magnitude;
                float yOffset = Random.Range(-.5f, .5f) * magnitude;

                camera.transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            camera.transform.localPosition = originalPos;
        }

    }
}