using _Game.Scripts.Battle;
using System;
using System.Collections;

namespace _Game.Features.Gameplay
{
    public class SpawnPart : PartModel
    {
        public Action OnAttackDone;
        public Area spawnArea;
        public EnemyManager enemyManager;
        public int total_power;
        public string[] poolId;
        public override void Initialize(GiantOctopus giantOctopus)
        {
            base.Initialize(giantOctopus);
            enemyManager = FindAnyObjectByType<EnemyManager>();
            SpawnPartView spartView = partView as SpawnPartView;
            spartView.attackDone += OnAttackEnded;
        }
        public override void DoAttack()
        {
            enemyManager.SpawnEnemy(poolId, total_power, spawnArea.SamplePoint());
        }

        public void OnAttackEnded()
        {
            OnAttackDone?.Invoke();
        }
    }
}