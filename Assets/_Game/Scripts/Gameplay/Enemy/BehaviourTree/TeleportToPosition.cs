using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;
using _Game.Scripts.Entities;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Teleport to position")]
    [AddComponentMenu("")]
    public class TeleportToPosition : Leaf
    {
        [SerializeField] Enemy enemy;
        [SerializeField] Vector2Reference destination;
        bool isfinished;
        bool isTriggered = false;
        public override void OnEnter()
        {
            base.OnEnter();
            isfinished = false;
            isTriggered = false;
        }

        public override NodeResult Execute()
        {
            if (!isTriggered)
            {
                isTriggered = true;
                enemy.Teleport(destination.Value, () =>
            isfinished = true);
            }

            return isfinished ? NodeResult.success : NodeResult.running;

        }
        public override void OnExit()
        {
            base.OnExit();
            isfinished = false;
        }
    }


}
