using UnityEngine;
using Spine.Unity;
using System;
using Spine;
public class SpineAnimationEnemyHandler : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    void Start()
    {
        skeletonAnimation.AnimationState.End += delegate (TrackEntry trackEntry)
        {
            switch (trackEntry.Animation.Name)
            {
                case "action":
                    Debug.Log("Play aninm Idle");
                    break;
            }
        };
    }

    public void PlayAttackAnim(bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "action", isLoop);
        Debug.Log("Play PlayAttackAnim");

        //Idle
    }

    public void PlayIdleAnim(bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "spineShoot", isLoop);
        //Idle
    }

    public void PlayMoveAnim(bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "spineShoot", isLoop);
        //Idle
    }

    public void PlayAnim(string name, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, name, isLoop);
    }
}
