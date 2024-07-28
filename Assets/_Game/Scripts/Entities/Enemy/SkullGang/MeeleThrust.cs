using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Skull Gang/MeleeThrust")]
    public class MeleeThrust : Leaf
    {
        public SkullGang SkullGang;
        public ShipReference ShipReference;
        public override NodeResult Execute()
        {
            SkullGang.MelleThust();
            return NodeResult.success;
        }
    }
}