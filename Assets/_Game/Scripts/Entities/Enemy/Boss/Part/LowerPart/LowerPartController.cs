using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Stats;
using _Game.Features.Gameplay;
using _Game.Scripts.Utils;
using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class LowerPartController : PartController
    {
        public GiantOctopus giantOctopus;
        public LowerPartModel lowerPartLeft;
        public LowerPartModel lowerPartRight;
        public float grabDuration;
        public float dmgInterval;
        public CameraShake cameraShake;
        public GridPicker gridPicker;
        public GridAttackHandler gridAttack;
        Coroutine atkCoroutine;

        private void Start()
        {
            gridPicker = FindAnyObjectByType<GridPicker>();
            gridAttack = FindAnyObjectByType<GridAttackHandler>();
            lowerPartLeft.OnAttack += CheckStartAttack;
            lowerPartRight.OnAttack += CheckStartAttack;
            lowerPartLeft.stats.HealthPoint.OnValueChanged += CheckHpStopAttack;
            lowerPartLeft.stats.HealthPoint.OnValueChanged += CheckHpStopAttack;
        }


        private void CheckHpStopAttack(RangedStat stat)
        {
            float currentHp = lowerPartLeft.stats.HealthPoint.Value + lowerPartRight.stats.HealthPoint.Value;
            float maxHp = lowerPartLeft.stats.HealthPoint.MaxValue + lowerPartRight.stats.HealthPoint.MaxValue;
            Debug.LogError(currentHp + " " + maxHp);
            if (currentHp < maxHp / 2)
            {
                Debug.Log("STOP ATTACK LOWER");
                StopAttack();
            }
        }


        void CheckStartAttack()
        {
            if (lowerPartLeft.isGrabbing && lowerPartRight.isGrabbing)
            {
                atkCoroutine = StartCoroutine(AttackCoroutine());
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
            atkCoroutine = null;
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
            if (atkCoroutine != null)
            {
                StopCoroutine(atkCoroutine);
                atkCoroutine = null;
            }
            lowerPartLeft.State = PartState.Hidding;
            lowerPartRight.State = PartState.Hidding;
            base.StopAttack();
        }

        void DoDmg()
        {
            Cell cell = gridPicker.PickRandomCell();
            EnemyAttackData enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = new List<Cell>() { cell };
            enemyAttackData.CenterCell = cell;

            Debug.LogError(cell.ToString());

            DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
            decreaseHp.Amount = giantOctopus.enemyStats.P2.Value;
            decreaseHp.ChanceAffectCell = 1;
            decreaseHp.transform.position = cell.transform.position;
            enemyAttackData.Effects = new List<Effect> { decreaseHp };

            gridAttack.ProcessAttack(enemyAttackData);
        }

        public override IEnumerator TransformCoroutine()
        {
            Coroutine a = StartCoroutine(lowerPartLeft.TransformCoroutine());
            Coroutine b = StartCoroutine(lowerPartRight.TransformCoroutine());
            yield return a;
            yield return b;
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