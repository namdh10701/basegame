using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Play Coroutine")]
    [AddComponentMenu("")]
    public class PlayCoroutine : Leaf
    {
        public EnemyAttackBehaviour attackBehaviour;
        public bool isFinished = false;
        bool played;
        public override void OnEnter()
        {
            base.OnEnter();
            isFinished = false;
            played = false;
        }
        public override NodeResult Execute()
        {
            if (!played)
            {
                attackBehaviour.PlayAttackSequence(() => { isFinished = true; });
                played = true;
            }
            if (isFinished)
            {
                return NodeResult.success;
            }
            else
            {
                return NodeResult.running;
            }

        }
    }


}
