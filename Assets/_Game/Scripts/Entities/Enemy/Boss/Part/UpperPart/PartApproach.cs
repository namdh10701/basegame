using _Game.Scripts;
using MBT;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Giant Octopus/Part Approach")]
    public class PartApproach : Leaf
    {
        public Rigidbody2D body;
        public PartModel part;
        public ShipReference ship;
        public Vector3 targetPos;
        public StatReference moveSpeed;
        public override void OnEnter()
        {
            base.OnEnter();
            targetPos = ship.Value.ShipArea.ClosetPointTo(body.position);

            part.State = PartState.Moving;
        }

        public override NodeResult Execute()
        {
            Vector2 direction = (Vector2)targetPos - body.position;
            body.AddForce(direction.normalized * moveSpeed.Value.Value);

            float distance = Vector2.Distance(body.position, targetPos);

            return distance > .5f ? NodeResult.running : NodeResult.success;
        }

        public override void OnExit()
        {
            base.OnExit();
            part.State = PartState.Idle;
        }
    }
}