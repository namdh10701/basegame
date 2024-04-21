using _Base.Scripts.RPG.Behaviours.AimTarget;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class CannonRenderer: MonoBehaviour
    {
        [SerializeField]
        private AimTargetBehaviour _aimTargetBehaviour;
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private Transform _crosshair;

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
    }
}