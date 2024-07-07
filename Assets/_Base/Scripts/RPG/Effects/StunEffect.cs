using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

public class StunEffect : OneShotEffect
{
    public float StunDuration = 1;

    protected override void OnApply(IEffectTaker entity)
    {
        Debug.LogError("HErer");
        if (entity is IStunable stunable)
        {
            Debug.LogError("HErer a");
            stunable.OnStun(StunDuration);
        }
    }
}
