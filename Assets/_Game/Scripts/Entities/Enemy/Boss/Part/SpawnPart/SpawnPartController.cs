using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class SpawnPartController : PartController
    {
        public SpawnPart left;
        public SpawnPart right;
        private void Awake()
        {
            left.OnAttackDone += CheckStopAttackLeft;
            right.OnAttackDone += CheckStopAttackRight;
        }

        void CheckStopAttackLeft()
        {
            StopAttack();
        }

        void CheckStopAttackRight()
        {
            StopAttack();
        }
        public override void StartAttack()
        {
            float rand = Random.Range(0, 1f);
            if (rand < .5f)
            {
                left.State = PartState.Attacking;
            }
            else
            {
                right.State = PartState.Attacking;
            }
            base.StartAttack();
        }

        public override void StopAttack()
        {
            left.State = PartState.Hidding;
            right.State = PartState.Hidding;
            base.StopAttack();
        }
        public override IEnumerator TransformCoroutine()
        {
            yield break;
        }
    }
}