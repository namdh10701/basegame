using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Wanderer/Select Wander Area")]
public class SelectWanderArea : Leaf
{
    [SerializeField] CircleReference wanderArea;
    [SerializeField] AreaReference moveableArea;
    public override NodeResult Execute()
    {
        //Vector2 pos = moveableArea.Value.SampleCircle(wanderArea.Value);
        //wanderArea.Value.transform.position = pos;
        return NodeResult.success;
    }
}
