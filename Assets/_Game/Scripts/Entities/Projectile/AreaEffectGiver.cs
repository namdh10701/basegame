using _Base.Scripts.RPG.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaEffectGiver : MonoBehaviour, IEffectGiver
{
    public CircleCollider2D circleCollider;

    public Transform Transform => transform;
    public List<Effect> outGoingEffects;
    public List<Effect> OutGoingEffects { get => outGoingEffects; set => outGoingEffects = value; }

    public virtual void SetRange(float range)
    {
        if (range <= 0)
        {
            range = 1;
        }
        circleCollider.radius = range;
    }
    public virtual void SetDamage(float damage, float armorPenetrate)
    {
        if (outGoingEffects == null)
            return;
        foreach (Effect effect in outGoingEffects)
        {
            if (effect is IDamageEffect damageEffect)
            {
                damageEffect.Amount = damage;
                damageEffect.ArmorPenetrate = armorPenetrate;
            }
        }
    }
}
