using _Game.Scripts;
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
namespace _Game.Scripts.BehaviourTree
{
    [MBTNode("Wanderer/Wander")]
    public class Wander : Leaf
    {
        public SpineAnimationEnemyHandler SpineAnimationEnemyHandler;

        [Header("Detector")]
        public AreaReference MoveableArea;
        public OctaDirectionRay Detector;
        public List<DirectionRay> RayToSelectDirection;

        [Header("Movement")]
        public Rigidbody2D Body;
        public DeviableFloat Force;
        public DeviableFloat MaxSpeed;


        [Header("Direction")]
        public State CurrentState;
        public Vector2 CurrentDirection;
        public Vector2 TargetDirection;
        public float ChangeDirectionSpeed;


        public float Timea;
        float timer;



        public DeviableFloat WanderTime;
        float wanderTimer;
        public bool IsTimeConstraint;
        private void Awake()
        {
            //Detector.OnChanged += UpdateDirection;
        }

        bool isPlayed;
        public override void OnEnter()
        {
            base.OnEnter();
            if (IsTimeConstraint)
            {
                WanderTime.RefreshValue(); wanderTimer = 0;
            }
            Detector.SetArea(MoveableArea.Value);
            SpineAnimationEnemyHandler.PlayMove();
        }
        public override NodeResult Execute()
        {
            if (!isPlayed)
            {
                isPlayed = true;
                WanderTime.RefreshValue();
            }
            wanderTimer += Time.fixedDeltaTime;
            timer += Time.fixedDeltaTime;
            if (Detector.IsInBounds)
            {
                if (timer > Timea)
                {
                    timer = 0;
                    UpdateTargetDirection(-50, 50);
                }
            }
            else
            {
                FindWayToBounds();
            }
            CurrentDirection = Vector2.Lerp(CurrentDirection, TargetDirection, ChangeDirectionSpeed);
            Move();
            ClampVel();
            Debug.DrawLine(Body.transform.position, (Vector3)CurrentDirection * 2 + Body.transform.position, Color.blue);
            if (!IsTimeConstraint)
            {
                return NodeResult.success;
            }
            else
            {
                return wanderTimer < WanderTime.Value ? NodeResult.running : NodeResult.success;
            }
        }
        bool needFindWay;
        void FindWayToBounds()
        {

            TargetDirection = (MoveableArea.Value.transform.position - Body.transform.position).normalized;
        }

        void Move()
        {
            if (Body.velocity.magnitude < MaxSpeed.Value)
            {
                Body.AddForce(Force.Value * CurrentDirection);
            }
        }

        void UpdateDirection()
        {
            if (MoveableArea.Value.bounds.Contains(Body.position))
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
                if (Body.position.y > MoveableArea.Value.bounds.max.y)
                {
                    isAbove = true;
                }
                else
                {
                    isAbove = false;
                }

                if (Body.position.x < MoveableArea.Value.bounds.max.x)
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

        float downangle = -20;
        float upangle = 0;

        public void ToLeft()
        {
            if (CurrentState != State.MovingRight)
            {
                CurrentState = State.MovingRight;
                UpdateTargetDirection(-20, 20);
            }
        }

        public void ToRight()
        {
            if (CurrentState != State.MovingLeft)
            {
                CurrentState = State.MovingLeft;
                UpdateTargetDirection(-20, 20);
            }
        }



        public bool isAbleToMoveUp;
        public bool isAbleToMoveDown;



        public void ToDown()
        {
            if (isAbleToMoveDown)
            {
                isAbleToMoveDown = false;
                UpdateTargetDirection(-50f, 50f);
            }
        }

        public void ToUp()
        {
            if (isAbleToMoveUp)
            {
                isAbleToMoveUp = false;
                UpdateTargetDirection(-50f, 50f);
            }
        }

        public void OutUp()
        {
            isAbleToMoveUp = true;
        }

        public void OutDown()
        {
            isAbleToMoveDown = true;

        }


        public void UpdateTargetDirection(float low, float high)
        {
            timer = 0;
            Force.RefreshValue();
            MaxSpeed.RefreshValue();
            if (MoveableArea == null)
            {
                return;
            }
            if (MoveableArea.Value.bounds.Contains(Body.position))
            {
                //RayToSelectDirection = //Detector.ReverseIntersectingRays;
                RayToSelectDirection = Detector.NotIntersectingRays;
                if (CurrentState == State.MovingLeft)
                {
                    foreach (DirectionRay ray in RayToSelectDirection.ToArray())
                    {
                        if (ray.rayDirection.x > 0)
                        {
                            RayToSelectDirection.Remove(ray);
                        }
                    }
                }
                else
                {
                    foreach (DirectionRay ray in RayToSelectDirection.ToArray())
                    {
                        if (ray.rayDirection.x < 0)
                        {
                            RayToSelectDirection.Remove(ray);
                        }
                    }
                }
            }


            downangle = low;
            upangle = high;

            if (!isAbleToMoveDown)
            {
                downangle = 0;
            }
            if (!isAbleToMoveUp)
            {
                upangle = 0;
            }


            if (CurrentState == State.MovingRight)
            {

                TargetDirection = Quaternion.Euler(0, 0, Random.Range(downangle, upangle)) * Vector2.right;
            }
            else
            {
                TargetDirection = Quaternion.Euler(0, 0, Random.Range(180 - upangle, 180 - downangle)) * Vector2.right;
            }
        }


        void ClampVel()
        {
            //Body.velocity = Vector2.ClampMagnitude(Body.velocity, MaxSpeed.Value);
        }
    }
}