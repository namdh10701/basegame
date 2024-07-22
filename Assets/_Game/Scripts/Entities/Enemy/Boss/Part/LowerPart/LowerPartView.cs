using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class LowerPartView : PartView
    {
        [SpineAnimation] public string start;
        [SpineAnimation] public string charging;
        [SpineAnimation] public string action;
        [SpineAnimation] public string loop;
        [SpineAnimation] public string toIdle;

        protected override void OnStateEntered(PartState state)
        {
            base.OnStateEntered(state);
            if (state == PartState.Attacking)
            {
                skeletonAnim.AnimationState.SetAnimation(0, start, false);
                skeletonAnim.AnimationState.AddAnimation(0, charging, true, 0);
                skeletonAnim.AnimationState.AddAnimation(0, action, false, Random.Range(2f, 3.5f));
                skeletonAnim.AnimationState.AddAnimation(0, loop, true, 0);
            }
        }
    }
}