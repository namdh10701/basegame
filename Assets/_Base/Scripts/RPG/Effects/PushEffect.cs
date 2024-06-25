using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PushEffect : OneShotEffect
{
    public LayerMask affectLayer;
    public float force;
    public bool isIgnorePoise;
    public PushEffect(float force)
    {
        this.force = force;
    }
    protected override void OnApply(Entity entity)
    {
        if (entity is Enemy enemy)
        {
            Vector2 direction = (entity.transform.position - transform.position).normalized;
            float poise = ((EnemyStats)enemy.Stats).Poise.Value;

            if (isIgnorePoise)
            {
            }
            else
            {
                force = force * (1 - poise);
            }
            entity.body.AddForceAtPosition(force * direction, transform.position);
        }
    }
}
