using System;
using System.Collections;
using _Base.Scripts.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Base.Scripts.UI.Managers
{
    public enum Transition
    {
        CrossFade, None
    }
    public class ViewTransitionManager : SingletonMonoBehaviour<ViewTransitionManager>
    {
        [SerializeField] Image image;
        [SerializeField] float duration;
        Color transparent = new Color(0, 0, 0, 0);
        Color full = new Color(0, 0, 0, 1);
        DG.Tweening.Tween tween;
        private void Start()
        {
            image.color = transparent;
        }
        public void TransitCrossFade(Action activeView)
        {
            image.raycastTarget = true;
            image.DOFade(1, duration / 2).OnComplete
            (() =>
                {
                    activeView();
                    image.DOFade(0, duration / 2).OnComplete(() =>
                        image.raycastTarget = false);
                }
            );
        }

        public IEnumerator TransitIn()
        {
            if (tween != null)
            {
                tween.Kill();
            }
            image.color = transparent;
            tween = image.DOFade(1, duration / 2);

            yield return tween.WaitForCompletion();
        }
        public void TransitOut()
        {

            if (tween != null)
            {
                tween.Kill();
            }
            image.color = full;
            tween = image.DOFade(0, duration / 2);
        }
    }
}