using System.Collections;
using System.Collections.Generic;
using _Game.Scripts;
using MBT;
using UnityEngine;

namespace _Game.Scripts
{
    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/Custom Condition")]
    public class CustomCondition : MBT.Condition
    {
        [SerializeField] Condition condition;

        public override bool Check()
        {
            return condition.IsMet;
        }

    }
}
