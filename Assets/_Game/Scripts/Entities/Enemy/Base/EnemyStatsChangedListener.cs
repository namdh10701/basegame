using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;

public class EnemyStatsChangedListener : MonoBehaviour
{
    public EnemyModel EnemyModel;
    public EnemyController EnemyController;
    public SpineAnimationEnemyHandler SpineAnimationEnemyHandler;
    float lastHp;
    private void Start()
    {
        lastHp = ((EnemyStats)EnemyModel.Stats).HealthPoint.Value;
        ((EnemyStats)EnemyModel.Stats).HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
    }
    private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
    {
        if (obj.Value < lastHp)
            SpineAnimationEnemyHandler.Blink();

        if (obj.StatValue.Value <= obj.MinValue)
        {
            EnemyController.Die();
        }
    }
}
