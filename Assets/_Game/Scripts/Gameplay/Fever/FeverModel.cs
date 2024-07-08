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
        public int[] thresholdSteps = new int[5] { 0, 200, 400, 600, 800 };
        public RangedStat FeverPoint;
        public FeverState CurrentState;
        public Action<FeverState> OnStateChanged;

        public void OnUseFever()
        {
            CurrentState = FeverState.Unleashing;
            OnStateChanged.Invoke(CurrentState);
        }

        public void SetFeverPointStats(RangedStat feverPoint)
        {
            this.FeverPoint = feverPoint;
            UpdateState();
        }

        public void UpdateState()
        {
            for (int i = 0; i < thresholdSteps.Length; i++)
            {
                if (FeverPoint.StatValue.BaseValue < thresholdSteps[i])
                {
                    ChangeState((FeverState)i);
                    Debug.LogError(i);
                    return;
                }
            }
            ChangeState(FeverState.State4);
        }

        public void ChangeState(FeverState nextState)
        {
            CurrentState = nextState;
            OnStateChanged?.Invoke(CurrentState);
        }
    }
}