using _Game.Features.GamePause;
using _Game.Features.Quest;
using _Game.Features.Shop;
using _Game.Features.WorldMap;
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
            var options = new ViewOptions(nameof(WorldMapScreen));
            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }

        [Binding]
        public async void ShowQuestPopup()
        {
            var options = new ViewOptions(nameof(QuestModal));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }

        [Binding]
        public async void ShowShopGemPopup()
        {
            var options = new ViewOptions(nameof(ShopGemViewModel));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }

        [Binding]
        public async void ShowSettingPopup()
        {
            var options = new ViewOptions(nameof(GameSettingsModal));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
    }
}