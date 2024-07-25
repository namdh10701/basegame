using _Base.Scripts.RPG.Effects;
using _Game.Features.Gameplay;
using _Game.Scripts;
using _Game.Scripts.Attributes;
using _Game.Scripts.Utils;
using MBT;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class UpperPartModel : PartModel
    {
        UpperPartView view;
        public Transform startPos;
        public MBTExecutor mbt;
        public Blackboard blackboard;
        public bool isAttacking;
        public CameraShake cameraShake;
        public AttackPatternProfile attackPatternProfile;
        public override void Initialize(GiantOctopus octopus)
        {
            base.Initialize(octopus);
            blackboard.GetVariable<StatVariable>("MoveSpeed").Value = stats.MoveSpeed;
            Ship ship = FindAnyObjectByType<Ship>();
            blackboard.GetVariable<ShipVariable>("Ship").Value = ship;
            view = partView as UpperPartView;
            stats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
        }

        private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat stat)
        {
            if (stat.Value < stat.MaxValue / 2)
            {
                IsAttacking = false;
            }
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            if (State == PartState.Stunning)
            {
                mbt.enabled = false;
            }
        }
        public override void AfterStun()
        {
            base.AfterStun();
            mbt.enabled = true;
        }

        public bool IsAttacking
        {
            get => isAttacking; set
            {
                isAttacking = value;
                mbt.enabled = value;
                if (!value)
                {
                    State = PartState.Hidding;
                }
            }
        }
        public override void Deactive()
        {
            base.Deactive();
            transform.position = startPos.position;
        }
        public Action OnAttack;
        public override void DoAttack()
        {
            base.DoAttack();
            Cell cell = gridPicker.PickRandomCell();
            EnemyAttackData enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = gridPicker.PickCells(transform, attackPatternProfile, out Cell centerCell);
            enemyAttackData.CenterCell = centerCell;

            DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
            decreaseHp.Amount = stats.AttackDamage.Value;
            enemyAttackData.Effects = new List<Effect> { decreaseHp };

            gridAtk.ProcessAttack(enemyAttackData);

            cameraShake.Shake(.1f, new Vector3(.1f, .1f, .1f));
        }

        public void DoneAttack()
        {
            OnAttack?.Invoke();
        }
    }
}