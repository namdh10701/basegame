using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEnemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] FieldOfView fieldOfView;
        public void StartAiming()
        {
            fieldOfView.TurnOn();
            StartCoroutine(StartAimingCoroutine());
        }

        IEnumerator StartAimingCoroutine()
        {
            float duration = 1f;
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                fieldOfView.FOV = Mathf.Lerp(25, 4, elapsedTime / duration);
                yield return null;
            }
            fieldOfView.FOV = 4;
        }
    }
}