using _Base.Scripts.Audio;
using _Game.Features.Gameplay;
using _Game.Scripts.Gameplay;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Features.Battle
{
    public class RankingBatlleScene : ZBase.UnityScreenNavigator.Core.Screens.Screen
    {
        public RankingBattleViewModel BattleViewModel;
        public override UniTask WillPopEnter(Memory<object> args)
        {
            return base.WillPopEnter(args);
        }
        public override async UniTask WillPushEnter(Memory<object> args)
        {
            await SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            await base.WillPushEnter(args);
            AudioManager.Instance.PlayBgmGameplay();

        }

        public override async UniTask WillPushExit(Memory<object> args)
        {
            BattleViewModel.CleanUp();
            Time.timeScale = 1;
            await SceneManager.UnloadSceneAsync("BattleScene");
            await base.WillPushExit(args);
        }
    }
}