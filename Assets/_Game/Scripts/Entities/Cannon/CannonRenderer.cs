using _Base.Scripts.RPG.Behaviours.AimTarget;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class CannonRenderer : MonoBehaviour
    {
        [SerializeField]
        private AimTargetBehaviour _aimTargetBehaviour;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private Transform _crosshair;
        Sequence blinkSequence;
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
            if (blinkSequence != null)
                return;
            blinkSequence = DOTween.Sequence();
            blinkSequence.Append(_spriteRenderer.DOFade(.5f, 0.25f));
        }
        public void StopBlink()
        {
            if (blinkSequence == null)
                return;
            blinkSequence.Kill();
            _spriteRenderer.color = Color.white;
        }
    }
}