using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Select Next position")]
    [AddComponentMenu("")]
    public class SelectNextPosition : Leaf
    {
        [SerializeField] Vector2Reference point;
        [SerializeField] PositionPool positionPool;

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override NodeResult Execute()
        {
            point.Value = positionPool.GetNextPosition();

            return NodeResult.success;

        }
        public override void OnExit()
        {
            base.OnExit();
        }
    }


}
