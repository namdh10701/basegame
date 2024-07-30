using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Dialogs
{
    [Binding]
    public abstract class AsyncModal<TValue, TModalParams>: ModalWithViewModel where TModalParams: class
    {
        private TaskCompletionSource<TValue> _signal;
        private TModalParams _params;

        public TModalParams Params
        {
            get => _params;
            set => _params = value;
        }

        public async Task Close(TValue value = default)
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
            _signal.TrySetResult(value);
        }
        
        public override async UniTask Initialize(Memory<object> args)
        {
            _signal = new TaskCompletionSource<TValue>();
            Params = args.Span[0] as TModalParams;
            await InternalInitialize(Params);
        }

        protected abstract UniTask InternalInitialize(TModalParams args);

        public async Task<TValue> WaitForComplete()
        {
            
            return await _signal.Task;
        }

        public static async Task<TValue> Show<T>(TModalParams args)
        {
            var modalContainer = ModalContainer.Find(ContainerKey.Modals);
            // var resourcePath = await GetCallerClassName();
            var options = new ViewOptions(typeof(T).Name);
            await modalContainer.PushAsync(options, args);

            var modal = modalContainer.Current.View as AsyncModal<TValue, TModalParams>;
            return await modal!.WaitForComplete();
        }
    }
}