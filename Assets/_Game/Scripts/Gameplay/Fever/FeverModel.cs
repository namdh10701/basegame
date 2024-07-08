using _Base.Scripts.RPG.Stats;
using _Game.Scripts.Gameplay.Ship;
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
        public int[] thresholdSteps = new int[4] { 200, 400, 600, 800 };
        public RangedStat FeverPoint;
        public FeverState CurrentState;
        public Action<FeverState> OnStateChanged;

        public void AddFeverPoint(float amount)
        {
            FeverPoint.StatValue.BaseValue += amount;
            FeverPoint.StatValue.BaseValue = Mathf.Clamp(FeverPoint.StatValue.BaseValue, 0, FeverPoint.MaxStatValue.BaseValue);
            UpdateState();
        }

        public void RemoveFeverPoint(float amount)
        {
            FeverPoint.StatValue.BaseValue -= amount;
            FeverPoint.StatValue.BaseValue = Mathf.Clamp(FeverPoint.StatValue.BaseValue, 0, FeverPoint.MaxStatValue.BaseValue);
            UpdateState();
        }

        void UpdateState()
        {
            for (int i = thresholdSteps.Length - 1; i >= 0; i--)
            {
                if (FeverPoint.StatValue.BaseValue >= thresholdSteps[i])
                {
                    if ((int)CurrentState < i)
                    {
                        ChangeState((FeverState)i);
                    }
                    return;
                }
            }
        }

        public void ChangeState(FeverState nextState)
        {
            CurrentState = nextState;
            OnStateChanged?.Invoke(CurrentState);
        }
    }
}