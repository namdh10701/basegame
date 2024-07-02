﻿using System.Collections;
using System.Threading.Tasks;
using _Base.Scripts.StateMachine;
using _Game.Scripts.Bootstrap;
using _Game.Scripts.DB;
using _Game.Scripts.GD;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Scripts.GameStates
{
    /// <summary>
    /// Delays the state-machine for the set amount
    /// </summary>
    public class InitState : AbstractState
    {
        public override string Name => nameof(InitState);

        public override IEnumerator Execute()
        {
            Game.Instance.AssetLoader.Load();
            Game.Instance.GameManager.LoadDatabase();
            SaveSystem.LoadSave();

            // yield return new WaitForSeconds(10.0f);
            Application.targetFrameRate = 120;
            Debug.unityLogger.logEnabled = false;
            var asyncInitFunc = UniTask.RunOnThreadPool(async () => await AsyncInitFunc());
            yield return new WaitUntil(() => asyncInitFunc.Status.IsCompleted());
            Database.Load();
        }

        private async Task AsyncInitFunc()
        {
            // await UniTask.Delay(10000);


            await UniTask.SwitchToMainThread();
            
            Task levelDesignLoadTask = LevelDesignConfigLoader.Instance.Load();
            Task gdConfigLoadTask = GDConfigLoader.Instance.Load();
            await Task.WhenAll(levelDesignLoadTask, gdConfigLoadTask);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
