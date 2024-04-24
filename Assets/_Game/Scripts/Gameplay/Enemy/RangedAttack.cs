using _Base.Scripts.RPGCommon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class RangedAttack : EnemyAttackBehaviour
    {
        [SerializeField] Transform shootPos;
        [SerializeField] Demo.Scripts.Canon.Projectile projectilePrefab;
        public override void DoAttack()
        {
            Demo.Scripts.Canon.Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.position = shootPos.position;
            projectile.transform.up = (centerCell.transform.position - shootPos.position).normalized;
            
        }
    }
}