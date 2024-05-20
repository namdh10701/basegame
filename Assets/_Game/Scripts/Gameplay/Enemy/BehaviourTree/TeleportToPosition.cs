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
        bool isFinished;
        bool isTriggered = false;
        MyCoroutine myCoroutine;

        public override void OnEnter()
        {
            base.OnEnter();
            isFinished = false;
            myCoroutine = new MyCoroutine(enemy, "Teleport", destination.Value, () => isFinished = true);
            myCoroutine.Start();
        }

        public override NodeResult Execute()
        {
            return isFinished ? NodeResult.success : NodeResult.running;

        }
    }


}
