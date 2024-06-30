using _Game.Scripts.Gameplay;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace _Game.Features.Battle
{
    public class BattleScreen : ZBase.UnityScreenNavigator.Core.Screens.Screen
    {
        public GameObject GameplayRoot;
        public BattleManager BattleManager;

        public override void DidPopEnter(Memory<object> args)
        {
            base.DidPopEnter(args);
            Debug.Log("Enter ");
        }
        public override UniTask Initialize(Memory<object> args)
        {
            GameplayRoot = GameObject.Find("Gameplay").transform.GetChild(0).gameObject;
            GameplayRoot.SetActive(true);
            BattleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
            BattleManager.Initnialize();
            return base.Initialize(args);
        }

        public override UniTask WillPushExit(Memory<object> args)
        {
            GameplayRoot.SetActive(false);
            BattleManager.CleanUp();
            return base.WillPushExit(args);
        }
    }
}
