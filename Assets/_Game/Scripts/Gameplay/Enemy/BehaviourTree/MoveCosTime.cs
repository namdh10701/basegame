using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Move Cos Time")]
    [AddComponentMenu("")]
    public class MoveCosTime : Leaf
    {
        public Rigidbody2D body;
        [SerializeField] private float frequency;
        [SerializeField] private float amplitude;
        [SerializeField] private float modifier;
        [SerializeField] TargetInRange range;
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override NodeResult Execute()
        {
            body.velocity = Mathf.Clamp((Mathf.Cos(Time.time * frequency) * amplitude + modifier), 0, 10000) * Vector2.down;
            return range.isMet ? NodeResult.success : NodeResult.running;
        }
        public override void OnExit()
        {
            base.OnExit();
            body.velocity = Vector3.zero;
        }
    }


}
