using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class CameraShake : MonoBehaviour
    {
        public float duration;
        public float magnitude;

        public IEnumerator Shake(float duration, float magnitude)
        {
            Vector3 originalPos = new Vector3(0, 0, -10);
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                float xOffset = Random.Range(-.5f, .5f) * magnitude;
                float yOffset = Random.Range(-.5f, .5f) * magnitude;

                transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            transform.localPosition = originalPos;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(Shake(duration, magnitude));
            }
        }

    }
}