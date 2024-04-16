using _Base.Scripts.UI;
using _Base.Scripts.UI.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class HomeView : View
    {
        [Header("Buttons")]
        [SerializeField] Button preBattleBtn;

        private void OnEnable()
        {
            preBattleBtn.onClick.AddListener(OnPrebattleClick);
        }

        private void OnDisable()
        {
            preBattleBtn.onClick.RemoveAllListeners();
        }

        void OnPrebattleClick()
        {
            LinkEvents.Click_PreBattle.Raise();
        }
    }
}