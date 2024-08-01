using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Stats;
using DG.Tweening;
using System;
using UnityEngine;

namespace _Game.Features.Battle
{
    public enum FeverState
    {
        State0 = 0, State1 = 1, State2 = 2, State3 = 3, State4 = 4, Unleashing
    }
    public class FeverModel : MonoBehaviour
    {
        private FeverState currentState;
        private FeverState lastState;

        public int[] thresholdSteps = new int[5] { 0, 200, 400, 600, 800 };
        public RangedStat FeverStat;
        public FeverState CurrentState
        {
            get => currentState;
            set
            {
                lastState = currentState;
                currentState = value;
                if (lastState != currentState)
                {
                    OnStateChanged?.Invoke(currentState);
                }
            }
        }
        public Action<FeverState> OnStateChanged;
        public Action<RangedStat> OnStatChanged;
        public void OnUseFever()
        {
            CurrentState = FeverState.Unleashing;
            FeverStat.StatValue.BaseValue = 0;
        }

        public void SetFeverPointStats(RangedStat feverPoint)
        {
            this.FeverStat = feverPoint;
            this.FeverStat.OnValueChanged += FeverPoint_OnValueChanged;
            UpdateState();
        }

        private void FeverPoint_OnValueChanged(RangedStat obj)
        {
            UpdateState();
            OnStatChanged?.Invoke(obj);
        }

        public void UpdateState()
        {
            if (CurrentState == FeverState.Unleashing)
            {
                return;
            }

            for (int i = 0; i < thresholdSteps.Length; i++)
            {
                if (FeverStat.StatValue.BaseValue < thresholdSteps[i])
                {
                    CurrentState = (FeverState)i;
                    return;
                }
            }
            CurrentState = FeverState.State4;
        }

        public void ChangeState(FeverState nextState)
        {
            CurrentState = nextState;
        }
    }
}