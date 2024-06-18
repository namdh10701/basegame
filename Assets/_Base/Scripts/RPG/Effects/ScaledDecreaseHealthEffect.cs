using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledDecreaseHealthEffect : OneShotEffect
{
    [field: SerializeField]
    public float EnemyAmount { get; set; }
    [field: SerializeField]
    public float PlayerAmount { get; set; }
    public ScaledDecreaseHealthEffect(float enemyAmount, float playerAmount)
    {
        EnemyAmount = enemyAmount;
        PlayerAmount = playerAmount;
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

        float finalAmount;
        float blockChance;

        if (entity is Ship || entity is Crew || entity is Cannon || entity is Cell)
        {
            finalAmount = PlayerAmount;
        }
        else
        {
            finalAmount = EnemyAmount;
        }
        alive.HealthPoint.StatValue.BaseValue -= finalAmount;
    }

}
