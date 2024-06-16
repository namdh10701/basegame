
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    [ExecuteAlways]
    public class ShipSpeed : MonoBehaviour
    {
        public Renderer foam1;
        public Renderer foam2;
        public Renderer bigFoam;


        Material foam1Mat;
        Material foam2Mat;
        Material bigFoamMat;
        public float shipSpeed;
        [SerializeField] Vector2 currentOffset;
        public Vector2 direction;
        public Vector2 scrollSpeed;

        private void OnEnable()
        {
            foam1Mat = foam1.sharedMaterial;
            foam2Mat = foam2.sharedMaterial;
            bigFoamMat = bigFoam.sharedMaterial;
        }

        private void Update()
        {
            currentOffset += scrollSpeed * Time.deltaTime * direction;
            foam1Mat.SetVector("_Offset", currentOffset);
            bigFoamMat.SetVector("_Offset", currentOffset);
        }

        public void AdjustSpeed(Vector2 newSpeed, float duration)
        {
            StartCoroutine(AdjustSpeedCoroutine(newSpeed, duration));
        }

        IEnumerator AdjustSpeedCoroutine(Vector2 newSpeed, float duration)
        {
            float elapsedTime = 0;
            Vector2 startSpeed = scrollSpeed;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                scrollSpeed = Vector2.Lerp(startSpeed, newSpeed, elapsedTime / duration);
                yield return null;
            }
            scrollSpeed = newSpeed;
        }

    }
}
