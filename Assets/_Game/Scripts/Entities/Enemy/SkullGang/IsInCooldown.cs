using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Skull Gang/Is Not In Cooldown")]
    public class IsNotInCooldown : Condition
    {
        public CooldownBehaviour cooldown;
        public override bool Check()
        {
            return !cooldown.IsInCooldown;
        }
    }
}