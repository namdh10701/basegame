using System;
using System.Collections;
using _Base.Scripts;
using _Base.Scripts.StateMachine;
using _Game.Scripts.Bootstrap;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.GameStates
{
    /// <summary>
    /// This state loads a scene 
    /// </summary>
    public class LoadSceneState : AbstractState
    {
        readonly string m_Scene;
        readonly string m_uiScene;
        readonly Action m_OnLoadCompleted;

        public override string Name => $"{nameof(LoadSceneState)}: {m_Scene}";

        /// <param name="sceneController">The SceneController for the current loading operation</param>
        /// <param name="scene">The path to the scene</param>
        /// <param name="onLoadCompleted">An action that is invoked when scene loading is finished</param>
        public LoadSceneState(string scene, Action onLoadCompleted = null)
        {
            m_Scene = scene;
            m_OnLoadCompleted = onLoadCompleted;
        }

        public override IEnumerator Execute()
        {
            yield return Game.Instance.SceneController.LoadScene(m_Scene);
        }
        public override void Exit()
        {
            base.Exit();
            m_OnLoadCompleted?.Invoke();
        }
    }
}
