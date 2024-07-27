using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Skull Gang/Move To Fixed Position")]
    public class MoveToFixedPosition : Leaf
    {
        MoveAreaController controller;
        public SkullGang skullGang;
        public Rigidbody2D body;
        public StatReference statReference;
        Vector2 pos;
        public override void OnEnter()
        {
            base.OnEnter();
            controller = FindAnyObjectByType<MoveAreaController>();
            pos = controller.GetSkullGangMovePos();
            skullGang.State = SkullGangState.Moving;
        }
        public override NodeResult Execute()
        {

            Vector2 direction = (pos - body.position).normalized;
            body.AddForce(statReference.Value.Value * direction);

            float distance = Vector2.Distance(pos, body.position);
            if (distance > 2)
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
            skullGang.State = SkullGangState.Idle;
        }
    }
}