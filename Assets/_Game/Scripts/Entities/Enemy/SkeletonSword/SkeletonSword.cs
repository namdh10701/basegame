using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{

    public class SkeletonSword : EnemyModel
    {
        public override IEnumerator AttackSequence()
        {
            enemyView.PlayAttack();
            State = EnemyState.Attacking;
            cooldownBehaviour.StartCooldown();
            yield break;
        }

        public override void DoAttack()
        {
            if (findTargetBehaviour.MostTargets.Count > 0)
            {
                List<Cell> cells = gridPicker.PickCells(transform, attackPatternProfile, out Cell centerCell);
                EnemyAttackData enemyAtk = new EnemyAttackData();
                enemyAtk.CenterCell = centerCell;
                enemyAtk.TargetCells = cells;
                DecreaseHealthEffect dhe = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
                dhe.transform.position = centerCell.transform.position;
                dhe.Amount = _stats.AttackDamage.Value;
                enemyAtk.Effects = new List<Effect> { dhe };
                atkHandler.ProcessAttack(enemyAtk);
            }
        }
    }
}