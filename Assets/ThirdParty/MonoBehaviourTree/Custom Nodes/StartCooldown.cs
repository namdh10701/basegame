using System.Collections;
using System.Collections.Generic;
using Demo.Scripts.Enemy;
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
