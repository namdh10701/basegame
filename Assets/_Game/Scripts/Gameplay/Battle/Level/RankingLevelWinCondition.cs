using _Game.Features.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Gameplay
{
    public class RankingLevelWinCondition : WinCondition
    {
        public GiantOctopus giantOctopus;

        public override void StartChecking()
        {
            base.StartChecking();
            StartCoroutine(CheckLevelState());
        }
        public IEnumerator CheckLevelState()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                Debug.LogError("Check ");
                if (giantOctopus.State == OctopusState.Dead)
                {
                    Debug.LogError("Check 2");
                    yield return new WaitForSeconds(5);
                    BattleManager.Instance.Win();
                    yield break;
                }
                else
                {
                    Debug.LogError("Check 3");
                }
            }
        }
    }

}