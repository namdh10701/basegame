using System;
using _Base.Scripts.RPG.Behaviours.AimTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Behaviours.AttackStrategies;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
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

            if (entity is Cannon cannon)
            {
                CannonStats cannonStats = (CannonStats)cannon.Stats;

                if (cannonStats.Ammo.Value <= cannonStats.Ammo.MinValue)
                {
                    cannon.OnEmptyAmmo();
                    return;
                }
                else
                {
                    cannonStats.Ammo.SetValue(cannonStats.Ammo.Value - 1);
                }
            }
            // DoAttack();

            attackStrategy.SetData(entity, shootPosition, projectilePrefab, aimTargetBehaviour.FollowTargetBehaviour.FindTargetBehaviour.Strategy, aimTargetBehaviour.LockedPosition);
            attackStrategy.DoAttack();
        }

        // protected abstract void DoAttack();

        private void Start() { }
    }
}