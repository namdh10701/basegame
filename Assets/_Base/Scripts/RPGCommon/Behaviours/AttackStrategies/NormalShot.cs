using System;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] NormalShot")]
    public class NormalShot : AttackStrategy
    {
        Cannon Cannon;
        protected Transform shootPosition;
        private Entity projectilePrefab;
        private Vector3 TargetPosition;
        private FindTargetStrategy findTargetStrategy;
        private IShooter shooter;
        private void Awake()
        {
            Cannon = GetComponent<Cannon>();
        }
        public override void SetData(Entity shooter, Transform shootPosition, Entity projectilePrefab, FindTargetStrategy findTargetStrategy, Vector3 TargetPosition)
        {
            if (shooter is not IShooter fighter)
            {
                throw new Exception("IShooter not found");
            }

            this.shooter = fighter;
            this.shootPosition = shootPosition;
            this.projectilePrefab = projectilePrefab;
            this.findTargetStrategy = findTargetStrategy;
            this.TargetPosition = TargetPosition;
        }

        public override void DoAttack()
        {
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);
            if (transform.gameObject.name == "slow")
            {
                projectile.transform.localScale = Vector3.one * 1.5f;
            }
        }

        protected virtual Projectile SpawnProjectile(Quaternion shootDirection, Transform ShootPosition = null)
        {
            var projectileEntity = Object.Instantiate(projectilePrefab, ShootPosition.position, shootDirection, null);
            var projectile = projectileEntity.GetComponent<Projectile>();
            AddProjectileModifiers(projectile);
            return projectile;
        }

        protected void AddProjectileModifiers(Projectile projectile)
        {
            CannonStats cannonStats = (CannonStats)Cannon.Stats;
            projectile.AddCritChance(new StatModifier(cannonStats.CriticalChance.Value, StatModType.Flat, 1));
            projectile.AddAccuaracy(new StatModifier(cannonStats.AttackAccuracy.Value, StatModType.Flat, 1));
            projectile.AddDamage(new StatModifier(cannonStats.AttackDamage.Value, StatModType.Flat, 1));
        }

        protected virtual Quaternion CalculateShootDirection()
        {
            float addAccuarcyFromProjectile = ((ProjectileStats)projectilePrefab.Stats).Accuracy.Value;
            var attackAccuracy = shooter.FighterStats.AttackAccuracy;
            var targetPosition = TargetPosition;
            targetPosition.x += Random.Range(-attackAccuracy.Value - addAccuarcyFromProjectile, attackAccuracy.Value + addAccuarcyFromProjectile);
            targetPosition.y += Random.Range(-attackAccuracy.Value - addAccuarcyFromProjectile, attackAccuracy.Value + addAccuarcyFromProjectile);

            var direction = targetPosition - shootPosition.position;
            var aimDirection = Quaternion.LookRotation(Vector3.forward, direction);
            return aimDirection;
        }
    }
}