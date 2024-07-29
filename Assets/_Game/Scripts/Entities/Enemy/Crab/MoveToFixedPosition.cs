using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Crab/Move To Fixed Position")]
    public class CrabMoveToFixedPosition : Leaf
    {
        MoveAreaController controller;
        public Crab crab;
        public Rigidbody2D body;
        public StatReference statReference;
        Vector2 pos;
        public float multiplier;
        public float timelimit;
        public bool isLimitTime;
        float elapsedTime;
        public override void OnEnter()
        {
            base.OnEnter();
            elapsedTime = 0;
            controller = FindAnyObjectByType<MoveAreaController>();
            pos = controller.GetCrabMovePos();
            crab.CrabState = CrabState.Move;
        }
        public override NodeResult Execute()
        {
            if (isLimitTime)
            {
                elapsedTime += Time.fixedDeltaTime;
                if (elapsedTime > timelimit)
                {
                    return NodeResult.success;
                }
            }
            Vector2 direction = (pos - body.position).normalized;
            body.AddForce(statReference.Value.Value * multiplier * direction);

            float distance = Vector2.Distance(pos, body.position);
            if (distance > 1)
            {
                return NodeResult.running;
            }
            else
            {
                return NodeResult.success;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            crab.CrabState = CrabState.Idle;
        }
    }
}