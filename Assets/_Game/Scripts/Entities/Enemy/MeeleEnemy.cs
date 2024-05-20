using _Game.Scripts.Gameplay.Ship;
using MBT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Game.Scripts.Entities
{
    public class MeeleEnemy : Enemy
    {
        public Vector2[] targetPoses = new Vector2[2];


        public void SetTargetPosition(Vector2[] targetPoses)
        {
            if (_blackboard == null)
            {
                _blackboard = GetComponent<Blackboard>();
            }

            _blackboard.GetVariable<Vector2Variable>("StartPoint").Value = targetPoses[0];
            _blackboard.GetVariable<Vector2Variable>("Destination").Value = targetPoses[1];
        }
        public override IEnumerator Teleport(Vector2 pos)
        {
            yield break;
        }
    }
}
