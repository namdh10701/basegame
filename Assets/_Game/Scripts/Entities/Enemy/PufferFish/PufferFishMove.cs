using _Game.Scripts.Battle;
using MBT;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class PufferFishMove : MonoBehaviour
    {
        public PufferFishAnimation pfa;
        public Rigidbody2D body;

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

        public DeviableVector2 direction;

        public float playAnimTimer; 
        public float timer;
        private void Start()
        {
            direction.RefreshValue();
            fastForce.RefreshValue();
            maxFastSpeed.RefreshValue();
            timeAccelerate.RefreshValue();
            normalSpeed.RefreshValue();
            normalForce.RefreshValue();
            slowdownTime.RefreshValue();
            slowdownSpeed.RefreshValue();
        }

        public void Move()
        {
            if (isSlowdowned)
            {
                if (slowdownTimer < slowdownTime.Value)
                {
                    slowdownTimer += Time.fixedDeltaTime;
                    if (body.velocity.magnitude < slowdownSpeed.Value)
                    {
                        body.AddForce(normalForce.Value * direction.Value);
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
                normalMoveTimer += Time.fixedDeltaTime;

                if (!animPlayed && normalMoveTimer > timer)
                {
                    pfa.PlayAnim("move_fast", false);
                    animPlayed = true;
                }

                if (normalMoveTimer < normalTime.Value)
                {
                    if (body.velocity.magnitude < normalSpeed.Value)
                    {
                        body.AddForce(normalForce.Value * direction.Value);
                    }
                }
                else
                {
                    animPlayed = false;
                    isAccelerate = true;
                    isSlowdowned = false;
                    fastTimer = 0;
                }
            }
            else if (isAccelerate)
            {

                if (fastTimer < timeAccelerate.Value)
                {

                    fastTimer += Time.fixedDeltaTime;
                    body.AddForce(fastForce.Value * direction.Value);
                }
                else
                {
                    slowdownTimer = 0;
                    isAccelerate = false;
                    isSlowdowned = true;
                    direction.RefreshValue();
                    fastForce.RefreshValue();
                    maxFastSpeed.RefreshValue();
                    timeAccelerate.RefreshValue();
                    normalSpeed.RefreshValue();
                    normalForce.RefreshValue();
                    slowdownTime.RefreshValue();
                    slowdownSpeed.RefreshValue();
                }
            }
        }

        bool animPlayed;
    }
}