
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class RangedAttack : EnemyAttackBehaviour
    {
        [SerializeField] Transform shootPos;
        [SerializeField] EnemyProjectile projectilePrefab;
        [SerializeField] PositionPool positionPool;
        public override void DoAttack()
        {
            if (EnemyAttackData == null)
            {
                return;
            }
            EnemyProjectile projectile = Instantiate(projectilePrefab);
            projectile.SetData(EnemyAttackData, shootPos.position);
            projectile.Launch();
        }
    }
}