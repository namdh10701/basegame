using UnityEngine;

namespace _Game.Scripts.Entities
{

    public class PufferFishModel : EnemyModel
    {
        [Header("Puffer Fish")]
        [Space]
        public PufferFishMove PufferFishMove;
        public DamageArea DamageArea;

        public override void OnSlowed()
        {
            base.OnSlowed();
            PufferFishMove.SetSpeed(.5f);
        }

        public override void OnSlowEnded()
        {
            base.OnSlowEnded();
            PufferFishMove.SetSpeed(1);
        }
        public override void ApplyStats()
        {
            base.ApplyStats();
            EnemyStats stats = Stats as EnemyStats;
            DamageArea.SetRange(stats.AttackRange.Value);
            DamageArea.SetDamage(stats.AttackDamage.Value, 0);
        }
    }
}