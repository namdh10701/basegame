using _Base.Scripts.UI.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Scripts
{
    public class ResetScene : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(OnButtonClick);
        }

        void OnButtonClick()
        {
            ViewManager.Instance.ResetScene();
        }


    }
}
