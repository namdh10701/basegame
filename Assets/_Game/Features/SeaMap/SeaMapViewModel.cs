using _Game.Scripts.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.SeaMap
{
    [Binding]
    public class SeaMapViewModel : SubViewModel
    {
        [Binding]
        public async void NavBack()
        {
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(false);
        }
    }
}