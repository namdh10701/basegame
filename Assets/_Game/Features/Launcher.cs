using System;
using System.Threading.Tasks;
using _Base.Scripts.Audio;
using _Game.Features.Bootstrap;
using _Game.Features.Home;
using _Game.Features.MyShipScreen;
using _Game.Scripts.DB;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.GD.Parser;
using _Game.Scripts.SaveLoad;
using _Game.Scripts.UI.Utils;
using Cysharp.Threading.Tasks;
using Map;
using Online;
using UnityEngine;
using ZBase.UnityScreenNavigator.Core;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;
using ZBase.UnityScreenNavigator.Core.Windows;

namespace _Game.Features
{
    public class Launcher : UnityScreenNavigatorLauncher
    {
        public static WindowContainerManager ContainerManager { get; private set; }

        protected override void OnAwake()
        {
            ContainerManager = this;
        }

        protected override async void OnPostCreateContainers()
        {
            var bootstrapScreen = await Nav.ShowScreenAsync<BootstrapScreen>(false);
            
            if (!PlayerPrefs.HasKey("PlayingStage"))
            {
                PlayerPrefs.SetString("PlayingStage", "0001");
            }
            
            Application.targetFrameRate = 120;
            UnityScreenNavigatorSettings.Initialize();

            await PlayfabManager.Instance.LoginAsync();

            bootstrapScreen.LoadingProgress = 0.8f;
            await GameData.Load();
            
            Database.Load();
            
            SaveSystem.LoadSave();
            
            // TODO test
            // PlayfabManager.Instance.RankInfo.EndTimestamp = (ulong)DateTime.UtcNow.AddDays(2).ToFileTimeUtc();
            
            AudioManager.Instance.IsBgmOn = PlayerPrefs.GetInt("Settings.MuteBGM", 0) == 0;
            AudioManager.Instance.IsSfxOn = PlayerPrefs.GetInt("Settings.MuteSFX", 0) == 0;

            bootstrapScreen.LoadingProgress = 0.8f;
            
            await Nav.PreloadScreenAsync<MainScreen>();

            bootstrapScreen.LoadingProgress = 1f;

            await Nav.PopCurrentScreenAsync(false);
            await Nav.ShowScreenAsync<MainScreen>(false);
            // ShowTopPage().Forget();
        }

        private void OnOnStagePassed()
        {

        }

        private async UniTaskVoid ShowTopPage()
        {
            await Nav.ShowScreenAsync<MainScreen>(false);
        }
    }
}