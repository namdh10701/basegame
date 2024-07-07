using System;
using System.Linq;
using _Game.Features.Battle;
using _Game.Features.BattleLoading;
using _Game.Features.Home;
using _Game.Features.Inventory;
using _Game.Scripts.Gameplay;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.FightNodeInfoPopup
{
    [Binding]
    public class FightNodeInfoModal : ModalWithViewModel
    {
        private string _stageId;
        public override async UniTask Initialize(Memory<object> args)
        {
            _stageId = args.ToArray().FirstOrDefault() as string;

            if (string.IsNullOrEmpty(_stageId))
            {
                return;
            }
            
            Items.Clear();
            for (int i = 0; i < 7; i++)
            {
                Items.Add(new InventoryItem { Type = ItemType.CREW });
            }
        }
        
        public enum Style
        {
            Normal,
            Boss
        }

        public Style style = Style.Normal;

        [Binding] 
        public bool IsBossStyle => style == Style.Boss;
        
        #region Binding: Items

        private ObservableList<InventoryItem> items = new ObservableList<InventoryItem>();

        [Binding]
        public ObservableList<InventoryItem> Items => items;

        #endregion


        [Binding]
        public async void NavToBattle()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
            var screenContainer = ScreenContainer.Find(ContainerKey.Screens);
            
            // TODO load data here
            // var data = load(_stageId)
            
            await screenContainer.PushAsync(new 
                ScreenOptions(nameof(BattleLoadingScreen), stack: false));
            
            await UniTask.Delay(500);
            
            await screenContainer.PushAsync(
                new ScreenOptions(nameof(BattleScreen), stack: false));

        }

        [Binding]
        public async void NavToMyShip()
        {
            ScreenContainer.Find(ContainerKey.Screens).PushAsync(
                new ScreenOptions(nameof(MainScreen), false));
            MainViewModel.Instance.ActiveMainNavIndex = (int)MainViewModel.Nav.SHIP;
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }
    }
}