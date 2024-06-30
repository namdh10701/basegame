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
        [SerializeField]
        [SpineAnimation]
        string dropdown;

        [SerializeField] StunFx stunFx;
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
            skeletonAnimation.AnimationState.SetAnimation(0, move, true);
            skeletonAnimation.AnimationState.SetAnimation(1, carry, true);
        }

        public void StopCarry()
        {

            skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
            skeletonAnimation.AnimationState.AddEmptyAnimation(1, 0, 0);
        }

        public void PlayStun()
        {
            PlayIdle();
            stunFx.Play();
        }

        public void StopStun()
        {
            stunFx.Stop();
        }

        public void PlayDropDown()
        {
            skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0);
            skeletonAnimation.AnimationState.SetAnimation(0, dropdown, true);
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
