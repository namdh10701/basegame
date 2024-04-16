using _Base.Scripts.StateMachine;
using _Base.Scripts.UI;
using _Game.Scripts;
using Map;
using UnityEngine;
using UnityEngine.UI;
public class LevelInfoPopup : Popup
{
    [SerializeField] Button playBtn;

    private void OnDisable()
    {
        playBtn.onClick.RemoveAllListeners();
    }

    private void OnEnable()
    {
        playBtn.onClick.AddListener(OnPlayClick);
    }

    void OnPlayClick()
    {
        LinkEvents.Click_Play.Raise();
    }
    public void SetData(MapNode mapNode)
    {

    }
}
