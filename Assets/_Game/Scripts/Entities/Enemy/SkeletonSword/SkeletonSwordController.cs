using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class SkeletonSwordController : EnemyController
    {
        SkeletonSword skeletonSword;
        public SkeletonSwordAnimation skelAnim;
        public AttackPatternProfile attackPatternProfile;
        public FindTargetBehaviour findTargetBehaviour;
        public CooldownBehaviour cooldownBehaviour;
        private void OnEnable()
        {
            skelAnim.OnAttack += DoAttack;
        }

        protected virtual void DoAttack()
        {

            if (findTargetBehaviour.MostTargets.Count > 0)
            {
                EnemyStats enemyStats = enemyModel.Stats as EnemyStats;
                List<Cell> cells = gridPicker.PickCells(transform, attackPatternProfile, out Cell centerCell);
                EnemyAttackData enemyAtk = new EnemyAttackData();
                enemyAtk.CenterCell = centerCell;
                enemyAtk.TargetCells = cells;
                DecreaseHealthEffect dhe = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
                dhe.transform.position = centerCell.transform.position;
                dhe.Amount = enemyStats.AttackDamage.Value;
                enemyAtk.Effects = new List<Effect> { dhe };
                atkHandler.ProcessAttack(enemyAtk);
            }

        }


        private void OnDisable()
        {
            skelAnim.OnAttack -= DoAttack;
        }
        public override IEnumerator AttackSequence()
        {
            skelAnim.PlayAttack();
            skeletonSword.State = EnemyState.Attacking;
            cooldownBehaviour.StartCooldown();
            yield break;
        }

        public override bool IsReadyToAttack()
        {
            return !cooldownBehaviour.IsInCooldown;
        }

        public override IEnumerator StartActionCoroutine()
        {
            skeletonSword = enemyModel as SkeletonSword;
            EnemyStats stat = enemyModel.Stats as EnemyStats;
            SetState(EnemyState.Entry);
            cooldownBehaviour.SetCooldownTime(stat.ActionSequenceInterval.Value);
            cooldownBehaviour.StartCooldown();
            yield return new WaitForSeconds(1f);
            SetState(EnemyState.Idle);
            yield break;
        }

        public void SetState(EnemyState state)
        {
            skeletonSword.State = state;
            if (state == EnemyState.Hiding || state == EnemyState.Entry)
            {
                skeletonSword.EffectTakerCollider.gameObject.SetActive(false);
            }
            else
            {
                skeletonSword.EffectTakerCollider.gameObject.SetActive(true);
            }
        }

        public override void Die()
        {
            base.Die();
            StopAllCoroutines();
            skeletonSword.State = EnemyState.Dead;
        }
    }
}
