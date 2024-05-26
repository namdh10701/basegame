using _Game.Scripts.Battle;
using MBT;
using UnityEngine;

namespace _Game.Scripts.Entities
{

    [MBTNode("PufferFish/Move")]
    [AddComponentMenu("")]
    public class PufferFishMove : Leaf
    {
        public Rigidbody2D body;
        public Vector2Reference TargetPoint;

        [Header("Fast phase")]
        public DeviableFloat fastForce = new(30, 5);
        public DeviableFloat maxFastSpeed = new(3, 2);
        public DeviableFloat timeAccelerate = new(.5f, .5f);
        public bool isAccelerate;
        public float fastTimer;

        [Header("Normal phase")]
        public DeviableFloat normalSpeed = new(.3f, .1f);
        public DeviableFloat normalForce = new(.6f, .3f);
        public DeviableFloat normalTime = new(3, 1f);

        [Header("Slow phase")]
        public DeviableFloat slowdownTime = new(.25f, .125f);
        public DeviableFloat slowdownSpeed = new(.15f, .075f);

        public bool isSlowdowned;
        public float slowdownTimer = 0;
        public float normalMoveTimer;

        Vector2 direction;
        private void Start()
        {
            direction = (TargetPoint.Value - (Vector2)transform.position).normalized;
            fastForce.RefreshValue();
            maxFastSpeed.RefreshValue();
            timeAccelerate.RefreshValue();
            normalSpeed.RefreshValue();
            normalForce.RefreshValue();
            slowdownTime.RefreshValue();
            slowdownSpeed.RefreshValue();
        }

        public override NodeResult Execute()
        {
            if (isSlowdowned)
            {
                if (slowdownTimer < slowdownTime.Value)
                {
                    slowdownTimer += DeltaTime;
                    if (body.velocity.magnitude < slowdownSpeed.Value)
                    {
                        body.AddForce(normalForce.Value * direction);
                    }
                }
                else
                {
                    isSlowdowned = false;
                    isAccelerate = false;
                    normalMoveTimer = 0;
                }
            }
            else if (!isAccelerate)
            {
                normalMoveTimer += DeltaTime;
                if (normalMoveTimer < normalTime.Value)
                {
                    if (body.velocity.magnitude < normalSpeed.Value)
                    {
                        body.AddForce(normalForce.Value * direction);
                    }
                }
                else
                {
                    isAccelerate = true;
                    isSlowdowned = false;
                    fastTimer = 0;
                }
            }
            else if (isAccelerate)
            {
                if (fastTimer < timeAccelerate.Value)
                {

                    fastTimer += DeltaTime;
                    body.AddForce(fastForce.Value * direction);
                }
                else
                {
                    slowdownTimer = 0;
                    isAccelerate = false;
                    isSlowdowned = true;
                }
            }
            ClampVel(maxFastSpeed.Value);


            return NodeResult.success;
        }

        void ClampVel(float maxVel)
        {
            body.velocity = Vector2.ClampMagnitude(body.velocity, maxVel);
        }

    }
}