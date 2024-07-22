using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Giant Octopos/Grab Attack")]
    public class GrabAttack : Leaf
    {
        public GiantOctopus GiantOctopus;
        public override NodeResult Execute()
        {
            GiantOctopus.LaserAttack();
            return NodeResult.success;
        }


    }
}