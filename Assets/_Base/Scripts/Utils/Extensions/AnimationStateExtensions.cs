using System.Threading;
using Cysharp.Threading.Tasks;
using Spine;

namespace _Base.Scripts.Utils.Extensions
{
    public static class AnimationStateExtensions
    {
        public static async UniTask SetAnimationAsync(this AnimationState animationState, int trackIndex, string animationName, bool loop, CancellationToken cancellationToken = default)
        {
            var tcs = new UniTaskCompletionSource();

            // Set the animation
            var trackEntry = animationState.SetAnimation(trackIndex, animationName, loop);

            // Define a handler to complete the task when the animation ends
            AnimationState.TrackEntryDelegate onEnd = null;
            onEnd = entry =>
            {
                if (entry == trackEntry && !loop)
                {
                    animationState.End -= onEnd;
                    tcs.TrySetResult();
                }
            };

            // Attach the handler to the animation end event
            animationState.End += onEnd;

            // Support for cancellation
            cancellationToken.Register(() =>
            {
                animationState.End -= onEnd;
                // Pause the animation by setting the time scale to 0
                trackEntry.TimeScale = 0;
                tcs.TrySetCanceled();
            });

            try
            {
                await tcs.Task;
            }
            finally
            {
                // Ensure the handler is removed if the task is completed or canceled
                animationState.End -= onEnd;
            }
        }
    }
}