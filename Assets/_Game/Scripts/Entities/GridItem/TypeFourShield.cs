using _Game.Features.Gameplay;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class TypeFourShield : CarpetShield
    {

        public override void Buff(IBuffable gridItem)
        {
            if (gridItem is IShieldable shieldable)
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

        protected override int EffectId()
        {
            return 4;
        }
    }
}