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
        private JellyFishModel jellyFishModel;
        private JellyFishView jellyFishAnimation;
        private CooldownBehaviour cooldownBehaviour;
        [SerializeField] private JellyFishAttack attack;

        public override void Initialize(EnemyModel enemyModel)
        {
            jellyFishAnimation.OnAttack += Attack;
            jellyFishAnimation.OnAttackMeele += AttackMelee;
        }

        public void Attack()
        {
            jellyFishModel.DoAttack();
        }

        public void AttackMelee()
        {
            jellyFishModel.DoMeeleAttack();
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