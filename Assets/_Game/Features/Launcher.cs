using System.Threading.Tasks;
using _Game.Features.Home;
using _Game.Features.MyShipScreen;
using _Game.Scripts.DB;
using _Game.Scripts.GD;
using _Game.Scripts.GD.Parser;
using Cysharp.Threading.Tasks;
using Map;
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
            // Debug.unityLogger.logEnabled = false;
            Application.targetFrameRate = 120;
            UnityScreenNavigatorSettings.Initialize();

            var loadDataTasks = new Task[]
            {
                GameLevelManager.Instance.LoadData(),

                ShopDataItem.Instance.LoadData(),

                // Debug.Log("Load" + "ShopDataListing");
                ShopDataListing.Instance.LoadData(),

                // Debug.Log("Load" + "ShopDataRarity");
                ShopDataRarity.Instance.LoadData(),

                // Debug.Log("Load" + "GDConfigLoader");
                GDConfigLoader.Instance.Load(),
            };

            await Task.WhenAll(loadDataTasks);
            
            Debug.Log("Load" + "Database");
            Database.Load();

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