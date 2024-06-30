using _Game.Scripts.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Home
{
    [Binding]
    public class WorldMapViewModel : RootViewModel
    {
        // protected override void InitializeInternal()
        // {
        //     
        // }

        [Binding]
        public async void NavBack()
        {
            // var options = new ViewOptions("MainScreen", false, loadAsync: false);
            
            // await ScreenContainer.Of(transform).PushAsync(options);
            await ScreenContainer.Of(transform).PopAsync(false);
            // await Launcher.ContainerManager.Find<ScreenContainer>(ContainerKey.Screens).PushAsync(options);
        }
        
        [Binding]
        public async void ShowQuestPopup()
        {
            var options = new ViewOptions("QuestModal", true);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
    }
}