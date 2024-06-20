using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Left, Right
}
namespace _Game.Scripts
{
    public class CrewAniamtionHandler : SpineAnimationHandler
    {
        [SerializeField]
        [SpineAnimation]
        string move;

        [SerializeField]
        [SpineAnimation]
        string idle;

        [SerializeField]
        [SpineAnimation]
        string carry;
        [SerializeField]
        [SpineAnimation]
        string fix;
        public void PlayFix()
        {
            skeletonAnimation.AnimationState.SetAnimation(0, fix, true);
        }
        public void PlayMove()
        {
            skeletonAnimation.AnimationState.SetAnimation(0, move, true);
        }
        public void PlayIdle()
        {
            skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        }
        public void PlayCarry()
        {
            skeletonAnimation.AnimationState.SetAnimation(0, carry, true);
        }

        public void PlayStun()
        {
            skeletonAnimation.AnimationState.ClearTracks();
        }

        public void AddIdle()
        {
            skeletonAnimation.AnimationState.AddAnimation(0, idle, true, 0);
        }
        public void Flip(Direction direction)
        {
            if (direction == Direction.Left)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {

                transform.localScale = Vector3.one;
            }
        }
    }
}
