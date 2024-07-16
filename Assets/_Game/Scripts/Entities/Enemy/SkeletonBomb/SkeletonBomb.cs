using _Base.Scripts.RPG.Effects;
using _Game.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{

    public class SkeletonBomb : EnemyModel
    {
        public SkeletonBombProjectile skeletonBombProjectilePrefab;
        public Transform shootPos;
        protected override void Awake()
        {
            base.Awake();

        }
        public override void DoAttack()
        {
            SelectCells();
            SkeletonBombProjectile projectile = Instantiate(skeletonBombProjectilePrefab);
            projectile.SetData(enemyAttackData, shootPos.transform.position, -15, _stats.AttackDamage.Value);
            projectile.Launch();
        }

        void SelectCells()
        {
            enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = gridPicker.PickCells(transform, attackPatternProfile, out Cell centerCell);
            enemyAttackData.CenterCell = centerCell;
            DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
            decreaseHp.Amount = _stats.AttackDamage.Value;// Take from boss stats;
            decreaseHp.ChanceAffectCell = .5f;
            enemyAttackData.Effects = new List<Effect> { decreaseHp };
        }

        public override IEnumerator AttackSequence()
        {
            enemyView.PlayAttack();
            yield break;
        }
    }
}