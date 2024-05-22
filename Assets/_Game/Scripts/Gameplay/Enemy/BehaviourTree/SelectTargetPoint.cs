using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode(name = "PufferFish/Select Target Point")]
public class SelectTargetPoint : Leaf
{
    public ShipReference shipReference;
    public Vector2Reference targetPoint;
    public override void OnEnter()
    {
        base.OnEnter();
        Collider2D collider = shipReference.Value.EntityCollisionDetector.GetComponent<Collider2D>();
        targetPoint.Value = GetClosetPoint(collider);

    }
    public override NodeResult Execute()
    {
        return NodeResult.success;
    }

    Vector2 GetClosetPoint(Collider2D collider)
    {
        Vector2 closestPoint = collider.ClosestPoint(transform.position);
        return closestPoint;
    }


}
