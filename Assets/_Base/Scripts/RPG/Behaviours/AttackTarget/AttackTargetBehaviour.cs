using _Base.Scripts.RPG.Behaviours.AimTarget;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Base.Scripts.RPG.Behaviours.AttackTarget
{
    public abstract class AttackTargetBehaviour: MonoBehaviour
    {
        // public AttackAccuracy attackAccuracy;
        // public Transform attackTargetPosition;
        public AimTargetBehaviour aimTargetBehaviour;
        // public CollidedTargetChecker collidedTargetChecker;

        private void Awake()
        {
            Assert.IsNotNull(aimTargetBehaviour);
        }

        public void Attack()
        {
            if (!aimTargetBehaviour.IsReadyToAttack)
            {
                return;
            }

            DoAttack();
        }

        protected abstract void DoAttack();
    }
}