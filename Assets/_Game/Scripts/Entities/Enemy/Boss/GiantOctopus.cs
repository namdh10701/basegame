using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class GiantOctopus : MonoBehaviour
    {
        public enum OctopusState
        {
            None, State1, State2, State3, Dead
        }
        OctopusState state;
        public OctopusState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    OctopusState lastState = state;
                    state = value;
                    OnStateEntered?.Invoke(state);
                    OnEnterState();
                }
            }
        }
        public Action<OctopusState> OnStateEntered;


        public Part[] Parts;
        public Action OnDied;
        private void Awake()
        {
            foreach (Part part in Parts)
            {
                part.EnemyStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            }
        }

        private void OnDestroy()
        {
            foreach (Part part in Parts)
            {
                part.EnemyStats.HealthPoint.OnValueChanged -= HealthPoint_OnValueChanged;
            }
        }

        private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
        {
            if (obj.Value <= 0)
            {
                CheckDie();
            }
        }

        void OnEnterState()
        {

        }

        void CheckDie()
        {
            foreach (Part part in Parts)
            {
                if (part.EnemyStats.HealthPoint.Value > 0)
                {
                    return;
                }
            }
            OnDied?.Invoke();
        }
    }
}