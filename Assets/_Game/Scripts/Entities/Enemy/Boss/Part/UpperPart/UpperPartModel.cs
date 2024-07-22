using _Game.Features.Gameplay;
using _Game.Scripts;
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
        bool isAttacking;
        public CameraShake cameraShake;

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
            cameraShake.Shake(.1f, new Vector3(.1f, .1f, .1f));
            DoneAttack();
        }

        public void DoneAttack()
        {
            OnAttack?.Invoke();
        }
    }
}