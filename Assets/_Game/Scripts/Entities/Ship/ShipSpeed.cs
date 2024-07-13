
using System.Collections;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class ShipSpeed : MonoBehaviour
    {
        public Renderer[] foams;

        Material[] foamMats;
        [SerializeField] Vector2 currentOffset;
        public Vector2 direction = new Vector2(1, 1);
        public Vector2 scrollSpeed = new Vector2(.25f, 1);

        private void OnEnable()
        {
            foamMats = new Material[foams.Length];
            for (int i = 0; i < foams.Length; i++)
            {
                foamMats[i] = foams[i].material;
            }
        }

        private void Update()
        {
            currentOffset += scrollSpeed * Time.deltaTime * direction;
            if (foamMats == null)
            {
                return;
            }
            foreach (Material mat in foamMats)
            {
                mat.SetVector("_Offset", currentOffset);
            }
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
