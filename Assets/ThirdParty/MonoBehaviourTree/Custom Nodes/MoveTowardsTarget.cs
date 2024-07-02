using UnityEngine;
using MBT;

namespace MBTExample
{
    [MBTNode("Example/Move Toward Target")]
    [AddComponentMenu("")]
    public class MoveTowardTarget : Leaf
    {
        public Transform transform;
        public TransformReference transformToMove;
        public float speed = 0.1f;
        public float minDistance = 0f;
        public Rigidbody2D body;

        public override void OnEnter()
        {
            base.OnEnter();

        }

        public override NodeResult Execute()
        {
            Vector3 target = body.transform.position;
            Transform obj = transformToMove.Value;
            // Move as long as distance is greater than min. distance
            float dist = Vector3.Distance(target, obj.position);
            if (dist > minDistance)
            {
                // Move towards target
                obj.position = Vector3.MoveTowards(
                    obj.position,
                    target,
                    (speed > dist) ? dist : speed
                );
                return NodeResult.running;
            }
            else
            {
                return NodeResult.success;
            }
        }
    }
}
