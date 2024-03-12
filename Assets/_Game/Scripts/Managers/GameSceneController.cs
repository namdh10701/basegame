using _Base.Scripts;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Managers
{
    public class GameSceneController : SceneController
    {
        protected override Scene GetDefaultNeverUnloadScene()
        {
            return SceneManager.GetActiveScene();
        }
    }
}
