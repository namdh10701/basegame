using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using UnityEngine;

public class StunEffect : UnstackableEffect
{
    public override string Id => "StunEffect";
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
        stunable.OnStun();
    }

    public override void OnEnd(IEffectTaker entity)
    {
        base.OnEnd(entity);
        IStunable stunable = entity as IStunable;
        stunable.OnAfterStun();
    }

    public override void RefreshEffect(UnstackableEffect effect)
    {
        base.RefreshEffect(effect);
    }
}
