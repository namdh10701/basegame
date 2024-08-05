using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Dialogs
{
    public sealed class Void
    {
        private Void() { }
    }

    [Binding]
    public abstract class AsyncModal : AsyncModal<Void, Void>
    {
    }
    
    [Binding]
    public abstract class AsyncModal<TValue, TModalParams>: ModalWithViewModel// where TModalParams: class
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

        [Binding]
        public async Task DoClose()
        {
            await Close();
        }
        
        public sealed override async UniTask Initialize(Memory<object> args)
        {
            _signal = new TaskCompletionSource<TValue>();
            Params = args.Span[0] is TModalParams ? (TModalParams)args.Span[0] : default;
            await InternalInitialize(Params);
        }

        protected virtual UniTask InternalInitialize(TModalParams args)
        {
            return UniTask.CompletedTask;
        }

        public async Task<TValue> WaitForComplete()
        {
            
            return await _signal.Task;
        }

        public static async Task<TValue> Show<T>([CanBeNull] TModalParams args = default)
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