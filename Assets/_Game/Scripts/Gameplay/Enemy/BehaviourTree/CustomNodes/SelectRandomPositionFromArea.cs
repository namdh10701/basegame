using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Select Random Position From Area")]
    [AddComponentMenu("")]
    public class SelectRandomPositionFromArea : Leaf
    {
        [SerializeField] AreaReference areaReference;
        [SerializeField] Vector2Reference point;
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override NodeResult Execute()
        {
            point.Value = areaReference.Value.SamplePoint();
            return NodeResult.success;

        }
        public override void OnExit()
        {
            base.OnExit();
        }
    }


}
