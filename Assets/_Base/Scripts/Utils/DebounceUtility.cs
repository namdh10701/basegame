using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace _Base.Scripts.Utils
{

    public static class DebounceUtility
    {
        private static CancellationTokenSource _debounceCts;

        public static void Debounce(Action action, int millisecondsDelay)
        {
            // Cancel any existing debounce operations
            _debounceCts?.Cancel();
            _debounceCts = new CancellationTokenSource();

            // Debounce the function call
            DebounceFunction(action, millisecondsDelay, _debounceCts.Token).Forget();
        }

        private static async UniTaskVoid DebounceFunction(Action action, int millisecondsDelay, CancellationToken token)
        {
            try
            {
                // Wait for the debounce delay
                await UniTask.Delay(millisecondsDelay, cancellationToken: token);

                // Perform the debounced action
                action.Invoke();
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation if needed
                // Debug.Log("Debounce operation was canceled.");
            }
        }

        public static void CancelDebounce()
        {
            // Cancel any ongoing debounce operation
            _debounceCts?.Cancel();
        }
    }
}