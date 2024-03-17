using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/IsAbleToAttack")]
    public class IsAbleToAttack : Condition
    {
        public Enemy enemy;

        public override bool Check()
        {
            return enemy.IsAbleToAttack;
        }

    }
}
