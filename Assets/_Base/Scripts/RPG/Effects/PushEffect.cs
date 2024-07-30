using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System;
using UnityEngine;

[Serializable]
public class PushEffect : OneShotEffect
{
    public float force;
    public bool isIgnorePoise;
    public Rigidbody2D body;
    protected override void OnApply(IEffectTaker entity)
    {
        Debug.Log(entity + " PUSH");
        if (entity is IPhysicsEffectTaker enemy)
        {
            float poise = enemy.Poise;

            if (!isIgnorePoise)
            {
                force = force * (1 - poise);
            }
            if (body != null)
            {
                enemy.Body.AddForceAtPosition(force * body.velocity.normalized, transform.position);
            }
            else
            {
                enemy.Body.AddForceAtPosition(force * (enemy.Body.transform.position - transform.position).normalized, transform.position);
            }
        }
    }
}
