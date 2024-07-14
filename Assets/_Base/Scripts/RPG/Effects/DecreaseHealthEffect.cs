using System;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Base.Scripts.Shared;
using _Game.Scripts;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    [Serializable]
    public class DecreaseHealthEffect : OneShotEffect, IDamageEffect
    {
        [field: SerializeField]
        public float Amount { get; set; }

        [field: SerializeField]
        public float ArmorPenetrate { get; set; }

        public bool IsCrit;

        public float ChanceAffectCell = 1;


        public override bool CanEffect(IEffectTaker entity)
        {
            return true;
        }

        protected override void OnApply(IEffectTaker entity)
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

            if (statsBearer.Stats is CellStats cellStats)
            {
                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand > ChanceAffectCell)
                    return;
            }

            blockChance -= ArmorPenetrate;

            blockChance = Mathf.Clamp01(blockChance);
            finalAmount = finalAmount * (1 - blockChance);
            if (finalAmount > 0)
            {
                if (alive.HealthPoint.StatValue.BaseValue > alive.HealthPoint.MinStatValue.Value)
                {
                    alive.HealthPoint.StatValue.BaseValue -= finalAmount;
                    GlobalEvent<float, bool, Vector3>.Send("DAMAGE_INFLICTED", finalAmount, IsCrit, transform.position);
                }
            }
        }

    }
}