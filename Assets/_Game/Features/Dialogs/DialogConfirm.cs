using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Dialogs
{
    [Binding]
    public class DialogConfirm: ModalWithViewModel
    {
        private TaskCompletionSource<bool> signal;

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

        public async Task Close(bool value = false)
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
            signal.TrySetResult(value);
        }
        
        public override UniTask Initialize(Memory<object> args)
        {
            signal = args.ToArray()[0] as TaskCompletionSource<bool>;
            Message = args.ToArray()[1].ToString();
            return UniTask.CompletedTask;
        }
        
        public static async Task<bool> Show(string confirmMessage)
        {
            TaskCompletionSource<bool> signal = new();
            var options = new ViewOptions(nameof(DialogConfirm));
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(options, signal, confirmMessage);

            return await signal.Task;
        }
    }
}