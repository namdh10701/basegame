using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

public class KillShot : OneShotEffect
{
    [field: SerializeField]
    public float Amount { get; set; }

    [field: SerializeField]
    public float AmmoPenetrate { get; set; }
    public bool IsCrit = true;
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
        Amount = 999999;
        if (Amount > 0)
        {
            if (alive.HealthPoint.StatValue.BaseValue > alive.HealthPoint.MinStatValue.Value)
            {
                alive.HealthPoint.StatValue.BaseValue -= Amount;
                GlobalEvent<float, bool, Vector3>.Send("DAMAGE_INFLICTED", Amount, IsCrit, transform.position);
            }
        }
    }
}
