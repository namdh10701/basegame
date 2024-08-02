using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Dialogs
{
    [Binding]
    public class AlertModal: AsyncModal<bool, string>
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
        
        protected override UniTask InternalInitialize(string args)
        {
            Message = args;
            return UniTask.CompletedTask;
        }
        
        public static async Task<bool> Show(string alertMessage)
        {
            return await Show<AlertModal>(alertMessage);
        }
    }
}