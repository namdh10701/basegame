using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts;
using _Game.Scripts.GD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public enum CrabState
    {
        None, Entry, Idle, Move, Defense, Gore, Stun, Attack, Dead
    }
    public class Crab : EnemyModel, IEffectTaker, IGDConfigStatsTarget
    {
        public CrabState crabState;

        public CrabState CrabState
        {
            get { return crabState; }
            set
            {
                if (crabState == CrabState.Dead)
                {
                    return;
                }
                CrabState lastState = crabState;
                if (lastState != value)
                {
                    crabState = value;
                    OnCrabStateEntered.Invoke(crabState);
                    OnStateEnter(crabState);
                }
            }
        }
        public Action<CrabState> OnCrabStateEntered;
        public CrabDefense crabDefense;
        public CrabView crabView;
        public override void ApplyStats()
        {

        }
        private void Start()
        {
            effectTakerCollider.Taker = this;
            effectHandler.EffectTaker = this;
            crabDefense.OnBroken += OnDefenseBroke;
            crabDefense.OnActive += OnDefenseActive;
            crabDefense.OnDeactive += OnDefenseDeactive;
        }

        void OnDefenseActive()
        {
            effectTakerCollider.gameObject.SetActive(false);
        }

        void OnDefenseDeactive()
        {

            effectTakerCollider.gameObject.SetActive(true);
        }

        void OnDefenseBroke()
        {
            CrabState = CrabState.Stun;
            mbtExecutor.enabled = false;
            Invoke("EndStun", 4);
        }

        void EndStun()
        {
            CrabState = CrabState.Idle;
            mbtExecutor.enabled = true;
        }

        void OnStateEnter(CrabState state)
        {
            switch (state)
            {
                case CrabState.Idle:
                    body.velocity = Vector2.zero;
                    break;
                case CrabState.Defense:
                    break;
                case CrabState.Stun:
                    body.velocity = Vector2.zero;
                    break;
            }
        }

        public override void DoAttack()
        {
            Cell cell = gridPicker.PickRandomCell();
            EnemyAttackData enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = new List<Cell>() { cell };
            enemyAttackData.CenterCell = cell;
            DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
            decreaseHp.Amount = _stats.AttackDamage.Value;
            decreaseHp.ChanceAffectCell = 1;
            decreaseHp.transform.position = cell.transform.position;
            enemyAttackData.Effects = new List<Effect> { decreaseHp };
            atkHandler.ProcessAttack(enemyAttackData);

            ParticleSystem particle = Instantiate(onHitParticle,
            cell.Grid.ship.ShipArea.ClosetPointTo(transform.position), Quaternion.identity);
            particle.gameObject.SetActive(true);
        }
        public ParticleSystem onHitParticle;

        public override IEnumerator AttackSequence()
        {
            yield break;
        }
    }
}