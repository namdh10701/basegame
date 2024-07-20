using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public enum OctopusState
    {
        None, Entry, State1, State2, Mad, Dead
    }
    public class GiantOctopus : MonoBehaviour
    {
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


        public PartModel[] Parts;
        public GiantOctopusView GiantOctopusView;
        public Action OnDied;
        private void Start()
        {
            GiantOctopusView.Initialize(this);
            State = OctopusState.State1;
        }
        private void Awake()
        {
            foreach (PartModel part in Parts)
            {
                part.EnemyStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            }
        }

        private void OnDestroy()
        {
            foreach (PartModel part in Parts)
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
            foreach (PartModel part in Parts)
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