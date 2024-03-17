using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace MBTExample
{
    [AddComponentMenu("")]
    [MBTNode("Example/Set Random Vector2", 500)]
    public class SetRandomVector2 : Leaf
    {
        public Bounds bounds;
        public Bounds ShipBounds;
        public Vector2Reference blackboardVariable = new Vector2Reference(VarRefMode.DisableConstant);

        public override void OnEnter()
        {
            base.OnEnter();

            blackboardVariable.Value = bounds.center;
        }

        public override NodeResult Execute()
        {
            // Random values per component inside bounds\
            while (ShipBounds.Contains(blackboardVariable.Value))
            {
                blackboardVariable.Value = new Vector2(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y)
                );
            }
            return NodeResult.success;
        }
    }
}
