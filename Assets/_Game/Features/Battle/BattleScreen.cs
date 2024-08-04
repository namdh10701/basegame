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
            return base.WillPopEnter(args);
        }
        public override async UniTask WillPushEnter(Memory<object> args)
        {
            await SceneManager.LoadSceneAsync("RankingScene", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            await base.WillPushEnter(args);
            AudioManager.Instance.PlayBgmGameplay();
            
        }

        public override async UniTask WillPushExit(Memory<object> args)
        {
            BattleViewModel.CleanUp();
            Time.timeScale = 1;
            await SceneManager.UnloadSceneAsync("RankingScene");
            await base.WillPushExit(args);
        }
    }
}
