using _Game.Scripts;
using BehaviorDesigner.Runtime.Tasks;
using MBT;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Crab/Move Forward")]

    public class MoveForward : Leaf
    {
        public Rigidbody2D body;
        public StatReference statReference;
        public ShipReference shipReference;
        public Crab crab;
        public float minDistance;
        public float maxDistance;
        public float multiplier;

        Vector2 targetPosition;
        public override void OnEnter()
        {
            base.OnEnter();
            crab.CrabState = CrabState.Move;
            targetPosition = body.position + Vector2.down * Random.Range(minDistance, maxDistance);
        }
        public override void OnExit()
        {
            base.OnExit();
            crab.CrabState = CrabState.Idle;
        }
        public override NodeResult Execute()
        {
            Vector2 pos = targetPosition;
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
    }
}