using System.Threading.Tasks;
using _Game.Features.Dialogs;
using Online;
using UnityWeld.Binding;

namespace _Game.Features.GameSettings
{
    [Binding]
    public class LinkAccountModal : AsyncModal
    {
        public static async Task Show()
        {
            await Show<LinkAccountModal>();
        }
        
        [Binding]
        public async void OnClickSignInWithFacebook()
        {
            await PlayfabManager.Instance.LinkFacebook();
        }
        
        [Binding]
        public async void OnClickSignInWithGoogle()
        {
            
        }
        
        [Binding]
        public async void OnClickSignInWithApple()
        {
            
        }
    }
}