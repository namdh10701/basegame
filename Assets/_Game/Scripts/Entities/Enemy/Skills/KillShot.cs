using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

public class KillShotEffect : OneShotEffect
{
    [field: SerializeField]
    public float Threshold { get; set; }
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

        if (alive.HealthPoint.Value > 0)
        {
            if ((alive.HealthPoint.Value / alive.HealthPoint.MaxValue) < Threshold)
            {
                alive.HealthPoint.StatValue.BaseValue -= 999999;
                GlobalEvent<float, bool, Vector3>.Send("DAMAGE_INFLICTED", 999999, true, transform.position);
            }
        }

    }
}
