using _Game.Features.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Gameplay
{
    public class RankingLevelWinCondition : WinCondition
    {
        public GiantOctopus giantOctopus;
        public Timer timer;
        public override void StartChecking()
        {
            base.StartChecking();

            timer.timeCap = 300;
            timer.StartTimer();
            TimedEvent timedEvent = new TimedEvent(300, () => OnWin());
            timer.RegisterEvent(timedEvent);

            StartCoroutine(CheckLevelState());
        }
        public override void StopCheck()
        {
            base.StopCheck();
            timer.Stop();
        }
        public IEnumerator CheckLevelState()
        {
            while (IsChecking)
            {
                yield return new WaitForSeconds(1);
                if (giantOctopus.State == OctopusState.Dead)
                {
                    OnWin();
                }

            }
        }

        void OnWin()
        {
            if (IsChecking)
            {
                timer.Stop();
                BattleManager.Instance.Win();
            }
        }
    }

}