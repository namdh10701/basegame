using System;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] NormalShot")]
    public class NormalShot: AttackStrategy
    {
        protected Transform shootPosition; 
        private Entity projectilePrefab;
        private Vector3 TargetPosition;
        private FindTargetStrategy findTargetStrategy;
        private IShooter shooter;
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
            var projectile = SpawnProjectile(shootDirection);
            
        }

        protected virtual Projectile SpawnProjectile(Quaternion shootDirection)
        {
            var projectileEntity = Object.Instantiate(projectilePrefab, shootPosition.position, shootDirection, null);
            Debug.Log(projectileEntity.transform.position);
            var projectile = projectileEntity.GetComponent<Projectile>();

            if (projectile == null)
            {
                throw new Exception("Can not find projectile component in prefab");
            }
            
            projectile.findTargetStrategy = findTargetStrategy;
            
            // var dec = new GameObject().AddComponent<DecreaseHealthPointEffect>();
            // dec.Amount = 100;
            // projectile.AddCarryingEffect<DecreaseHealthPointEffect>().Amount = 100;
            // projectile.OutgoingEffects.Add(new DecreaseHealthEffect(100));
            // projectile.OutgoingEffects.Add(new DrainHealthEffect(50, 1, 3));
            
            //projectile.moveSpeed.BaseValue = 100;


            // var fighterStats = fighter.FighterStats;
            // fighter.AttackStrategy.DoAttack();

            // projectile.OutgoingEffects.Add(new DecreaseHealthEffect(fighterStats.AttackDamage.Value));

            // shooter.FighterStats.AttackDamage.Value
            shooter.BulletEffects.ForEach(v => projectile.OutgoingEffects.Add(v));

            return projectile;
        }
        
        protected virtual Quaternion CalculateShootDirection()
        {
            var attackAccuracy = shooter.FighterStats.AttackAccuracy;
            var targetPosition = TargetPosition;
            targetPosition.x += Random.Range(-attackAccuracy.Value, attackAccuracy.Value);
            targetPosition.y += Random.Range(-attackAccuracy.Value, attackAccuracy.Value);

            var direction = targetPosition - shootPosition.position;
            var aimDirection = Quaternion.LookRotation(Vector3.forward, direction);
            return aimDirection;
        }
    }
}