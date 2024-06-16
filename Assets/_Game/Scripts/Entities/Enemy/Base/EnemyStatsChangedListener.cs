using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsChangedListener : MonoBehaviour
{
    public Enemy Enemy;
    public SpineAnimationEnemyHandler SpineAnimationEnemyHandler;
    private void Awake()
    {
        ((EnemyStats)Enemy.Stats).HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
    }
    private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
    {
        SpineAnimationEnemyHandler.Blink();
        if (obj.StatValue.Value <= obj.MinValue)
        {

            Enemy.Die();
        }
    }
}
