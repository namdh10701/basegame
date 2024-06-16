using System;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    [Serializable]
    public class DecreaseHealthEffect : OneShotEffect
    {
        [field: SerializeField]
        public float Amount { get; set; }

        public DecreaseHealthEffect(float amount)
        {
            Amount = amount;
        }

        protected override void OnApply(Entity entity)
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
            float blockChance;

            if (entity.Stats is EnemyStats enemyStats)
            {

            }

            if (entity.Stats is CannonStats cannonStats)
            {

            }

            alive.HealthPoint.StatValue.BaseValue -= finalAmount;
        }

    }
}