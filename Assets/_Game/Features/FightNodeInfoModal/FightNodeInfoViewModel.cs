using System.Threading.Tasks;
using _Game.Features.MyShipScreen;
using _Game.Features.Inventory;
using _Game.Scripts.Gameplay;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.FightNodeInfo
{
    [Binding]
    public class FightNodeInfoViewModel : RootViewModel
    {
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

        public async Task Init()
        {

            Items.Clear();
            for (int i = 0; i < 7; i++)
            {
                Items.Add(new InventoryItem { Type = ItemType.CREW });
            }
        }

        [Binding]
        public async void NavToBattle()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);  

            var options = new ScreenOptions("BattleLoadingScreen", true, stack: false);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
            await UniTask.Delay(500);
            options = new ScreenOptions("BattleScreen", true, stack: false);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);

        }

        [Binding]
        public async void NavToMyShip()
        {
            ScreenContainer.Find(ContainerKey.Screens).PushAsync(new ScreenOptions(nameof(MainScreen), false));
            MainViewModel.Instance.ActiveMainNavIndex = (int)MainViewModel.Nav.SHIP;
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }
    }
}