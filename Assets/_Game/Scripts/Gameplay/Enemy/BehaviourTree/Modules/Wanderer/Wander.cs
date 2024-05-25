using _Game.Scripts.Battle;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[MBTNode("Wanderer/Wander")]
public class Wander : Leaf
{
    public MonoBehaviour MonoBehaviour;

    public DeviableFloat TargetSpeed = new(2.5f, 0.25f);
    public float Force;


    public Area MoveableArea;
    public CircleCollider2D SampledMoveArea;
    public CircleCollider2D Collider;
    public Rigidbody2D Body;
    public Vector2 destination;

    public Vector2 direction;
    public Vector2 currentDirection;


    public float ChangeDirectionSpeed;
    public float changeDirectionTime;
    public float changeDiretionTimer;

    public float distance;
    public float currentDistance;
    
    public Vector2 SelectDestination()
    {
        SampledMoveArea.transform.position = MoveableArea.SampleCircle(SampledMoveArea, Body.position, 3);
        Vector2 ret = SampledMoveArea.SamplePoint(Collider);
        return ret;

    }

    public override void OnEnter()
    {
        base.OnEnter();

    }
    public override NodeResult Execute()
    {

        direction = (destination - Body.position).normalized;
        Debug.Log(DeltaTime);
        currentDirection = Vector2.Lerp(currentDirection, direction, ChangeDirectionSpeed);

        if (!SampledMoveArea.bounds.Contains(Body.position))
        {
        
            if (Body.velocity.magnitude < TargetSpeed.Value)
            {
                Body.AddForce(currentDirection * Force);
            }
        }
        else
        {
                distance = ((destination - Body.position)).magnitude;
            destination = SelectDestination();
            TargetSpeed.RefreshValue();
        }


        ClampVel();
        return NodeResult.success;
    }

    void ClampVel()
    {
        Body.velocity = Vector2.ClampMagnitude(Body.velocity, TargetSpeed.Value);
    }
}
