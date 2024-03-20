using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace MBTExample
{
    [MBTNode("Example/Random fly Target")]
    [AddComponentMenu("")]
    public class RandomFlyingNode : Leaf
    {
        public Rigidbody2D body;
        float targetVelocity = 6f;
        public float elapsedTime;
        float moveTime;
        public Vector2Reference targetPosition = new Vector2Reference(VarRefMode.DisableConstant);
        float distance;
        Vector2 startPos;
        public override void OnEnter()
        {
            base.OnEnter();
            startPos = body.position;
            elapsedTime = 0;
            distance = Vector2.Distance((Vector2)body.transform.position, targetPosition.Value);
            moveTime = distance / targetVelocity;
            targetVelocity = 6f;
        }

        public override NodeResult Execute()
        {
            elapsedTime += Time.deltaTime;
            Vector3 pos = Vector2.Lerp(startPos, targetPosition.Value, elapsedTime / moveTime);
            body.MovePosition(pos);
            if (elapsedTime <= moveTime)
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
            body.MovePosition(targetPosition.Value);
        }
    }


}
