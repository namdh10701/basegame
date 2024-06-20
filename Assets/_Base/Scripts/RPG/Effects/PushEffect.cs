using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PushEffect : OneShotEffect
{
    public LayerMask affectLayer;
    public float force;
    public PushEffect(float force)
    {
        this.force = force;
    }
    protected override void OnApply(Entity entity)
    {
        Vector2 direction = (entity.transform.position - transform.position).normalized;
        entity.body.AddForceAtPosition(force * direction, transform.position);
    }
}
