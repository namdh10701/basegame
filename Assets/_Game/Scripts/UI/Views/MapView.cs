using _Base.Scripts.UI;
using _Base.Scripts.UI.Managers;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts.UI
{
    public class MapView : View
    {
        [Header("Buttons")]
        [SerializeField] Button backToPreBattleBtn;

        private void OnEnable()
        {
            backToPreBattleBtn.onClick.AddListener(OnBackToPreBattleClick);
        }

        private void OnDisable()
        {
            backToPreBattleBtn.onClick.RemoveAllListeners();
        }

        void OnBackToPreBattleClick()
        {
            ViewManager.Instance.Show<PreBattleView>();
        }
    }
}