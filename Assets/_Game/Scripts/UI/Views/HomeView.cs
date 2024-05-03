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
        [SerializeField] Button craftingBtn;
        [SerializeField] Button rankingbtn;
        [SerializeField] Button crewBtn;
        [SerializeField] Button monsterPoolBtn;
        [SerializeField] Button shopBtn;
        [SerializeField] Button inventoryBtn;
        [SerializeField] Button farmBtn;
        private void OnEnable()
        {
            preBattleBtn.onClick.AddListener(OnPrebattleClick);
            craftingBtn.onClick.AddListener(OnCraftingClick);
            rankingbtn.onClick.AddListener(OnRankingClick);
            crewBtn.onClick.AddListener(OnCrewClick);
            monsterPoolBtn.onClick.AddListener(OnMonsterPoolClick);
            shopBtn.onClick.AddListener(OnShopClick);
            inventoryBtn.onClick.AddListener(OnInventoryClick);
            farmBtn.onClick.AddListener(OnFarmClick);
        }

        private void OnDisable()
        {
            preBattleBtn.onClick.RemoveListener(OnPrebattleClick);
        }

        void OnPrebattleClick()
        {
            LinkEvents.Click_PreBattle.Raise();
        }
        void OnCraftingClick()
        {
            LinkEvents.Click_PreBattle.Raise();
        }
        void OnRankingClick()
        {
            LinkEvents.Click_PreBattle.Raise();
        }
        void OnCrewClick()
        {
            LinkEvents.Click_PreBattle.Raise();
        }
        void OnMonsterPoolClick()
        {
            LinkEvents.Click_PreBattle.Raise();
        }
        void OnShopClick()
        {
            ViewManager.Instance.Show<BattleView>();
        }
        void OnInventoryClick()
        {
            ViewManager.Instance.Show<InventoryView>();
        }
        void OnFarmClick()
        {
            LinkEvents.Click_PreBattle.Raise();
        }
    }
}