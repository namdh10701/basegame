using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using UnityEngine;

public class StunEffect : UnstackableEffect, IProbabilityEffect
{
    public override string Id => "StunEffect";

    [field: SerializeField]
    public float Prob { get; set; }

    public IStunable affected;
    public override bool CanEffect(IEffectTaker entity)
    {
        return entity is IStunable;
    }
    public override void Apply(IEffectTaker entity)
    {
        base.Apply(entity);
        IStunable stunable = entity as IStunable;
        Affected = stunable as IEffectTaker;
        if (Affected != null)
            stunable.OnStun();
    }

    public override void OnEnd(IEffectTaker entity)
    {
        base.OnEnd(entity);
        if (Affected != null)
        {
            IStunable stunable = entity as IStunable;
            stunable.OnAfterStun();
        }
    }

    public override void RefreshEffect(UnstackableEffect effect)
    {
        base.RefreshEffect(effect);
    }
}
