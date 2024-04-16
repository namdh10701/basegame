using _Base.Scripts.EventSystem;
using _Base.Scripts.UI;
using _Game.Scripts.GameContext;
using _Game.Scripts.Gameplay.Ship;
using Slash.Unity.DataBind.Core.Presentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts.UI
{
    public class BattleView : View
    {
        public ContextHolder ContextHolder;
        public Button decreaseManaBtn;
        public TextMeshProUGUI text;
        private void Awake()
        {
            ContextHolder.Context = GlobalContext.PlayerContext;
        }

        private void OnEnable()
        {
            decreaseManaBtn.onClick.AddListener(OnDecreaseManaClick);
        }
        private void OnDisable()
        {
            decreaseManaBtn.onClick.RemoveAllListeners();
        }

        void OnDecreaseManaClick()
        {
            if (!Ship.Instance.ShipMana.ConsumeMana(25))
            {
                text.gameObject.SetActive(true);
            }
            else
            {
                text.gameObject.SetActive(false);
            }
        }
    }
}