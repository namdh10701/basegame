using _Base.Scripts.RPG.Effects;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class StandardProjectile : CannonProjectile
    {
        public DecreaseHealthEffect decreaseHpEffect;
        public PushEffect pushEffect;
        public override void ApplyStats()
        {
            base.ApplyStats();
            decreaseHpEffect.Amount = _stats.Damage.Value;
            decreaseHpEffect.ArmorPenetrate = _stats.ArmorPenetrate.Value;
            decreaseHpEffect.IsCrit = isCrit;
            pushEffect.force = 150;
            pushEffect.body = body;
        }

        public override void SetDamage(float dmg, bool isCrit)
        {
            base.SetDamage(dmg, isCrit);
            decreaseHpEffect.Amount = _stats.Damage.Value;
            decreaseHpEffect.ArmorPenetrate = _stats.ArmorPenetrate.Value;
            decreaseHpEffect.IsCrit = isCrit;
        }
    }
}