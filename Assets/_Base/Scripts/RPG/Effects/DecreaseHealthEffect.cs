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
    public class DecreaseHealthEffect : OneShotEffect
    {
        [field: SerializeField]
        public float Amount { get; set; }

        [field: SerializeField]
        public float AmmoPenetrate { get; set; }
        public bool IsCrit;

        protected override void OnApply(IEffectTaker entity)
        {
            if (entity.Stats == null)
            {
                return;
            }
            if (entity.Stats is not IAliveStats alive)
            {
                return;
            }
            float finalAmount = Amount;
            float blockChance = 0;

            if (entity.Stats is EnemyStats enemyStats)
            {
                blockChance = enemyStats.BlockChance.Value;
            }

            if (entity.Stats is ShipStats shipStats)
            {
                blockChance = shipStats.BlockChance.Value;
            }
            blockChance -= AmmoPenetrate;
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