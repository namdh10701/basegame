using _Game.Features.Quest;
using _Game.Scripts.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.WorldMap
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
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true);
        }
        
        [Binding]
        public async void ShowQuestPopup()
        {
            var options = new ViewOptions(nameof(QuestModal), true);
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options);
        }
    }
}