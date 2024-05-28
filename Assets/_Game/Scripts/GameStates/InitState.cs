using System.Collections;
using System.Threading.Tasks;
using _Base.Scripts.StateMachine;
using _Game.Scripts.Bootstrap;
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
            
            var asyncInitFunc = UniTask.RunOnThreadPool(AsyncInitFunc);
            yield return new WaitUntil(() => asyncInitFunc.GetAwaiter().IsCompleted);
        }

        private async void AsyncInitFunc()
        {
            await UniTask.SwitchToMainThread();
            await GDConfigLoader.Instance.Load();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
