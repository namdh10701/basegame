using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Demo
{
    public class HitFx : MonoBehaviour
    {
        [SerializeField] SpriteRenderer hit1;
        [SerializeField] SpriteRenderer hit2;
        Coroutine playCoroutine;

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
            hit1.DOColor(Color.white, .15f);
            hit2.DOColor(Color.white, .15f);

            yield return new WaitForSeconds(.15f);
            hit1.DOColor(Color.clear, .3f);
            hit2.DOColor(Color.clear, .3f);
            yield return new WaitForSeconds(.3f);
            playCoroutine = null;
        }
    }
}