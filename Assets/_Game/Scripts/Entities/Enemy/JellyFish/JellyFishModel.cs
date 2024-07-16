using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.BehaviourTree;
using _Game.Scripts.Entities;
using _Game.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class JellyFishModel : EnemyModel
    {
        [Header("Jelly Fish")]
        [Space]
        [SerializeField] JellyFishAttack attack;
        JellyFishView jellyFishView;
        bool isCurrentAttackLeftHand;
        public override Stats Stats => _stats;

        protected override void Awake()
        {
            base.Awake();
            jellyFishView = enemyView as JellyFishView;
            MoveAreaController moveArea = FindAnyObjectByType<MoveAreaController>();
            Area area = moveArea.GetArea(AreaType.Floor2Plus3);
            blackboard.GetVariable<AreaVariable>("MoveArea").Value = area;

            _Game.Scripts.BehaviourTree.Wander wander = mbtExecutor.GetComponent<_Game.Scripts.BehaviourTree.Wander>();
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
            cooldownBehaviour.StartCooldown();
        }

        public override void DoAttack()
        {
            if (isCurrentAttackLeftHand)
                attack.DoLeftAttack();
            else
                attack.DoRightAttack();
            isCurrentAttackLeftHand = !isCurrentAttackLeftHand;
        }

        public void DoMeeleAttack()
        {
            if (isAttackLefthand)
            {
                attack.DoLeftMeleeAttack();
            }
            else
            {
                attack.DoRightMelleAttack();
            }
            cooldownBehaviour.StartCooldown();
        }

        public override IEnumerator AttackSequence()
        {
            if (isCurrentAttackLeftHand)
            {
                jellyFishView.PlayIdleToAttackLoopLeftHand();
            }
            else
            {
                jellyFishView.PlayIdleToAttackLoopRightHand();
            }
            yield return new WaitForSeconds(2);
            if (isCurrentAttackLeftHand)
            {
                jellyFishView.PlayAttackLeftHand();
            }
            else
            {
                jellyFishView.PlayAttackRightHand();
            }
        }

        bool isAttackLefthand;
        IEnumerator LeftAttackSequence()
        {
            cooldownBehaviour.StartCooldown();
            isAttackLefthand = true;
            jellyFishView.PlayAttackMeeleLeftHand();
            yield return new WaitForSeconds(1f);
        }

        IEnumerator RightAttackSequence()
        {
            cooldownBehaviour.StartCooldown();
            isAttackLefthand = false;
            jellyFishView.PlayAttackMeeleRightHand();
            yield return new WaitForSeconds(1f);
        }
    }
}