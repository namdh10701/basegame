using _Base.Scripts.RPG.Behaviours.AimTarget;
using DG.Tweening;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class CannonRenderer : MonoBehaviour
    {
        [SerializeField] SkeletonAnimation _skeletonAnimation;
        [SerializeField] Skeleton skeleton;

        DG.Tweening.Sequence blinkSequence;

        private void Start()
        {
            skeleton = _skeletonAnimation?.skeleton;
        }
        public void Blink()
        {
            if (blinkSequence != null)
                return;
            blinkSequence = DOTween.Sequence();
            blinkSequence.Append(DOTween.ToAlpha(() => skeleton.GetColor(), x =>
            {
                skeleton.SetColor(x);

            }, 0.5f, 0.25f));
            blinkSequence.Append(DOTween.ToAlpha(() => skeleton.GetColor(), x =>
            {
                skeleton.SetColor(x);
            }, 1, 0.25f));
            blinkSequence.SetLoops(-1);

        }
        public void StopBlink()
        {
            if (blinkSequence == null)
                return;
            blinkSequence.Kill();

            blinkSequence = null;
            skeleton.SetColor(Color.white);
        }
    }
}