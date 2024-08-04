using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Base.Scripts.Utils
{
    public class ScreenSpaceCanvas : MonoBehaviour
    {
        Canvas canvas;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            SceneManager.activeSceneChanged += ChangedActiveScene;
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= ChangedActiveScene;
        }


        private void ChangedActiveScene(Scene arg0, Scene arg1)
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
