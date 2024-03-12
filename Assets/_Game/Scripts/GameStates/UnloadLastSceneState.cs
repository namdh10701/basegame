using System.Collections;
using _Base.Scripts;
using _Base.Scripts.StateMachine;

namespace _Game.Scripts.GameStates
{
    /// <summary>
    /// Unloads a currently loaded scene
    /// </summary>
    public class UnloadLastSceneState : AbstractState
    {
        readonly SceneController m_SceneController;

        /// <param name="sceneController">The SceneController for the current unloading operation</param>
        public UnloadLastSceneState(SceneController sceneController)
        {
            m_SceneController = sceneController;
        }
        
        public override IEnumerator Execute()
        {
            yield return m_SceneController.UnloadLastScene();
        }
    }
}