using UnityEngine;
using UnityEngine.UI;

namespace _Game.Common
{
    [RequireComponent(typeof(Button))]
    public class ExternalUrlButton : MonoBehaviour
    {
        private Button _button;
        public string Url;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Application.OpenURL(Url);
        }
    }
}