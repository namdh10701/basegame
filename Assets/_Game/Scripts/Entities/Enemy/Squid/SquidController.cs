using _Game.Scripts;
using _Game.Scripts.BehaviourTree;
using _Game.Scripts.Entities;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class SquidController : EnemyController
    {
        SquidAnimation squidAnimation;
        CooldownBehaviour cooldownBehaviour;
        _Game.Scripts.BehaviourTree.Wander wander;

        public override void Initialize(EnemyModel enemyModel, EffectTakerCollider effectTakerCollider, Blackboard blackboard, MBTExecutor mbtExecutor, Rigidbody2D body, SpineAnimationEnemyHandler anim)
        {
            squidAnimation = anim as SquidAnimation;
            cooldownBehaviour = ((SquidModel)enemyModel).CooldownBehaviour;
            MoveAreaController moveArea = FindAnyObjectByType<MoveAreaController>();
            blackboard.GetVariable<AreaVariable>("MoveArea").Value = moveArea.GetArea(AreaType.All);
            base.Initialize(enemyModel, effectTakerCollider, blackboard, mbtExecutor, body, anim);
        }
        public override IEnumerator AttackSequence()
        {
            squidAnimation.PlayAttack();
            yield return new WaitForSeconds(2);
            cooldownBehaviour.StartCooldown();
        }

        public override bool IsReadyToAttack()
        {
            return !cooldownBehaviour.IsInCooldown;
        }

        public override IEnumerator StartActionCoroutine()
        {
            wander = mbtExecutor.GetComponent<_Game.Scripts.BehaviourTree.Wander>();
            effectTakerCollider.enabled = false;
            squidAnimation.Appear();
            yield return new WaitForSeconds(1.5f);
            float rand = Random.Range(0, 1f);
            if (rand < .5f)
            {
                wander.ToLeft();
            }
            else
            {
                wander.ToRight();
            }
            wander.UpdateTargetDirection(-50, 50);
            effectTakerCollider.enabled = true;
            EnemyStats _stats = enemyModel.Stats as EnemyStats;
            cooldownBehaviour.SetCooldownTime(_stats.ActionSequenceInterval.Value);
            cooldownBehaviour.StartCooldown();
        }

        public override void Die()
        {
            base.Die();
            squidAnimation.PlayDie(() => Destroy(gameObject));
        }


    }
}