using _Game.Scripts.Utils;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class BodyView : PartView
    {
        [SpineAnimation] public string shake;
        [SpineAnimation] public string shakeToIdle;
        public override void PlayEntry()
        {
            gameObject.SetActive(true);
            skeletonAnim.AnimationState.SetAnimation(0, entry, false);
            skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
        }

        public void PlayShake()
        {
            skeletonAnim.AnimationState.SetAnimation(0, shake, true);
        }

        public void StopShake()
        {
            skeletonAnim.AnimationState.SetAnimation(0, shakeToIdle, true);
            skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
        }
    }
}