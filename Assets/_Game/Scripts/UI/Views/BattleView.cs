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
        public Button decreaseHpBtn;
        public Button decreaseManaBtn;
        public TextMeshProUGUI text;
        private void Awake()
        {
            ContextHolder.Context = GlobalContext.PlayerContext;
        }

        private void OnEnable()
        {
            decreaseHpBtn.onClick.AddListener(() =>
            {
                ((ShipStats)Ship.Instance.Stats).HealthPoint.Value -= 25;
            });
            decreaseManaBtn.onClick.AddListener(() =>
            {
                ((ShipStats)Ship.Instance.Stats).ManaPoint.Value -= 25;
            });
        }
        private void OnDisable()
        {
            decreaseHpBtn.onClick.RemoveAllListeners();
            decreaseManaBtn.onClick.RemoveAllListeners();
        }

        // void OnDecreaseManaClick()
        // {
        //     if (!Ship.Instance.ShipMana.ConsumeMana(25))
        //     {
        //         text.gameObject.SetActive(true);
        //     }
        //     else
        //     {
        //         text.gameObject.SetActive(false);
        //     }
        // }
    }
}