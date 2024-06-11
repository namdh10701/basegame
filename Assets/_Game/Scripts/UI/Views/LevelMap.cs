using _Base.Scripts.UI;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.Managers;
using Map;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts.UI
{
    public class LevelMap : View
    {
        [Header("Buttons")]
        [SerializeField] Button _btnBackToPreBattle;
        private void OnEnable()
        {
            _btnBackToPreBattle.onClick.AddListener(OnAbadoneRunClick);
        }

        private void OnDisable()
        {
            _btnBackToPreBattle.onClick.RemoveAllListeners();
        }

        void OnAbadoneRunClick()
        {
            ViewManager.Instance.Show<PreBattleView>();
            GameManager.Instance.MapManager.GenerateNewMap();
        }
    }
}