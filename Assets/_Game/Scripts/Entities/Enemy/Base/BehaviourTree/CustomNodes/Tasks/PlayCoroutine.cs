using _Base.Scripts.Unity.EditorUsedAttributes;
using MBT;
using System;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Play Coroutine")]
    [AddComponentMenu("")]
    public class PlayCoroutine : Leaf
    {
        [MonoScript(typeof(MonoBehaviour))]
        [SerializeField] private string type;
        [SerializeField] private MonoBehaviour monoBehaviour;
        [SerializeField] private string coroutineName;
        private bool isFinished = false;
        MyCoroutine myCoroutine;
        public override void OnEnter()
        {
            base.OnEnter();
            isFinished = false;
            monoBehaviour = (MonoBehaviour)monoBehaviour.GetComponent(Type.GetType(type));
            myCoroutine = new MyCoroutine(monoBehaviour, coroutineName, null, () => isFinished = true);
            myCoroutine.Start();
        }
        public override NodeResult Execute()
        {
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
