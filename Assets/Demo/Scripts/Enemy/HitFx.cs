using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Demo.Scripts.Enemy
{
    public class HitFx : MonoBehaviour
    {
        [SerializeField] SpriteRenderer hit1;
        [SerializeField] SpriteRenderer hit2;
        Coroutine playCoroutine;
        Tween tween1;
        Tween tween2;
        public void Play()
        {
            if (playCoroutine != null)
            {
                StopCoroutine(playCoroutine);
            }
            playCoroutine = StartCoroutine(PlayFx());
        }

        IEnumerator PlayFx()
        {
            hit1.color = Color.clear;
            hit2.color = Color.clear;
            tween1 = hit1.DOColor(Color.white, .15f);
            tween2 = hit2.DOColor(Color.white, .15f);

            yield return new WaitForSeconds(.15f);
            tween1 = hit1.DOColor(Color.clear, .3f);
            tween2 = hit2.DOColor(Color.clear, .3f);
            yield return new WaitForSeconds(.3f);
            playCoroutine = null;
        }

        private void OnDestroy()
        {
            tween1.Kill();
            tween2.Kill();
        }
    }

}