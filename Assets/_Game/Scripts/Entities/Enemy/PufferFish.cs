using _Game.Scripts.Battle;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{

    public class PufferFish : EnemyAttackBehaviour
    {
        public PufferFishAnimation Animation;
        public Enemy Enemy;
        public Rigidbody2D body;
        public Vector2 direction;

        [Header("Fast phase")]
        public DeviableFloat fastForce = new(30, 5);
        public DeviableFloat maxFastSpeed = new(3, 2);
        public DeviableFloat timeAccelerate = new(.5f, .5f);
        bool isAccelerate;
        float fastTimer;

        [Header("Normal phase")]
        public DeviableFloat normalSpeed = new(.3f, .1f);
        public DeviableFloat normalForce = new(.6f, .3f);

        [Header("Slow phase")]
        public DeviableFloat slowdownTime = new(.25f, .125f);
        public DeviableFloat slowdownSpeed = new(.15f, .075f);
        bool isSlowdowned;
        float slowdownTimer = 0;


        bool isDecelerate;
        private void Start()
        {
            fastForce.RefreshValue();
            maxFastSpeed.RefreshValue();
            timeAccelerate.RefreshValue();
            normalSpeed.RefreshValue();
            normalForce.RefreshValue();
            slowdownTime.RefreshValue();
            slowdownSpeed.RefreshValue();
        }
        private void FixedUpdate()
        {
            if (!isDecelerate)
            {
                if (isSlowdowned)
                {
                    if (slowdownTimer < slowdownTime.Value)
                    {
                        slowdownTimer += Time.fixedDeltaTime;
                        if (body.velocity.magnitude < slowdownSpeed.Value)
                        {
                            body.AddForce(normalForce.Value * direction);
                        }
                    }
                    else
                    {
                        isSlowdowned = false;

                    }
                }
                else
                {
                    if (body.velocity.magnitude < normalSpeed.Value)
                    {
                        body.AddForce(normalForce.Value * direction);
                    }
                }
                if (isAccelerate)
                {
                    if (fastTimer < timeAccelerate.Value)
                    {
                        fastTimer += Time.fixedDeltaTime;
                        body.AddForce(fastForce.Value * direction);
                    }
                    else
                    {
                        slowdownTimer = 0;
                        isAccelerate = false;
                        isSlowdowned = true;
                    }
                }
                ClampVel(fastForce.Value);
            }
            else
            {
                body.drag = 10;
            }
        }

        void ClampVel(float maxVel)
        {
            body.velocity = Vector2.ClampMagnitude(body.velocity, maxVel);
        }

        public override IEnumerator AttackSequence()
        {
            Animation.ChargeExplode();
            yield return new WaitForSeconds(2);
            Enemy.Die();
            yield break;
        }

        public void Move()
        {
            isSlowdowned = false;
            isAccelerate = true;
            fastTimer = 0;
        }

        public void Decelerate()
        {
            isDecelerate = true;
        }

        public override void DoAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}