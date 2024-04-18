
using _Base.Scripts.UI;
using UnityEngine.UI;
using UnityEngine;
using _Base.Scripts.UI.Managers;
namespace _Game.Scripts.UI
{
    public class EndBattleView : View
    {
        [SerializeField] Button mapBtn;

        private void OnEnable()
        {
            mapBtn.onClick.AddListener(OnMapBtnClick);
        }

        private void OnDisable()
        {
            mapBtn.onClick.RemoveAllListeners();
        }

        void OnMapBtnClick()
        {
            LinkEvents.Click_PreBattle.Raise();
        }


    }
}
