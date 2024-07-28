using _Game.Features.Gameplay;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Skull Gang/Range atack puffer fish")]
public class RangeAttackPufferFish : Leaf
{
    public SkullGang SkullGang;
    public override NodeResult Execute()
    {
        SkullGang.ThrowFish();
        return NodeResult.success;
    }
}
