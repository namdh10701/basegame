using System;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;
using Screen = ZBase.UnityScreenNavigator.Core.Screens.Screen;

namespace _Game.Features.Home
{
    [Binding]
    public class MyShipScreen : RootViewModel
    {
        
        
        [Binding]
        public async void NavToEquipmentScreen()
        {
            var options = new ViewOptions("EquipmentScreen");
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
    }
}








// using System;
// using Cysharp.Threading.Tasks;
// using UnityEngine;
// using UnityWeld.Binding;
// using ZBase.UnityScreenNavigator.Core.Screens;
// using ZBase.UnityScreenNavigator.Core.Sheets;
// using ZBase.UnityScreenNavigator.Core.Views;
//
// namespace _Game.Features.Home
// {
//     [Binding]
//     public class MyShipScreen : Sheet
//     {
//         // Called just after this screen is loaded.
//         public override UniTask Initialize(Memory<object> args)
//         {
//             Debug.Log("XXX: Initialize");
//             return UniTask.CompletedTask;
//         }
//
//         public override UniTask WillEnter(Memory<object> args)
//         {
//             Debug.Log("XXX: WillEnter");
//             return UniTask.CompletedTask;
//         }
//
//         // Called just after this sheet is displayed.
//         public override void DidEnter(Memory<object> args)
//         {
//             Debug.Log("XXX: DidEnter");
//         }
//
//         // Called just before this sheet is hidden.
//         public override UniTask WillExit(Memory<object> args)
//         {
//             Debug.Log("XXX: WillExit");
//             return UniTask.CompletedTask;
//         }
//
//         // Called just after this sheet is hidden.
//         public override void DidExit(Memory<object> args)
//         {
//             Debug.Log("XXX: DidExit");
//
//         }
//
//         // Called just before this sheet is released.
//         public override UniTask Cleanup(Memory<object> args)
//         {
//             Debug.Log("XXX: Cleanup");
//             return UniTask.CompletedTask;
//         }
//         
//         [Binding]
//         public async void NavToEquipmentScreen()
//         {
//             var options = new ViewOptions("EquipmentScreen");
//             await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
//         }
//     }
// }