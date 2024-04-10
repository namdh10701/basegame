using MBT;
using UnityEngine;

namespace Demo.Scripts.Enemy
{
    public class Shark : Enemy
    {
        [SerializeField] HitFx hitFx;
        [SerializeField] Vector2 targetPos;
        protected override void Start()
        {
            base.Start();
            Collider2D col = target.GetComponent<Collider2D>();
            targetPos = col.ClosestPoint(transform.position);
            blackboard.GetVariable<Vector3Variable>("targetPos").Value = targetPos;
            blackboard.GetVariable<TransformVariable>("target").Value = target;
        }
        public override void DoAttack()
        {
            base.DoAttack();
            hitFx.transform.position = targetPos;
            hitFx.Play();
        }

 
    }
}