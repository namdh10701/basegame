using _Game.Scripts;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineAnimationCannonHandler : SpineAnimationHandler
{
    [SpineAnimation] public string spineShoot;
    [SpineEvent] public string shootEvent;
    public Action OnShoot;
    public SkeletonAnimation shellAnimation;
    public SkeletonAnimation muzzleAnimation;

    bool isLeft;
    public void PlayShootAnim(bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, spineShoot, isLoop);
    }
    protected virtual void Start()
    {
        skeletonAnimation.AnimationState.Event += AnimationState_Event;
    }

    public void PlayChargeAnim()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "begin_charge", false);
        skeletonAnimation.AnimationState.AddAnimation(0, "charge", true, 0);
    }

    protected void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == shootEvent)
        {
            OnShoot?.Invoke();
            isLeft = !isLeft;
            if (isLeft) shellAnimation.AnimationState.SetAnimation(0, "shell_left", false);
            else shellAnimation.AnimationState.SetAnimation(0, "shell_right", false);
            muzzleAnimation?.AnimationState.AddAnimation(0, "demo_fireeffect", false, .15f);
            muzzleAnimation?.AnimationState.AddEmptyAnimation(0, 0f, 0);
        }
    }


}
