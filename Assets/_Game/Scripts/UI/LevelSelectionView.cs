using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        LinkEvent.Click_MainMenu.Raise();
    }
}
