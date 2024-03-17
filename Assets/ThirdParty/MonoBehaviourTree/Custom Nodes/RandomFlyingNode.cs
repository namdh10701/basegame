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
        public float time;
        float elapsedTime;
        public override void OnEnter()
        {
            base.OnEnter();
            elapsedTime = 0;

        }

        public override NodeResult Execute()
        {
            if (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                body.velocity = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
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
            body.velocity = Vector2.zero;
        }
    }


}
