using _Game.Scripts.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Harbor
{
    [Binding]
    public class HarborViewModel : RootViewModel
    {
        // protected override void InitializeInternal()
        // {
        //     
        // }

        [Binding]
        public async void NavToTalentTreeScreen()
        {
            var options = new ViewOptions("TalentTreeScreen", false, loadAsync: false);
            
            await ScreenContainer.Of(transform).PushAsync(options);
        }

        [Binding]
        public async void NavToMergeScreen()
        {
            var options = new ViewOptions("MergeScreen", false, loadAsync: false);
            
            await ScreenContainer.Of(transform).PushAsync(options);
        }
    }
}