using _Game.Features.Gameplay;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeTwoShield : CarpetShield
{
    public override void Buff(IBuffable gridItem)
    {
        if (gridItem is IShieldable shieldable)
        {
            if (!shieldable.Shields.Contains(Shield))
            {
                Debug.Log(shieldable + " SHIELDed " + Shield.GetHashCode());
                shieldable.Shields.Add(Shield);
            }
        }
    }

    public override void RemoveBuff(IBuffable buffable)
    {
        if (buffable is IShieldable shieldable)
        {
            if (shieldable.Shields.Contains(Shield))
            {
                Debug.Log(shieldable + " unSHIELDed");
                shieldable.Shields.Remove(Shield);
            }
        }
    }
    protected override int EffectId()
    {
        return 2;
    }
}
