using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class PhunGaiController : SkeletonBombController
    {
        PhunGaiModel model;
        public PhunGaiProjectile phunGaiProjectile;
        public override IEnumerator AttackSequence()
        {
            yield return base.AttackSequence();
        }

        public override bool IsReadyToAttack()
        {
            return !cooldownBehaviour.IsInCooldown;
        }

        public override IEnumerator StartActionCoroutine()
        {
            yield return base.StartActionCoroutine();
        }

        protected override void DoAttack()
        {
            SelectCells();
            EnemyStats enemyStats = enemyModel.Stats as EnemyStats;
            PhunGaiProjectile projectile = Instantiate(phunGaiProjectile);
            projectile.SetData(enemyAttackData, shootPos.transform.position, 0, enemyStats.AttackDamage.Value);
            projectile.Launch();
        }

        void SelectCells()
        {
            EnemyStats enemyStats = enemyModel.Stats as EnemyStats;
            enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = gridPicker.PickCells(transform, attackPatternProfile, out Cell centerCell);
            enemyAttackData.CenterCell = centerCell;
        }
    }
}