using _Base.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class LevelSelectionView : View
    {
        [SerializeField] Button mainMenuBtn;

        private void OnEnable()
        {
            mainMenuBtn.onClick.AddListener(OnMainMenuClick);
        }
        private void OnDisable()
        {
            mainMenuBtn.onClick.RemoveListener(OnMainMenuClick);
        }
        void OnMainMenuClick()
        {
            LinkEvents.Click_MainMenu.Raise();
        }
    }
}
