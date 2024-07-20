using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public enum PartState
    {
        None, Entry, Idle, Attacking, Transforming, Dead
    }
    public class PartModel : MonoBehaviour
    {

        PartState state;
        public PartState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    PartState lastState = state;
                    state = value;
                    OnStateEntered?.Invoke(state);
                    OnEnterState();
                }
            }
        }
        public Action<PartState> OnStateEntered;

        void OnEnterState()
        {

        }

        public void Active()
        {

        }

        public EnemyStats EnemyStats;
    }
}