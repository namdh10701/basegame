using _Game.Scripts.Battle;
using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Utils;
using _Game.Scripts.Entities;

namespace _Game.Features.Gameplay
{
    public class JellyFishController : EnemyController
    {
        bool isCurrentAttackLeftHand;
        public CameraShake cameraShake;
        private JellyFishAnimation jellyFishAnimation;
        private CooldownBehaviour cooldownBehaviour;
        [SerializeField] private JellyFishAttack attack;

        public override void Initialize(EnemyModel enemyModel, EffectTakerCollider effectTakerCollider, Blackboard blackboard, MBTExecutor mbtExecutor, Rigidbody2D body, SpineAnimationEnemyHandler anim)
        {
            cooldownBehaviour = ((JellyFishModel)enemyModel).cooldownBehaviour;
            jellyFishAnimation = anim as JellyFishAnimation;
            MoveAreaController moveArea = FindAnyObjectByType<MoveAreaController>();
            Area area = moveArea.GetArea(AreaType.Floor2Plus3);
            blackboard.GetVariable<AreaVariable>("MoveArea").Value = area;
            jellyFishAnimation.Attack.AddListener(Attack);
            jellyFishAnimation.AttackMeele.AddListener(AttackMelee);
            base.Initialize(enemyModel, effectTakerCollider, blackboard, mbtExecutor, body, anim);
        }
        public override IEnumerator AttackSequence()
        {
            if (isCurrentAttackLeftHand)
            {
                jellyFishAnimation.PlayIdleToAttackLoopLeftHand();
            }
            else
            {
                jellyFishAnimation.PlayIdleToAttackLoopRightHand();
            }
            yield return new WaitForSeconds(2);
            if (isCurrentAttackLeftHand)
            {
                jellyFishAnimation.PlayAttackLeftHand();
            }
            else
            {
                jellyFishAnimation.PlayAttackRightHand();
            }
            cooldownBehaviour.StartCooldown();
        }

        public override bool IsReadyToAttack()
        {
            return !cooldownBehaviour.IsInCooldown;
        }

        public override void Die()
        {
            base.Die();
            anim.PlayDie(() => Destroy(gameObject));
            StopAllCoroutines();
        }

        public override IEnumerator StartActionCoroutine()
        {
            EnemyStats enemyStats = enemyModel.Stats as EnemyStats;
            cooldownBehaviour.SetCooldownTime(enemyStats.ActionSequenceInterval.Value);
            jellyFishAnimation.Appear();
            cameraShake.Shake(3f);
            _Game.Scripts.BehaviourTree.Wander wander = mbtExecutor.GetComponent<_Game.Scripts.BehaviourTree.Wander>();
            effectTakerCollider.gameObject.SetActive(false);
            yield return new WaitForSeconds(4.5f);
            effectTakerCollider.gameObject.SetActive(true);
            float rand = Random.Range(0, 1f);
            if (rand < .5f)
            {
                wander.ToLeft();
            }
            else
            {
                wander.ToRight();
            }

            cooldownBehaviour.StartCooldown();
            wander.UpdateTargetDirection(-50, 50);
        }

        public void Attack()
        {
            if (isCurrentAttackLeftHand)
                attack.DoLeftAttack();
            else
                attack.DoRightAttack();
            isCurrentAttackLeftHand = !isCurrentAttackLeftHand;
        }

        public void AttackMelee()
        {
            if (isAttackLefthand)
            {
                attack.DoLeftMeleeAttack();
            }
            else
            {
                attack.DoRightMelleAttack();
            }
        }

        public IEnumerator MeleeAttackSequence()
        {
            float rand = Random.Range(0, 1f);
            if (rand < .5f)
            {
                yield return LeftAttackSequence();
            }
            else
            {
                yield return RightAttackSequence();
            }
        }
        bool isAttackLefthand;
        IEnumerator LeftAttackSequence()
        {
            cooldownBehaviour.StartCooldown();
            isAttackLefthand = true;
            jellyFishAnimation.PlayAttackMeeleLeftHand();
            yield return new WaitForSeconds(1f);
        }

        IEnumerator RightAttackSequence()
        {
            cooldownBehaviour.StartCooldown();
            isAttackLefthand = false;
            jellyFishAnimation.PlayAttackMeeleRightHand();
            yield return new WaitForSeconds(1f);
        }
    }
}