using _Base.Scripts.UI;
using _Base.Scripts.UI.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class MainMenuView : View
    {
        [SerializeField] Button levelSelectBtn;

        [SerializeField] Button showPopupBtn;
        private void OnEnable()
        {
            levelSelectBtn.onClick.AddListener(OnLevelSelectBtnClick);
            showPopupBtn.onClick.AddListener(OnShowPopupClick);
        }
        private void OnDisable()
        {
            levelSelectBtn.onClick.RemoveListener(OnLevelSelectBtnClick);
            showPopupBtn.onClick.RemoveListener(OnShowPopupClick);
        }

        void OnLevelSelectBtnClick()
        {
            LinkEvents.Click_LevelSelect.Raise();
        }
        void OnShowPopupClick()
        {
            PopupManager.Instance.ShowPopup<SettingPopup>();
        }
    }
}
