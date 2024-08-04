using _Game.Scripts.Battle;
using Map;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class NormalLevelWinCondition : WinCondition
    {
        public EnemyWaveManager EnemyManager;
        public EntityManager EntityManager;

        public override void StartChecking()
        {
            base.StartChecking();
            EntityManager.AliveEnemiesChanged += CheckWin;
        }

        void CheckWin()
        {
            Debug.Log("Check win");
            if (!IsChecking)
                return;

            Debug.Log("Check win 1");
            if (!EnemyManager.IsLevelDone)
                return;

            Debug.Log("Check win 2");
            if (EntityManager.aliveEnemies.Count == 0)
            {
                BattleManager.Instance.Win();
            }
        }
    }
}