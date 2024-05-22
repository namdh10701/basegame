
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class RangedAttack : CellAttacker
    {
        [SerializeField] Transform shootPos;
        [SerializeField] EnemyProjectile projectilePrefab;
        public override void DoAttack()
        {
            if (enemyAttackData == null)
            {
                return;
            }
            EnemyProjectile projectile = Instantiate(projectilePrefab);
            projectile.SetData(enemyAttackData, shootPos.position);
            projectile.Launch();
        }
    }
}