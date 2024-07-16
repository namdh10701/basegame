using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    public class BurningEffect : UnstackablePeriodicEffect
    {
        [field: SerializeField]
        public float Amount { get; set; }

        public override string Id => nameof(BurningEffect);

        public override bool CanEffect(IEffectTaker entity)
        {
            return entity is IBurnable;
        }

        protected override void OnTick(IEffectTaker entity)
        {
            if (entity is not IStatsBearer statsBearer)
            {
                return;
            }
            if (statsBearer.Stats is not IAliveStats alive)
            {
                return;
            }

            float finalAmount = Amount;
            float blockChance = 0;

            if (statsBearer.Stats is EnemyStats enemyStats)
            {
                blockChance = enemyStats.BlockChance.Value;
            }

            if (statsBearer.Stats is ShipStats shipStats)
            {
                blockChance = shipStats.BlockChance.Value;
            }

            blockChance = Mathf.Clamp01(blockChance);
            finalAmount = finalAmount * (1 - blockChance);
            Debug.LogError(finalAmount);
            if (finalAmount > 0)
            {
                if (alive.HealthPoint.StatValue.BaseValue > alive.HealthPoint.MinStatValue.Value)
                {
                    alive.HealthPoint.StatValue.BaseValue -= finalAmount;
                    GlobalEvent<float, bool, Vector3>.Send("DAMAGE_INFLICTED", finalAmount, false, entity.Transform.position);
                }
            }
            alive.HealthPoint.StatValue.BaseValue -= Amount;
        }

        public override void Apply(IEffectTaker entity)
        {
            base.Apply(entity);
            IBurnable slowable = entity as IBurnable;
            Affected = (IEffectTaker)slowable;
            slowable.OnBurn();
        }

        public override void OnEnd(IEffectTaker entity)
        {
            base.OnEnd(entity);
            IBurnable slowable = entity as IBurnable;
            slowable.OnBurnEnd();
        }
    }
}