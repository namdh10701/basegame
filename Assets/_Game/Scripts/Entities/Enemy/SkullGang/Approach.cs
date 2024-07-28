using _Game.Scripts;
using BehaviorDesigner.Runtime.Tasks;
using MBT;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Skull Gang/Approach")]

    public class Approach : Leaf
    {
        public Rigidbody2D body;
        public StatReference statReference;
        public ShipReference shipReference;
        public SkullGang skullGang;
        public override void OnEnter()
        {
            base.OnEnter();
            skullGang.State = SkullGangState.Moving;
        }
        public override void OnExit()
        {
            base.OnExit();

            skullGang.State = SkullGangState.Idle;
        }
        public override NodeResult Execute()
        {
            Vector2 pos = shipReference.Value.ShipArea.ClosetPointTo(body.position);
            Vector2 direction = (pos - body.position).normalized;
            body.AddForce(statReference.Value.Value * direction);

            float distance = Vector2.Distance(shipReference.Value.transform.position, body.position);
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