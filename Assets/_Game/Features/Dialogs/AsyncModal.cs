using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core;
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

        public static async Task<TValue> Show<T>([CanBeNull] TModalParams args = default, AsyncModalOptions? modalOptions = null)
        {
            var modalContainer = ModalContainer.Find(ContainerKey.Modals);
            // var resourcePath = await GetCallerClassName();
            // var options = modalOptions ?? new ModalOptions();
            // options.options.res
            var options = new ModalOptions(typeof(T).Name);
            if (modalOptions.HasValue)
            {
                options = new ModalOptions(
                    typeof(T).Name, 
                    modalOptions.Value.options.playAnimation,
                    modalOptions.Value.options.onLoaded,
                    modalOptions.Value.options.loadAsync,
                    modalOptions.Value.backdropAlpha,
                    modalOptions.Value.closeWhenClickOnBackdrop,
                    modalOptions.Value.modalBackdropResourcePath,
                    modalOptions.Value.options.poolingPolicy
                );
            }
            
            await modalContainer.PushAsync(options, args);

            var modal = modalContainer.Current.View as AsyncModal<TValue, TModalParams>;
            return await modal!.WaitForComplete();
        }
    }
        
        public readonly struct AsyncModalOptions
        {
            public readonly float? backdropAlpha;
            public readonly bool? closeWhenClickOnBackdrop;
            public readonly string modalBackdropResourcePath;
            public readonly AsyncViewOptions options;

            public AsyncModalOptions(
                in AsyncViewOptions options
                , in float? backdropAlpha = null
                , in bool? closeWhenClickOnBackdrop = null
                , string modalBackdropResourcePath = null
            )
            {
                this.options = options;
                this.backdropAlpha = backdropAlpha;
                this.closeWhenClickOnBackdrop = closeWhenClickOnBackdrop;
                this.modalBackdropResourcePath = modalBackdropResourcePath;
            }

            public AsyncModalOptions(
                bool playAnimation = true
                , OnViewLoadedCallback onLoaded = null
                , bool loadAsync = true
                , in float? backdropAlpha = null
                , in bool? closeWhenClickOnBackdrop = null
                , string modalBackdropResourcePath = null
                , PoolingPolicy poolingPolicy = PoolingPolicy.UseSettings
            )
            {
                this.options = new(playAnimation, onLoaded, loadAsync, poolingPolicy);
                this.backdropAlpha = backdropAlpha;
                this.closeWhenClickOnBackdrop = closeWhenClickOnBackdrop;
                this.modalBackdropResourcePath = modalBackdropResourcePath;
            }

            public static implicit operator AsyncModalOptions(in AsyncViewOptions options)
                => new(options);

            public static implicit operator AsyncViewOptions(in AsyncModalOptions options)
                => options.options;
        }
        
        public readonly struct AsyncViewOptions
        {
            public readonly bool loadAsync;
            public readonly bool playAnimation;
            public readonly PoolingPolicy poolingPolicy;
            public readonly OnViewLoadedCallback onLoaded;

            public AsyncViewOptions(
                bool playAnimation = true
                , OnViewLoadedCallback onLoaded = null
                , bool loadAsync = true
                , PoolingPolicy poolingPolicy = PoolingPolicy.UseSettings
            )
            {
                this.loadAsync = loadAsync;
                this.playAnimation = playAnimation;
                this.poolingPolicy = poolingPolicy;
                this.onLoaded = onLoaded;
            }
        }
}