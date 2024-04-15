using _Base.Scripts;
using _Base.Scripts.StateMachine;
using _Base.Scripts.UI.Managers;
using _Base.Scripts.Utils;
using _Game.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace _Game.Scripts.GameStates
{
    public class TransitState : AbstractState
    {
        public override string Name => nameof(TransitState);
        SceneRef toScene;
        SceneRef toUIScene;
        _Base.Scripts.SceneController sceneController;
        public TransitState(SceneRef toScene, SceneRef toUIScene, Transition transition, _Base.Scripts.SceneController sceneController)
        {
            this.sceneController = sceneController;
            this.toScene = toScene;
            this.toUIScene = toUIScene;
        }
        public override IEnumerator Execute()
        {
            int fromScene = SceneManager.GetActiveScene().buildIndex;

            yield return ViewTransitionManager.Instance.TransitIn();
            if (toScene != null && toScene.m_ScenePath != SceneManager.GetActiveScene().path)
            {
                yield return SceneManager.LoadSceneAsync(toScene.m_ScenePath, LoadSceneMode.Additive);
                if (fromScene != 0)
                {
                    yield return SceneManager.UnloadSceneAsync(fromScene);
                }
            }

            if (toUIScene != null)
            {
                Debug.Log(toUIScene);
                yield return sceneController.LoadUIScene(toUIScene.m_ScenePath);
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByPath(toScene.m_ScenePath));
            yield return null;
        }

        public override void Exit()
        {
            base.Exit();
            ViewTransitionManager.Instance.TransitOut();
        }
    }
}