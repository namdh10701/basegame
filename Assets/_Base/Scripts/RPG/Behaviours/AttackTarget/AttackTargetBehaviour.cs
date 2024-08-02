using _Base.Scripts.RPG.Behaviours.AimTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Features.Gameplay;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Base.Scripts.RPG.Behaviours.AttackTarget
{
    public class AttackTargetBehaviour : MonoBehaviour
    {
        public AimTargetBehaviour aimTargetBehaviour;
        public IAttackStrategy attackStrategy;
        public Cannon entity;
        public Transform shootPosition;
        public CannonProjectile projectilePrefab;
        public SpineAnimationCannonHandler Animation;
        public Transform rotateTransform;

        private void Awake()
        {
            Assert.IsNotNull(aimTargetBehaviour);
            attackStrategy = (entity as IFighter).AttackStrategy;
            Animation.OnShoot += DoAttack;

        }


        public void DoAttack()
        {
            if (entity != null)
            {
                RangedStat Ammo = ((CannonStats)entity.Stats).Ammo;
                if (Ammo.Value <= Ammo.MinValue)
                    return;
                if (entity.UsingAmmo.AmmoType == AmmoType.Standard && entity.FeverState != CannonFeverState.None)
                {

                }
                else
                {
                    attackStrategy.Consume(Ammo);
                }
            }
            attackStrategy.SetData(entity, shootPosition, projectilePrefab, rotateTransform.up);
            attackStrategy.DoAttack();
        }

        public void Attack()
        {
            RangedStat Ammo = ((CannonStats)entity.Stats).Ammo;
            if (Ammo.Value <= Ammo.MinValue)
                return;
            PlayAttackAnimation();
        }

        public virtual void PlayAttackAnimation()
        {
            Animation.PlayShootAnim(false);
        }

    }
}