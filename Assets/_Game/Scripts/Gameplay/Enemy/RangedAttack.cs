
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class RangedAttack : CellAttacker
    {
        [HideInInspector]public Transform ShootPos;
        [SerializeField] EnemyProjectile projectilePrefab;
        public override void DoAttack()
        {
            if (enemyAttackData == null)
            {
                return;
            }
            EnemyProjectile projectile = Instantiate(projectilePrefab);
            projectile.SetData(enemyAttackData, ShootPos.position);
            projectile.Launch();
        }
    }
}