﻿using _Base.Scripts.Audio;
using _Game.Features.Bootstrap;
using _Game.Features.Home;
using _Game.Scripts.DB;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using _Game.Scripts.UI.Utils;
using Cysharp.Threading.Tasks;
using Online;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZBase.UnityScreenNavigator.Core;
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
			//Debug.unityLogger.logEnabled = false;
			if (await PlayfabManager.Instance.LoginAsync())
			{
				bootstrapScreen.LoadingProgress = 0.8f;
				await GameData.Load();

				Database.Load();

				SaveSystem.LoadSave();

				AudioManager.Instance.IsBgmOn = PlayerPrefs.GetInt("Settings.MuteBGM", 0) == 0;
				AudioManager.Instance.IsSfxOn = PlayerPrefs.GetInt("Settings.MuteSFX", 0) == 0;

				bootstrapScreen.LoadingProgress = 0.8f;

				await Nav.PreloadScreenAsync<MainScreen>();

                await SceneManager.LoadSceneAsync("HaborScene", LoadSceneMode.Additive);
                bootstrapScreen.LoadingProgress = 1f;
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));

                await Nav.PopCurrentScreenAsync(false);
				await Nav.ShowScreenAsync<MainScreen>(false);
				// ShowTopPage().Forget();
			}
			else
			{
				bootstrapScreen.LoadingProgress = 1f;
				Debug.LogError("TODO: Login Failed, show error message");
			}
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