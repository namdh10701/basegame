using System;
using System.Collections.Generic;
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
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(false);
        }
        
        [Binding]
        public async void ShowNormalFightPopupInfo()
        {
            var options = new ViewOptions("FightNodeInfoModal", true);
            // Memory<object> args = new Memory<object>();
            // new List<object>().ToArray().AsMemory();

            int[] array = { 1, 2, 3, 4, 5 };
            var args = new object[] { array }.AsMemory();
            
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options, args);
        }
        
        [Binding]
        public async void ShowBossFightPopupInfo()
        {
            var options = new ViewOptions("FightNodeInfoModal", true);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
    }
}