using _Base.Scripts.RPG.Behaviours.AimTarget;
using DG.Tweening;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class CannonRenderer : MonoBehaviour
    {
        [SerializeField]
        private AimTargetBehaviour _aimTargetBehaviour;

        [SerializeField] SkeletonAnimation _skeletonAnimation;
        [SerializeField] Skeleton skeleton;

        [SerializeField] private Transform _crosshair;
        DG.Tweening.Sequence blinkSequence;

        private void Start()
        {
            skeleton = _skeletonAnimation?.skeleton;
        }
        private void Update()
        {
            //_spriteRenderer.color = _aimTargetBehaviour.IsReadyToAttack ? Color.red : Color.white;

            if (_aimTargetBehaviour.IsReadyToAttack)
            {
                _crosshair.position = _aimTargetBehaviour.LockedPosition;
                _crosshair.gameObject.SetActive(true);
            }
            else
            {
                _crosshair.gameObject.SetActive(false);
            }

        }

        public void Blink()
        {
            Debug.Log(skeleton.GetColor());
            if (blinkSequence != null)
                return;
            blinkSequence = DOTween.Sequence();
            blinkSequence.Append(DOTween.ToAlpha(() => skeleton.GetColor(), x =>
            {
                skeleton.SetColor(x);

            }, 0.2f, 0.25f));
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
            skeleton.SetColor(Color.white);
        }
    }
}