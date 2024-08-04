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
            EntityManager.AliveEnemiesChanged += CheckWin;
        }

        void CheckWin()
        {
            if (!IsStarted)
                return;
            if (!EnemyManager.IsLevelDone)
                return;

            if (EntityManager.aliveEnemies.Count == 0)
            {
                BattleManager.Instance.Win();
            }
        }
    }
}