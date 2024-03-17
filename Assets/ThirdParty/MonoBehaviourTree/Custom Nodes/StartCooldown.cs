using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Start Cooldown")]
    public class StartCooldown : Leaf
    {
        public Enemy enemy;

        public override NodeResult Execute()
        {
            enemy.Cooldown.StartCooldown();
            return NodeResult.success;
        }
    }
}
