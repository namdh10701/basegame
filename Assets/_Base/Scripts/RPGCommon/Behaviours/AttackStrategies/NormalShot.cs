using _Base.Scripts.RPG.Behaviours.AttackTarget;
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
        protected Cannon Cannon;
        protected Transform shootPosition;
        protected CannonProjectile projectilePrefab;
        private Vector2 shootDirection;
        private void Awake()
        {
            Cannon = GetComponent<Cannon>();
        }
        public override void SetData(Cannon shooter, Transform shootPosition, CannonProjectile projectilePrefab, Vector3 shootDirection)
        {

            this.shootPosition = shootPosition;
            this.projectilePrefab = projectilePrefab;
            this.shootDirection = shootDirection;
        }

        public override void DoAttack()
        {
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);
        }

        protected virtual Projectile SpawnProjectile(Quaternion shootDirection, Transform ShootPosition = null)
        {
            var projectileEntity = Object.Instantiate(projectilePrefab, ShootPosition.position, shootDirection, null);
            var projectile = projectileEntity.GetComponent<Projectile>();
            projectile.gameObject.SetActive(true);
            AddProjectileModifiers(projectile);
            return projectile;
        }

        protected void AddProjectileModifiers(Projectile projectile)
        {
            CannonStats cannonStats = (CannonStats)Cannon.Stats;
            projectile.AddCritChance(new StatModifier(cannonStats.CriticalChance.Value, StatModType.Flat, 1));
            ProjectileStats pStats = (ProjectileStats)Cannon.usingBullet.Projectile.Stats;
            float addCritChanceFromProjectile = pStats.CritChance.Value;
            float totalCritChance = addCritChanceFromProjectile + Cannon.FighterStats.CriticalChance.Value;
            float addCritDmgFromProjectile = pStats.CritDamage.Value;
            float totalCritDmg = addCritDmgFromProjectile + Cannon.FighterStats.CriticalDamage.Value;
            float addDmg = pStats.Damage.Value;
            float totalDmg = addDmg + Cannon.FighterStats.AttackDamage.Value;
            float finalDmg = 0;

            bool isCrit = UnityEngine.Random.Range(0f, 1f) < totalCritChance;
            if (isCrit)
            {
                finalDmg = totalDmg * (totalCritDmg);
            }
            else
            {
                finalDmg = totalDmg;
            }

            projectile.SetDamage(finalDmg, isCrit);


        }

        protected virtual Quaternion CalculateShootDirection()
        {
            float addAccuarcyFromProjectile = ((ProjectileStats)projectilePrefab.Stats).Accuracy.Value;
            float shooterAccuracy = Cannon.FighterStats.AttackAccuracy.Value;
            float totalAccuaracy = addAccuarcyFromProjectile + shooterAccuracy;
            Vector2 finalShootDirection = Quaternion.Euler(0, 0, Random.Range(-totalAccuaracy, totalAccuaracy)) * shootDirection;
            var aimDirection = Quaternion.LookRotation(Vector3.forward, finalShootDirection);
            return aimDirection;
        }

        public override void Consume(RangedStat ammo)
        {
            int numOfProjectileCanProvide = (int)ammo.Value;
            ActualNumOfProjectile = Mathf.Min(numOfProjectileCanProvide, (int)Mathf.Max(1, NumOfProjectile.Value));
            ammo.StatValue.BaseValue -= ActualNumOfProjectile;
        }
    }
}