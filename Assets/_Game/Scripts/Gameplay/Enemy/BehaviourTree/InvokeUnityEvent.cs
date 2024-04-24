using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Invoke Unity Event")]
    [AddComponentMenu("")]
    public class InvokeUnityEvent : Leaf
    {
        public UnityEvent UnityEvent;

        public override NodeResult Execute()
        {
            UnityEvent.Invoke();
            return NodeResult.success;

        }
    }


}
