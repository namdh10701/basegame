using System.Threading.Tasks;
using _Game.Features;
using ZBase.UnityScreenNavigator.Core;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Scripts.UI.Utils
{
    public class Nav
    {
        private static ScreenContainer SC => ScreenContainer.Find(ContainerKey.Screens);
        private static ModalContainer MC => ModalContainer.Find(ContainerKey.Modals);
        public static async Task<T> ShowModalAsync<T>(params object[] args) where T : class
        {
            var options = new ViewOptions(typeof(T).Name);
            await MC.PushAsync(options, args);
            return MC.Current.View as T;
        }
        
        public static async Task PreloadScreenAsync<T>() where T : class
        {
            await SC.PreloadAsync(typeof(T).Name);
        }
        
        public static async Task<T> ShowScreenAsync<T>(bool playAnimation = true, bool preload = false, PoolingPolicy poolingPolicy = PoolingPolicy.UseSettings, params object[] args) where T : class
        {
            var options = new ViewOptions(typeof(T).Name, playAnimation, poolingPolicy: poolingPolicy);
            if (preload)
            {
                await SC.PreloadAsync(typeof(T).Name);
            }
            await SC.PushAsync(options, args);
            return SC.Current.View as T;
        }
        
        public static async Task PopCurrentScreenAsync(bool playAnimation = true)
        {
            await SC.PopAsync(playAnimation);
        }
        
        public static async Task PopCurrentPopupAsync(bool playAnimation = true)
        {
            await SC.PopAsync(playAnimation);
        }
        
        //
        // public static async void ShowSheet<T>(params object[] args)
        // {
        //     var options = new ViewOptions(typeof(T).Name);
        //     await SheetContainer.Find(ContainerKey.Activities).PushAsync(options, args);
        // }
    }
}