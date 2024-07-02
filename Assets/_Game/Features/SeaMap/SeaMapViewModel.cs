using System;
using System.Collections.Generic;
using _Game.Features.Battle;
using _Game.Features.FightNodeInfoPopup;
using _Game.Features.Home;
using _Game.Features.Quest;
using _Game.Scripts.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.SeaMap
{
    [Binding]
    public class SeaMapViewModel : RootViewModel
    {
        [Binding]
        public async void NavBack()
        {
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true);
        }

        [Binding]
        public async void ShowNormalFightPopupInfo()
        {
            var options = new ViewOptions(nameof(FightNodeInfoModal), true);
            // Memory<object> args = new Memory<object>();
            // new List<object>().ToArray().AsMemory();

            int[] array = { 1, 2, 3, 4, 5 };
            var args = new object[] { array }.AsMemory();

            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options, args);
        }

        [Binding]
        public async void ShowBossFightPopupInfo()
        {
            var options = new ViewOptions(nameof(FightNodeInfoModal));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
        
        [Binding]
        public async void ShowQuestPopup()
        {
            var options = new ViewOptions(nameof(QuestModal));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
        
        [Binding]
        public async void NavToWorldMap()
        {
            var options = new ViewOptions(nameof(WorldMapScreen));
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
        
        [Binding]
        public async void NavToHome()
        {
            var options = new ViewOptions(nameof(MainScreen));
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }

        [Binding]
        public async void NavBattleScene()
        {
            var options = new ViewOptions(nameof(BattleScreen));
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
    }
}