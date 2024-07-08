using _Game.Features.Gameplay;
using _Game.Scripts.Gameplay;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace _Game.Features.Battle
{
    public class BattleScreen : ZBase.UnityScreenNavigator.Core.Screens.Screen
    {
        public GameObject GameplayRoot;
        public BattleViewModel BattleViewModel;

        public override void DidPopEnter(Memory<object> args)
        {
            base.DidPopEnter(args);
        }
        public override UniTask Initialize(Memory<object> args)
        {
            GameplayRoot = GameObject.Find("Gameplay").transform.GetChild(0).gameObject;
            GameplayRoot.SetActive(true);
            BattleManager.Instance.Initialize(BattleViewModel);
            return base.Initialize(args);
        }

        public override UniTask WillPushExit(Memory<object> args)
        {
            Debug.Log(" EXIT ");
            GameplayRoot.SetActive(false);
            BattleManager.Instance.CleanUp();
            return base.WillPushExit(args);
        }
    }
}
