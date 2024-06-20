using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : OneShotEffect
{
    public float StunDuration = 2;

    protected override void OnApply(Entity entity)
    {
        if (entity.TryGetComponent(out IStunable stunable))
        {
            stunable.OnStun(StunDuration);
        }
    }
}
