using _Game.Features.GamePause;
using _Game.Features.GameSettings;
using _Game.Features.Quest;
using _Game.Features.Ranking;
using _Game.Features.Shop;
using _Game.Features.WorldMap;
using _Game.Scripts;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Online;
using Online.Enum;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Home
{
    [Binding]
    public class HomeViewModel : RootViewModel
    {
        // protected override void InitializeInternal()
        // {
        //     
        // }

        #region User profile

        [Binding]
        public string UserDisplayName => PlayfabManager.Instance.DisplayName;
        
        [Binding]
        public string UserLevel => PlayfabManager.Instance.Level.ToString().PadLeft(2, '0');

        [Binding]
        public string UserExp
        {
            get
            {
                var exp = PlayfabManager.Instance.Exp;
                var nextLevelExp = GameData.PlayerLevelTable.FindNextLevelExp(PlayfabManager.Instance.Level);
                if (nextLevelExp < 0)
                {
                    return "MAX";
                }

                return $"{exp}/{nextLevelExp}";
            }
        }

        [Binding]
        public float UserExpValue
        {
            get
            {
                if (UserExp == "MAX") return 1;

                var parts=  UserExp.Split("/");
                return 1f * int.Parse(parts[0]) / int.Parse(parts[1]);
            }
        }

        #endregion

        [Binding]
        public void ReloadUserInfo()
        {
            OnPropertyChanged(nameof(UserDisplayName));
            OnPropertyChanged(nameof(UserExp));
        }

        [Binding]
        public async void NavToWoldMapScreen()
        {
            var options = new ViewOptions(nameof(WorldMapScreen));
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }

        [Binding]
        public async void ShowQuestPopup()
        {
            var options = new ViewOptions(nameof(QuestModal));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }

        [Binding]
        public async void ShowShopGemPopup()
        {
            var options = new ViewOptions(nameof(ShopGemViewModel));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }

        [Binding]
        public async void ShowShopPopup()
        {
            var options = new ViewOptions(nameof(ShopPirateViewModel));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }

        [Binding]
        public async void ShowSettingPopup()
        {
            await GameSettingsModal.Show();
        }

        private void OnEnable()
        {
            if (HomeManager.Instance != null)
            {
                Debug.Log("RERESH 1");
                HomeManager.Instance.Refresh();
            }
        }
        [Binding]
        public async void NavToRankingScreen()
        {
            var options = new ViewOptions(nameof(RankingScreen));
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
        
        [Binding]
        public async void ShowMailBoxPopup()
        {
            await RankingTireResultConfirmModal.Show(ERank.Gunner, RankingTireResultConfirmModal.Mode.Promote);
            await RankingTireResultConfirmModal.Show(ERank.Rookie, RankingTireResultConfirmModal.Mode.Demote);
            await RankingTireResultConfirmModal.Show(ERank.Conquer, RankingTireResultConfirmModal.Mode.Remain);
            // var options = new ViewOptions(nameof(GameSettingsModal));
            // await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
        
        [Binding]
        public async void ShowDailyLoginPopup()
        {
            // var options = new ViewOptions(nameof(GameSettingsModal));
            // await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
    }
}