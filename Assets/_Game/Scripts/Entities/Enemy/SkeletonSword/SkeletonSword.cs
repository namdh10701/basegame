using _Game.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public enum EnemyState
    {
        None, Entry, Idle, Moving, Attacking, Hiding, Dead
    }

    public class SkeletonSword : EnemyModel
    {
        EnemyState state = EnemyState.None;
        public EnemyState State
        {
            get { return state; }
            set
            {
                EnemyState lastState = state;
                state = value;
                if (state != lastState)
                {
                    OnStateChanged?.Invoke(state);
                }
            }
        }

        public Action<EnemyState> OnStateChanged;
    }
}