using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class ReloadSign : MonoBehaviour
    {
        Tween tween;
        public void Play()
        {
            if (tween != null)
            {
                return;
            }
            tween = transform.DOLocalRotate(new Vector3(0, 0, -360), 1.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            tween.SetLoops(-1);
            tween.Play();
        }

        public void Stop()
        {
            if (tween != null)
            {
                tween.Kill();
                tween = null;
                transform.rotation = Quaternion.identity;
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }


        private void OnDestroy()
        {
            if (tween != null)
            {
                tween.Kill();
            }
        }
    }
}