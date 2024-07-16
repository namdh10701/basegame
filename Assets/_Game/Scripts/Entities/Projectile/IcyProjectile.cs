using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public class IcyProjectile : BombProjectile
    {
        public SlowEffect slowEffect;
        public override void ApplyStats()
        {
            base.ApplyStats();
            ProjectileStats stats = Stats as ProjectileStats;
            slowEffect.SetStrength(stats.SpeedModifier.Value);
            slowEffect.SetDuration(stats.Duration.Value);
            slowEffect.Prob = stats.TriggerProb.Value;
        }
    }

}