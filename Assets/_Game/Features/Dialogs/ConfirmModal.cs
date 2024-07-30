using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Dialogs
{
    [Binding]
    public class ConfirmModal: AsyncModal<bool, string>
    {

        #region Binding Prop: Message

        /// <summary>
        /// Message
        /// </summary>
        [Binding]
        public string Message
        {
            get => _message;
            set
            {
                if (Equals(_message, value))
                {
                    return;
                }

                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private string _message;

        #endregion

        [Binding]
        public async void OnOk()
        {
            await Close(true);
        }
        
        [Binding]
        public async void OnCancel()
        {
            await Close(false);
        }
        
        protected override UniTask InternalInitialize(string args)
        {
            Message = args;
            return UniTask.CompletedTask;
        }
        
        public static async Task<bool> Show(string confirmMessage)
        {
            return await Show<ConfirmModal>(confirmMessage);
        }
    }
}