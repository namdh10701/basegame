using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineEvent : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    private void OnEnable()
    {
        skeletonAnimation.AnimationState.Event += HandleEvent;
        skeletonAnimation.AnimationState.Start += OnAnimStart;
        skeletonAnimation.AnimationState.End += OnAnimEnd;
        skeletonAnimation.AnimationState.Complete += OnAnimCompleted;
    }

    private void OnDisable()
    {
        skeletonAnimation.AnimationState.Event -= HandleEvent;
        skeletonAnimation.AnimationState.Start -= OnAnimStart;
        skeletonAnimation.AnimationState.End -= OnAnimEnd;
        skeletonAnimation.AnimationState.Complete -= OnAnimCompleted;
    }

    private void OnAnimCompleted(TrackEntry trackEntry)
    {
        throw new NotImplementedException();
    }

    private void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        throw new NotImplementedException();
    }
    private void OnAnimEnd(TrackEntry trackEntry)
    {
        throw new NotImplementedException();
    }

    private void OnAnimStart(TrackEntry trackEntry)
    {
        throw new NotImplementedException();
    }
}
