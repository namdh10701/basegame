using UnityEngine;
using Spine.Unity;
using Spine;
using System;
using UnityEngine.Events;
using _Game.Scripts.Entities;
using _Game.Scripts;
using System.Collections.Generic;
using Unity.VisualScripting;
public enum Anim
{
    Idle, Dead, Attack, Hide, Appear
}
[Serializable]
public struct AnimName
{
    public string name;
    public Anim anim;
}
public abstract class SpineAnimationEnemyHandler : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    [SpineAnimation] public string idle;
    [SpineAnimation] public string dead;
    Action onDead;
    protected virtual void Start()
    {
        skeletonAnimation.AnimationState.Event += AnimationState_Event;
        skeletonAnimation.AnimationState.Complete += AnimationState_Complete;
    }

    protected abstract void AnimationState_Event(TrackEntry trackEntry, Spine.Event e);
    protected virtual void AnimationState_Complete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == dead)
        {
            onDead?.Invoke();
            onDead = null;
        }
        if (!trackEntry.Loop && trackEntry.Next == null)
            PlayIdle();
    }

    public void PlayIdle()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
    }

    public void PlayDie(Action onAnimCompleted = null)
    {
        onDead = onAnimCompleted;
        skeletonAnimation.AnimationState.SetAnimation(0, dead, false);
    }

    public void PlayAnim(string animName, bool isLoop, Action onAnimCompleted = null)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, animName, isLoop);

    }

    private void OnDestroy()
    {
        skeletonAnimation.AnimationState.Event -= AnimationState_Event;
        skeletonAnimation.AnimationState.Complete -= AnimationState_Complete;
    }

}
