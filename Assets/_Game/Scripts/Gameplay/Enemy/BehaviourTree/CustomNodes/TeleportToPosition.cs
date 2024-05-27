using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;
using _Game.Scripts.Entities;
using _Base.Scripts.Utils;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Teleport to position")]
    [AddComponentMenu("")]
    public class TeleportToPosition : Leaf
    {
        [SerializeField] MonoBehaviour enemy;
        bool isFinished;
        MyCoroutine myCoroutine;
        ITeleporter teleporter;
        public override void OnEnter()
        {
            base.OnEnter();
            teleporter = enemy.GetComponent<ITeleporter>();
            isFinished = false;
            Coroutines.StartCoroutine(Coroutine());
        }

        public override NodeResult Execute()
        {
            return isFinished ? NodeResult.success : NodeResult.running;
        }

        public void Start()
        {
            Coroutines.StartCoroutine(Coroutine());
        }

        IEnumerator Coroutine()
        {
            yield return teleporter.Teleport();
            isFinished = true;
        }
    }


}
