using _Game.Features.Gameplay;
using _Game.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class BehindPartController : PartController
    {
        public BehindPartModel lowerPartLeft;
        public BehindPartModel lowerPartRight;
        public float grabDuration;
        public float dmgInterval;

        private void Start()
        {
            lowerPartLeft.OnAttack += CheckStartAttack;
            lowerPartRight.OnAttack += CheckStartAttack;
        }

        void CheckStartAttack()
        {
            StopAttack();
        }

        public override void StartAttack()
        {

            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand < .5f)
            {
                lowerPartLeft.State = PartState.Attacking;
            }
            else
            {
                lowerPartRight.State = PartState.Attacking;
            }
            base.StartAttack();
        }
        public override void StopAttack()
        {
            lowerPartLeft.State = PartState.Idle;
            lowerPartRight.State = PartState.Idle;
            base.StopAttack();
        }


        public override IEnumerator TransformCoroutine()
        {
            Coroutine a = StartCoroutine(lowerPartLeft.TransformCoroutine());
            Coroutine b = StartCoroutine(lowerPartRight.TransformCoroutine());
            yield return a;
            yield return b;
        }

        internal void Active()
        {
            lowerPartLeft.Active();
            lowerPartRight.Active();
        }

        internal IEnumerator DeadCoroutine()
        {
            Coroutine a = StartCoroutine(lowerPartLeft.DeadCoroutine());
            Coroutine b = StartCoroutine(lowerPartRight.DeadCoroutine());
            yield return a;
            yield return b;
        }
    }
}