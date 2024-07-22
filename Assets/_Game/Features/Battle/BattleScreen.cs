using _Base.Scripts.Audio;
using _Game.Features.Gameplay;
using _Game.Scripts.Gameplay;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Features.Battle
{
    public class BattleScreen : ZBase.UnityScreenNavigator.Core.Screens.Screen
    {
        public BattleViewModel BattleViewModel;
        public override UniTask WillPopEnter(Memory<object> args)
        {
            Debug.Log("WILL PUS CCCC");
            return base.WillPopEnter(args);
        }
        public override async UniTask WillPushEnter(Memory<object> args)
        {
            Debug.Log("WILL PUS A");
            await SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            await base.WillPushEnter(args);
            AudioManager.Instance.PlayBgmGameplay();
            BattleManager.Instance.Initialize(BattleViewModel);
            Debug.Log("WILL PUS");
        }

        public override async UniTask WillPushExit(Memory<object> args)
        {
            Debug.Log(" EXIT ");
            BattleViewModel.CleanUp();
            Time.timeScale = 1;
            await SceneManager.UnloadSceneAsync("BattleScene");
            await base.WillPushExit(args);
        }
    }
}
