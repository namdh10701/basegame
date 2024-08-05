using System.Collections.Generic;
using System.Threading.Tasks;
using Spine.Unity;
using UnityEngine;

namespace _Game.Features.BattleLoading
{
    public class BattleLoadingScreen : ScreenWithViewModel
    {
        public SkeletonGraphic TopWave;
        public SkeletonGraphic BotSticker;

        void Start()
        {
            if (TopWave == null)
            {
                Debug.LogError("TopWave is not assigned.");
                return;
            }

            if (BotSticker == null)
            {
                Debug.LogError("BotSticker is not assigned.");
                return;
            }
            TopWave.AnimationState.SetAnimation(0, "begin", false);
            var topWaveAnimation = TopWave.AnimationState.SetAnimation(0, "begin", false);
            if (topWaveAnimation == null)
            {
                Debug.LogError("TopWave animation 'begin_songbien' not found.");
                return;
            }

            topWaveAnimation.Complete += (trackEntry) =>
            {
                Debug.Log("TopWave run");
                TopWave.AnimationState.SetAnimation(0, "loop", true);
            };

            var botStickerAnimation = BotSticker.AnimationState.SetAnimation(0, "begin", false);
            if (botStickerAnimation == null)
            {
                Debug.LogError("BotSticker animation 'begin' not found.");
                return;
            }

            botStickerAnimation.Complete += (trackEntry) =>
            {
                var loopEntry = BotSticker.AnimationState.SetAnimation(0, "loop", true);
                loopEntry.Complete += (loopTrackEntry) =>
                {
                    BotSticker.AnimationState.SetAnimation(0, "end", false);
                };
            };
        }

        public Task StopLoopAndPlayEnd()
        {
            var signal = new TaskCompletionSource<bool>();

            if (TopWave == null || BotSticker == null)
            {
                Debug.LogError("TopWave or BotSticker is not assigned.");
                signal.TrySetResult(false);
                return signal.Task;
            }

            var loopEntry = BotSticker.AnimationState.SetAnimation(0, "end", false);
            loopEntry.Complete += (loopTrackEntry) =>
            {
                signal.TrySetResult(true);
            };

            return signal.Task;
        }
    }
}
