using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Features.Inventory;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.NodeClear
{
    [Binding]
    public class NodeClearViewModel : RootViewModel
    {
        [Binding]
        public async void NavToQuest()
        {
            var options = new ModalOptions("QuestPopup", true);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
        
        [Binding]
        public async void NavToRelic()
        {
            var options = new ScreenOptions("RelicScreen", true);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
        
        [Binding]
        public async void NavToGear()
        {
            var options = new ScreenOptions("GearScreen", true);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
        
        [Binding]
        public async void Quit()
        {
            var options = new ScreenOptions("MainScreen", true, stack:false);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
        
        [Binding]
        public async void Continue()
        {
            // var options = new ScreenOptions("MyShipScreen", true);
            // await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
    }
}