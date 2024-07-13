using _Base.Scripts.RPG.Effects;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcyProjectile : BombProjectile
{
    public SlowEffect slowEffect;
    public override void ApplyStats()
    {
        base.ApplyStats();
    }
}
