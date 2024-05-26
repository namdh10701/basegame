using _Game.Scripts.Battle;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem;
using JetBrains.Annotations;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum State
{
    MovingLeft, MovingRight
}

[MBTNode("Wanderer/Wander")]
public class Wander : Leaf
{
    [Header("Detector")]
    public Area MoveableArea;
    public OctaDirectionRay Detector;
    public List<DirectionRay> RayToSelectDirection;

    [Header("Movement")]
    public Rigidbody2D Body;
    public float Force;
    public float MaxSpeed;


    [Header("Direction")]
    public State CurrentState;
    public Vector2 CurrentDirection;
    public Vector2 TargetDirection;
    public float ChangeDirectionSpeed;


    public float Timea;
    float timer;
    private void Awake()
    {
        //Detector.OnChanged += UpdateDirection;
    }

    public override void OnEnter()
    {
        base.OnEnter();

    }
    public override NodeResult Execute()
    {
        UpdateState();
        //UpdateDirection();

        timer += Time.fixedDeltaTime;
        if (timer > Timea)
        {
            timer = 0;
            UpdateTargetDirection();
        }
        CurrentDirection = Vector2.Lerp(CurrentDirection, TargetDirection, ChangeDirectionSpeed);
        Move();
        ClampVel();
        return NodeResult.success;
    }


    void Move()
    {
        Body.AddForce(Force * CurrentDirection);
    }

    void UpdateDirection()
    {
        Debug.Log("UPDATE");
        if (MoveableArea.bounds.Contains(Body.position))
        {
            //RayToSelectDirection = //Detector.ReverseIntersectingRays;
            RayToSelectDirection = Detector.NotIntersectingRays;
            if (CurrentState == State.MovingLeft)
            {
                foreach (DirectionRay ray in RayToSelectDirection.ToArray())
                {
                    if (ray.rayDirection.x >= 0)
                    {
                        RayToSelectDirection.Remove(ray);
                    }
                }
            }
            else
            {
                foreach (DirectionRay ray in RayToSelectDirection.ToArray())
                {
                    if (ray.rayDirection.x <= 0)
                    {
                        RayToSelectDirection.Remove(ray);
                    }
                }
            }
        }
        else
        {
            bool isAbove = false;
            bool isLeft = false;
            RayToSelectDirection = Detector.DirectionRay;
            if (Body.position.y > MoveableArea.bounds.max.y)
            {
                isAbove = true;
            }
            else
            {
                isAbove = false;
            }

            if (Body.position.x < MoveableArea.bounds.max.x)
            {
                isLeft = true;
            }
            else
            {
                isLeft = false;
            }

            foreach (DirectionRay ray in RayToSelectDirection.ToArray())
            {
                if (isAbove)
                {
                    if (ray.rayDirection.y > 0)
                    {
                        RayToSelectDirection.Remove(ray);
                    }
                }
                else
                {
                    if (ray.rayDirection.y < 0)
                    {
                        RayToSelectDirection.Remove(ray);
                    }
                }
                if (isLeft)
                {
                    if (ray.rayDirection.x < 0)
                    {
                        RayToSelectDirection.Remove(ray);
                    }
                }
                else
                {
                    if (ray.rayDirection.x > 0)
                    {
                        RayToSelectDirection.Remove(ray);
                    }
                }
            }
        }
        //TargetDirection = GetRandom(RayToSelectDirection.ToArray());
    }

    Vector2 GetRandom(DirectionRay[] rays)
    {
        DirectionRay randomRay = rays[Random.Range(0, rays.Length)];
        return randomRay.rayDirection;

    }

    public void Toleft()
    {
        CurrentState = State.MovingLeft;
        UpdateTargetDirection();
    }

    public void ToRight()
    {
        CurrentState = State.MovingRight;
        UpdateTargetDirection();
    }

    public void ToDown()
    {
        downangle = -20;
        upangle = 0; UpdateTargetDirection();
    }

    public void ToUp()
    {
        downangle = -20;
        upangle = 0; UpdateTargetDirection();
    }

    void UpdateState()
    {
        /*if (!MoveableArea.bounds.Contains(Body.position))
        {
            if (Body.position.x < MoveableArea.bounds.min.x)
            {
                CurrentState = State.MovingLeft;
            }

            if (Body.position.x > MoveableArea.bounds.max.x)
            {
                CurrentState = State.MovingRight;
            }
            if (Body.position.y < MoveableArea.bounds.min.y || Body.position.y > MoveableArea.bounds.max.y)
            {
                // Invert Y direction when out of vertical bounds
                TargetDirection.y = -TargetDirection.y;
            }
            // UpdateTargetDirection();
            Debug.Log("HERE");
        }*/
    }
    float downangle = 0;
    float upangle = 0;
    void UpdateTargetDirection()
    {
        if (CurrentState == State.MovingLeft)
        {
            TargetDirection = Quaternion.Euler(0, 0, Random.Range(downangle, upangle)) * Vector2.right;

        }
        else
        {
            TargetDirection = Quaternion.Euler(0, 0, Random.Range(downangle, upangle)) * Vector2.left;
        }
        downangle = -20;
        upangle = 20;
    }


    void ClampVel()
    {
        Body.velocity = Vector2.ClampMagnitude(Body.velocity, MaxSpeed);
    }
}
