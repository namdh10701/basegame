using _Base.Scripts.UI;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.Managers;
using Map;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts.UI
{
    public class MapView : View
    {
        [Header("Buttons")]
        [SerializeField] Button abandoneRunBtn;
        private void OnEnable()
        {
            abandoneRunBtn.onClick.AddListener(OnAbadoneRunClick);
        }

        private void OnDisable()
        {
            abandoneRunBtn.onClick.RemoveAllListeners();
        }

        void OnAbadoneRunClick()
        {
            ViewManager.Instance.Show<PreBattleView>();
            GameManager.Instance.MapManager.GenerateNewMap();
        }
    }
}