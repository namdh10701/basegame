using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class PhunGaiModel : EnemyModel
    {
        public PhunGaiProjectile phunGaiProjectile;
        public Transform shootPos;

        public override IEnumerator AttackSequence()
        {
            enemyView.PlayAttack();
            cooldownBehaviour.StartCooldown();
            yield break;
        }

        public override void DoAttack()
        {
            SelectCells();
            PhunGaiProjectile projectile = Instantiate(phunGaiProjectile);
            projectile.SetData(enemyAttackData, shootPos.transform.position, 0, _stats.AttackDamage.Value);
            projectile.Launch();
        }

        void SelectCells()
        {
            enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = gridPicker.PickCells(transform, attackPatternProfile, out Cell centerCell);
            enemyAttackData.CenterCell = centerCell;
        }
    }
}