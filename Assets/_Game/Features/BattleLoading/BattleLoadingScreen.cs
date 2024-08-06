using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Spine.Unity;

namespace _Game.Features.BattleLoading
{
    public class BattleLoadingScreen : ScreenWithViewModel
    {
        public SkeletonGraphic TopWave;
        public SkeletonGraphic BotSticker;

        void Start()
        {
            TopWave.AnimationState.SetAnimation(0, "begin_songbien", false).Complete += (trackEntry) =>
            {
                TopWave.AnimationState.SetAnimation(0, "loop_songbien", true);
            };
            
            BotSticker.AnimationState.SetAnimation(0, "begin", false).Complete += (trackEntry) =>
            {
                BotSticker.AnimationState.SetAnimation(0, "loop", true);
            };
        }

        public Task StopLoopAndPlayEnd()
        {
            var signal = new TaskCompletionSource<bool>();

            BotSticker.AnimationState.SetAnimation(0, "end", false);
            UniTask.RunOnThreadPool(async () =>
            {
                await UniTask.Delay(6500);
                TopWave.AnimationState.SetAnimation(0, "end_songbien", false).Complete += (trackEntry) =>
                {
                    signal.TrySetResult(true);
                };
            });

            return signal.Task;
        }
    }
}
