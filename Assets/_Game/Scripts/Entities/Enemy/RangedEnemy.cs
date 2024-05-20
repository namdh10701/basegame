
using _Game.Scripts.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class RangedEnemy : Enemy
    {
        public PositionPool positionPool;

        protected override void Start()
        {
            base.Start();
            GameObject moveArea = GameObject.Find("MoveArea");
            if (moveArea != null)
            {
                Area area = moveArea.GetComponent<Area>();
                _blackboard.GetVariable<AreaVariable>("MoveArea").Value = area;

            }
        }

        public void SetTargetPoses(List<Vector2> targetPoses)
        {
            positionPool.SetPool(targetPoses);
        }

        public override IEnumerator Teleport(Vector2 pos)
        {
            SpineAnimationEnemyHandler.PlayAnim(Anim.Hide, false, () => collider.enabled = false);
            yield return new WaitForSecondsRealtime(2f);
            body.MovePosition(pos);
            SpineAnimationEnemyHandler.PlayAnim(Anim.Appear, false, () => collider.enabled = true);
        }
    }
}
