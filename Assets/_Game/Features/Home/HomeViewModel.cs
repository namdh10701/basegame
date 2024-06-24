using _Game.Scripts.UI;
using UnityEngine.WSA;
using UnityWeld.Binding;
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

        [Binding]
        public async void NavToInventoryScreen()
        {
            var options = new ViewOptions("InventoryScreen", false, loadAsync: false);
            await Launcher.ContainerManager.Find<ScreenContainer>(ContainerKey.Screens).PushAsync(options);
        }
    }
}