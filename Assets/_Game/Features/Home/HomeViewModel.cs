using _Game.Scripts.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
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
        public async void NavToWoldMapScreen()
        {
            var options = new ViewOptions("WorldMapScreen", false, loadAsync: false);
            
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
            // await Launcher.ContainerManager.Find<ScreenContainer>(ContainerKey.Screens).PushAsync(options);
        }
        
        [Binding]
        public async void ShowQuestPopup()
        {
            var options = new ViewOptions("QuestModal", true);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
        
        [Binding]
        public async void ShowSettingPopup()
        {
            var options = new ViewOptions("SettingModal", true);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
    }
}