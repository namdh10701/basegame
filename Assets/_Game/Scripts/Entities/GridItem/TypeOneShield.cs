using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class TypeOneShield : CarpetShield
    {

        public override void Buff(IBuffable buffable)
        {
            if (buffable is IShieldable shieldable)
            {
                if (shieldable is Ship)
                {
                    shieldable.Blocks.Add(Shield);
                }
            }
        }

        public override void RemoveBuff(IBuffable buffable)
        {
            if (buffable is IShieldable shieldable)
            {
                if (shieldable is Ship)
                {
                    shieldable.Blocks.Remove(Shield);
                }
            }
        }

        protected override int EffectId()
        {
            return 1;
        }
    }
}