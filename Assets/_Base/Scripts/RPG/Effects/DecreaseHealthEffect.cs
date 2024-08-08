using System;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Base.Scripts.Shared;
using _Game.Features.Gameplay;
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

            blockChance -= ArmorPenetrate;

            blockChance = Mathf.Clamp01(blockChance);
            finalAmount = finalAmount * (1 - blockChance);

            if (statsBearer.Stats is CellStats cellStats || statsBearer.Stats is CarpetComponentStats componentStats)
            {
                finalAmount = 9999;
                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand > ChanceAffectCell)
                    return;
            }

            if (finalAmount > 0)
            {
               

                if (entity is IShieldable shieldable)
                {
                    bool isBlocked = false;
                    foreach (RangedStat block in shieldable.Blocks)
                    {
                        if (block.StatValue.BaseValue > 0)
                        {
                            block.StatValue.BaseValue -= 1;
                            isBlocked = true;
                            break;
                        }
                    }
                    if (isBlocked)
                    {
                        GlobalEvent<IEffectGiver, IEffectTaker, Vector3>.Send("DAMAGE_BLOCKED", Giver, entity, transform.position);
                        return;
                    }
                    foreach (RangedStat shield in shieldable.Shields)
                    {
                        if (shield.StatValue.BaseValue > 0)
                        {
                            float shieldAbsorbed = Mathf.Min(shield.StatValue.BaseValue, finalAmount);
                            shield.StatValue.BaseValue -= shieldAbsorbed;
                            finalAmount -= shieldAbsorbed;
                            if (finalAmount <= 0)
                                break;
                        }
                    }
                }
                if (alive.HealthPoint.StatValue.BaseValue > alive.HealthPoint.MinStatValue.Value)
                {
                    alive.HealthPoint.StatValue.BaseValue -= finalAmount;
                    GlobalEvent<float, bool, IEffectGiver, IEffectTaker, Vector3>.Send("DAMAGE_INFLICTED", finalAmount, IsCrit, Giver, entity, transform.position);
                }
            }
        }

    }
}