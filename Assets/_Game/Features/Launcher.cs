using System.Threading.Tasks;
using _Base.Scripts.Audio;
using _Game.Features.Home;
using _Game.Features.MyShipScreen;
using _Game.Scripts.DB;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.GD.Parser;
using _Game.Scripts.SaveLoad;
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
            if (!PlayerPrefs.HasKey("PlayingStage"))
            {
                PlayerPrefs.SetString("PlayingStage", "0001");
            }
            //Debug.unityLogger.logEnabled = false;
            Application.targetFrameRate = 120;
            UnityScreenNavigatorSettings.Initialize();
            
            await GameData.Load();
            Database.Load();
            SaveSystem.LoadSave();

            // AudioManager.Instance.LoadSoundSettings();
            AudioManager.Instance.IsBgmOn = !SaveSystem.GameSave.Settings.MuteBGM;
            AudioManager.Instance.IsSfxOn = !SaveSystem.GameSave.Settings.MuteSFX;

            PlayfabManager.Instance.Login();
            // MapPlayerTracker.Instance.OnStagePassed += OnOnStagePassed;
            ShowTopPage().Forget();
        }

        private void OnOnStagePassed()
        {

        }

        private async UniTaskVoid ShowTopPage()
        {
            var options = new ViewOptions(nameof(MainScreen), false, loadAsync: false);
            await ContainerManager.Find<ScreenContainer>(ContainerKey.Screens).PushAsync(options);
        }
    }
}