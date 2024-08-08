using _Base.Scripts.RPG.Stats;
using _Game.Features.Gameplay;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

public class TypeThreeShield : CarpetShield
{
    public override void OnShieldCooldownStart()
    {
        base.OnShieldCooldownStart();
        if (attached is Ship ship)
        {
            ship.ShipBuffStats.DmgIncrease.AddModifier(new StatModifier(.1f, StatModType.Flat, this));
        }
    }

    public override void OnShieldCooldownStop()
    {
        base.OnShieldCooldownStop();
        if (attached is Ship ship)
        {
            ship.ShipBuffStats.DmgIncrease.RemoveAllModifiersFromSource(this);
        }
    }
    public override void Buff(IBuffable buffable)
    {
        if (buffable is IShieldable shieldable)
        {
            if (shieldable is Ship)
            {
                shieldable.Shields.Add(Shield);
            }
        }
    }

    public override void RemoveBuff(IBuffable buffable)
    {
        if (buffable is IShieldable shieldable)
        {
            if (shieldable is Ship)
            {
                shieldable.Shields.Remove(Shield);
            }
        }
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override int EffectId()
    {
        return 3;
    }
}
