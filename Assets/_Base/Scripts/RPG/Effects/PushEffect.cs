using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System;
using UnityEngine;

[Serializable]
public class PushEffect : OneShotEffect
{
    public LayerMask affectLayer;
    public float force;
    public bool isIgnorePoise;
    public Vector2 pushDirection;
    public Rigidbody2D body;
    protected override void OnApply(Entity entity)
    {
        if (entity is Enemy enemy)
        {
            float poise = ((EnemyStats)enemy.Stats).Poise.Value;

            if (!isIgnorePoise)
            {
                force = force * (1 - poise);
            }
            if (body != null)
            {
                entity.body.AddForceAtPosition(force * body.velocity.normalized, transform.position);
            }
            else
            {
                entity.body.AddForceAtPosition(force * (entity.body.transform.position - transform.position).normalized, transform.position);
            }
        }
    }
}
