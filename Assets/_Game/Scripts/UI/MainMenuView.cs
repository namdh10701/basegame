using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        LinkEvent.Click_LevelSelect.Raise();
    }
    void OnShowPopupClick()
    {
        PopupManager.Instance.ShowPopup<SettingPopup>();
    }
}
