using System.Collections;
using System.Collections.Generic;
using _Game.Scripts;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/ CustomCondition")]
    public class CustomCondition : Condition
    {
        [SerializeReference] MyCustomCondition condition;

        public override bool Check()
        {
            return condition.IsMet();
        }

    }
}
