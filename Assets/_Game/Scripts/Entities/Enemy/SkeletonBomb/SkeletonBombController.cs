using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using _Game.Scripts.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class SkeletonBombController : SkeletonSwordController
    {
        public SkeletonBombProjectile skeletonBombProjectilePrefab;
        protected EnemyAttackData enemyAttackData;
        DecreaseHealthEffect decreaseHealthEffect;
        public Transform shootPos;
        protected override void DoAttack()
        {
            EnemyStats enemyStats = enemyModel.Stats as EnemyStats;
            SelectCells();
            SkeletonBombProjectile projectile = Instantiate(skeletonBombProjectilePrefab);
            projectile.SetData(enemyAttackData, shootPos.transform.position, -15, enemyStats.AttackDamage.Value);
            projectile.Launch();
        }

        void SelectCells()
        {
            EnemyStats enemyStats = enemyModel.Stats as EnemyStats;
            enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = gridPicker.PickCells(transform, attackPatternProfile, out Cell centerCell);
            enemyAttackData.CenterCell = centerCell;
            DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
            decreaseHp.Amount = enemyStats.AttackDamage.Value;// Take from boss stats;
            decreaseHp.ChanceAffectCell = .5f;
            enemyAttackData.Effects = new List<Effect> { decreaseHp };
        }
    }
}
