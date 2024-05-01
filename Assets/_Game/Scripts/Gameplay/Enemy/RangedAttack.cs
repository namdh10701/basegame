
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class RangedAttack : EnemyAttackBehaviour
    {
        [SerializeField] Transform shootPos;
        [SerializeField] Projectile projectilePrefab;
        public override void DoAttack()
        {
            SelectCells();
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.position = shootPos.position;
            projectile.transform.up = (centerCell.transform.position - shootPos.position).normalized;

        }
    }
}