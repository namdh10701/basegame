
using _Base.Scripts.UI;
using _Base.Scripts.UI.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class PreBattleView : View
    {
        [SerializeField] Button mapBtn;
        [SerializeField] Button homeBtn;

        private void OnEnable()
        {
            mapBtn.onClick.AddListener(OnMapBtnClick);
            homeBtn.onClick.AddListener(OnHomeBtnClick);
        }
        private void OnDisable()
        {
            mapBtn.onClick.RemoveAllListeners();
            homeBtn.onClick.RemoveAllListeners();
        }

        void OnHomeBtnClick()
        {
            LinkEvents.Click_Back.Raise();
        }
        void OnMapBtnClick()
        {
            ViewManager.Instance.Show<MapView>();   
            
        }
    }
}
