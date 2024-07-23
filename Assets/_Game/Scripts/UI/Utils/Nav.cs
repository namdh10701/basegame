using System;
using System.Threading.Tasks;
using _Game.Features;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Scripts.UI.Utils
{
    public class Nav
    {
        public static async Task ShowModal<T>(params object[] args)
        {
            var options = new ViewOptions(typeof(T).Name);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options, args);
        }
        
        public static async Task ShowScreen<T>(params object[] args)
        {
            var options = new ViewOptions(typeof(T).Name);
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options, args);
        }
        //
        // public static async void ShowSheet<T>(params object[] args)
        // {
        //     var options = new ViewOptions(typeof(T).Name);
        //     await SheetContainer.Find(ContainerKey.Activities).PushAsync(options, args);
        // }
    }
}