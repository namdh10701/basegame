using System;
using _Base.Scripts.RPG.Behaviours.AimTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Behaviours.AttackStrategies;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Base.Scripts.RPG.Behaviours.AttackTarget
{
    public class AttackTargetBehaviour : MonoBehaviour
    {
        // public AttackAccuracy attackAccuracy;
        // public Transform attackTargetPosition;
        public AimTargetBehaviour aimTargetBehaviour;
        // public CollidedTargetChecker collidedTargetChecker;
        // public IAttackStrategy attackStrategy = new ShootTargetStrategy_Normal();

        public IAttackStrategy attackStrategy;
        public Entity entity;
        public Transform shootPosition;
        public Entity projectilePrefab;
        public SpineAnimation spineAnimation;

        private void Awake()
        {
            Assert.IsNotNull(aimTargetBehaviour);
            attackStrategy = (entity as IFighter).AttackStrategy;
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
            // DoAttack();
            //spineAnimation.PlayAnim("Attack", false);
            attackStrategy.SetData(entity, shootPosition, projectilePrefab, aimTargetBehaviour.FollowTargetBehaviour.FindTargetBehaviour.Strategy, aimTargetBehaviour.LockedPosition);
            attackStrategy.DoAttack();
        }

        // protected abstract void DoAttack();

        private void Start() { }
    }
}