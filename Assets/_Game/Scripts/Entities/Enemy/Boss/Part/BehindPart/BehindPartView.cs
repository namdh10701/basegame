using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class BehindPartView : PartView
    {
        [SpineAnimation] public string start;
        [SpineAnimation] public string charging;
        [SpineAnimation] public string action;

        [SpineAnimation] public string attackCancel;


        protected override void OnStateEntered(PartState state)
        {
            base.OnStateEntered(state);
            if (state == PartState.Attacking)
            {
                skeletonAnim.AnimationState.SetAnimation(0, start, false);
                skeletonAnim.AnimationState.AddAnimation(0, charging, true, 0);
                skeletonAnim.AnimationState.AddAnimation(0, action, false, Random.Range(1f, 2f));
            }
        }
    }
}