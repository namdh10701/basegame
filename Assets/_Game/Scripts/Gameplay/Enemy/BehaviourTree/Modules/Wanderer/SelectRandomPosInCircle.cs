using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Wanderer/Select Random Pos In Circle")]
public class SelectRandomPosInCircle : Leaf
{
    [SerializeField] CircleCollider2D objectCollider;
    [SerializeField] CircleReference circle;
    [SerializeField] Vector2Reference pos;
    public override NodeResult Execute()
    {
        if (objectCollider == null)
        {
            pos.Value = circle.Value.SamplePoint();
        }
        else
        {
            pos.Value = circle.Value.SamplePoint(objectCollider);
        }
        return NodeResult.success;
    }
}
