using _Base.Scripts.RPG.Stats;
using _Game.Features.Gameplay;
using _Game.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class LowerPartController : PartController
    {
        public LowerPartModel lowerPartLeft;
        public LowerPartModel lowerPartRight;
        public float grabDuration;
        public float dmgInterval;
        public CameraShake cameraShake;


        private void Start()
        {
            lowerPartLeft.OnAttack += CheckStartAttack;
            lowerPartRight.OnAttack += CheckStartAttack;
            lowerPartLeft.stats.HealthPoint.OnValueChanged += CheckHpStopAttack;
            lowerPartLeft.stats.HealthPoint.OnValueChanged += CheckHpStopAttack;
        }


        private void CheckHpStopAttack(RangedStat stat)
        {
            float currentHp = lowerPartLeft.stats.HealthPoint.Value + lowerPartRight.stats.HealthPoint.Value;
            float maxHp = lowerPartLeft.stats.HealthPoint.MaxValue + lowerPartRight.stats.HealthPoint.MaxValue;
            if (currentHp < maxHp / 2)
            {
                StopAttack();
            }
        }


        void CheckStartAttack()
        {
            if (lowerPartLeft.isGrabbing && lowerPartRight.isGrabbing)
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        IEnumerator AttackCoroutine()
        {
            float elapsedTime = 0;
            float intervalElapsedTime = 0;
            cameraShake.Shake(grabDuration, new Vector3(.07f, .07f, .07f));
            while (elapsedTime < grabDuration)
            {

                elapsedTime += Time.deltaTime;
                intervalElapsedTime += Time.deltaTime;
                if (intervalElapsedTime > dmgInterval)
                {
                    intervalElapsedTime = 0;
                    DoDmg();
                }
                yield return null;
            }
            StopAttack();
        }
        public override void StartAttack()
        {
            lowerPartLeft.State = PartState.Entry;
            lowerPartRight.State = PartState.Entry;
            lowerPartLeft.stats.HealthPoint.StatValue.BaseValue = lowerPartLeft.stats.HealthPoint.MaxValue;
            lowerPartRight.stats.HealthPoint.StatValue.BaseValue = lowerPartRight.stats.HealthPoint.MaxValue;
            base.StartAttack();
        }
        public override void StopAttack()
        {
            lowerPartLeft.State = PartState.Hidding;
            lowerPartRight.State = PartState.Hidding;
            base.StopAttack();
        }

        void DoDmg()
        {
            Debug.Log("DO DMG");
        }

        public override IEnumerator TransformCoroutine()
        {
            Coroutine a = StartCoroutine(lowerPartLeft.TransformCoroutine());
            Coroutine b = StartCoroutine(lowerPartRight.TransformCoroutine());
            yield return a;
            yield return b;
        }
    }
}