using _Game.Features.MyShipScreen;
using _Game.Scripts.DB;
using _Game.Scripts.GD;
using _Game.Scripts.GD.Parser;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
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
            await GameLevelManager.LoadData();
            await GDConfigLoader.Instance.Load();
            Database.Load();
            ShowTopPage().Forget();
        }

        private async UniTaskVoid ShowTopPage()
        {
            var options = new ViewOptions(nameof(MainScreen), false, loadAsync: false);
            await ContainerManager.Find<ScreenContainer>(ContainerKey.Screens).PushAsync(options);
        }
    }
}