using _Base.Scripts.RPG.Behaviours.AimTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Base.Scripts.RPG.Behaviours.AttackTarget
{
    public class AttackTargetBehaviour : MonoBehaviour
    {
        public AimTargetBehaviour aimTargetBehaviour;
        public IAttackStrategy attackStrategy;
        public Entity entity;
        public Transform shootPosition;
        public Entity projectilePrefab;
        public SpineAnimationCannonHandler Animation;

        private void Awake()
        {
            Assert.IsNotNull(aimTargetBehaviour);
            attackStrategy = (entity as IFighter).AttackStrategy;
            Animation.OnShoot += DoAttack;
        }


        public void DoAttack()
        {
            attackStrategy.SetData(entity, shootPosition, projectilePrefab, aimTargetBehaviour.FollowTargetBehaviour.FindTargetBehaviour.Strategy, aimTargetBehaviour.LockedPosition);
            attackStrategy.DoAttack();
        }

        public void Attack()
        {
            if (!aimTargetBehaviour.IsReadyToAttack)
            {
                return;
            }

            if (entity != null)
            {
                RangedStat Ammo = ((CannonStats)entity.Stats).Ammo;

                if (Ammo.Value <= Ammo.MinValue)
                    return;
                Ammo.StatValue.BaseValue--;
            }
            PlayAttackAnimation();
        }

        public virtual void PlayAttackAnimation()
        {
            Animation.PlayShootAnim(false);
        }

    }
}