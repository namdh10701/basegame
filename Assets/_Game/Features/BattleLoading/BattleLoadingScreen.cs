using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace _Game.Features.BattleLoading
{
    public class BattleLoadingScreen : ScreenWithViewModel
    {
        // public List<SkeletonGraphic> skeletonGraphics;
        // public List<string> animationNames;
        //
        // void Start()
        // {
        //     if (skeletonGraphics == null || skeletonGraphics.Count == 0)
        //     {
        //         Debug.LogError("No SkeletonGraphics assigned.");
        //         return;
        //     }
        //
        //     if (animationNames == null || animationNames.Count != skeletonGraphics.Count)
        //     {
        //         Debug.LogError("Animation names count must match SkeletonGraphics count.");
        //         return;
        //     }
        //
        //     for (int i = 0; i < skeletonGraphics.Count; i++)
        //     {
        //         SkeletonGraphic skeletonGraphic = skeletonGraphics[i];
        //         string animationName = animationNames[i];
        //
        //         skeletonGraphic.AnimationState.Complete += OnAnimationComplete;
        //         PlayAnimation(skeletonGraphic, animationName);
        //     }
        // }
        //
        // void PlayAnimation(SkeletonGraphic skeletonGraphic, string name)
        // {
        //     skeletonGraphic.AnimationState.SetAnimation(0, name, false);
        // }
        //
        // void OnAnimationComplete(Spine.TrackEntry trackEntry)
        // {
        //     Debug.Log("Animation " + trackEntry.Animation.Name + " completed on " + trackEntry.TrackIndex);
        // }
        //
        // void OnDestroy()
        // {
        //     foreach (SkeletonGraphic skeletonGraphic in skeletonGraphics)
        //     {
        //         if (skeletonGraphic != null)
        //         {
        //             skeletonGraphic.AnimationState.Complete -= OnAnimationComplete;
        //         }
        //     }
        // }
    }
}
