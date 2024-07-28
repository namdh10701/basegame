using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Skull Gang/Range atack throw spear")]
    public class RangeAttackThrowSpear : Leaf
    {
        public SkullGang SkullGang;
        public override NodeResult Execute()
        {
            SkullGang.ThrowSpear();
            return NodeResult.success;
        }
    }
}