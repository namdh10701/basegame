using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class UpperPartView : PartView
    {
        [SpineAnimation] public string moving;
        [SpineAnimation] public string idleToMove;

        protected override void OnStateEntered(PartState state)
        {
            base.OnStateEntered(state);
            if (state == PartState.Moving)
            {
                skeletonAnim.AnimationState.SetAnimation(0, idleToMove, false);
                skeletonAnim.AnimationState.AddAnimation(0, moving, true, 0);
            }
            if (state == PartState.Attacking)
            {
                skeletonAnim.AnimationState.SetAnimation(0, attack, false);
                skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
            }
        }
    }
}