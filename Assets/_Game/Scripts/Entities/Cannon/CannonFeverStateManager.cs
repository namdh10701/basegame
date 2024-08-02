using _Game.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public enum CannonFeverState
    {
        None, Fever, FullFever
    }
    public class CannonFeverStateManager : MonoBehaviour
    {
        public ParticleSystem feverEnterFx;
        private CannonFeverState state;
        private CannonFeverState lastState;

        public CannonFeverState FeverState
        {
            get => state;
            set
            {
                if (state != value)
                {
                    lastState = state;
                    state = value;
                    OnStateEntered?.Invoke(state);
                    OnStateEnter(state);
                }
            }
        }

        public Action<CannonFeverState> OnStateEntered;

        protected virtual void OnStateEnter(CannonFeverState state)
        {
            // Default or shared implementation for handling state changes
            switch (state)
            {
                case CannonFeverState.Fever:
                case CannonFeverState.FullFever:
                    feverEnterFx.Play();
                    break;
            }
            switch (state)
            {
                case CannonFeverState.Fever:

                    break;
                case CannonFeverState.FullFever:

                    break;
            }
        }
    }
}