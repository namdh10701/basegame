using UnityEngine;
using Spine.Unity;
using Spine;
using System;
using UnityEngine.Events;
using _Game.Scripts.Entities;
using _Game.Scripts;
using System.Collections.Generic;
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
public class SpineAnimationEnemyHandler : MonoBehaviour
{
    [SerializeField] AnimName[] animNames;
    Dictionary<Anim, AnimName> animDic = new Dictionary<Anim, AnimName>();
    public SkeletonAnimation skeletonAnimation;
    public Action onHideCompleted;
    public Action onShowCompleted;
    public Action onDeadCompleted;
    public UnityEvent OnShoot;

    protected virtual void Start()
    {
        foreach (AnimName an in animNames)
        {
            animDic.Add(an.anim, an);
        }
        PlayAnim(Anim.Idle, true);
        skeletonAnimation.AnimationState.Event += AnimationState_Event;
        skeletonAnimation.AnimationState.Complete += AnimationState_Complete;
    }

    private void AnimationState_Complete(TrackEntry trackEntry)
    {
        switch (trackEntry.Animation.Name)
        {
            case "action":
                PlayAnim(Anim.Idle, true);
                break;
            case "appear":
                PlayAnim(Anim.Idle, true);
                onShowCompleted?.Invoke();
                onShowCompleted = null;
                break;
            case "hide":
                onHideCompleted?.Invoke();
                onHideCompleted = null;
                break;
            case "die":
            case "dead":
                onDeadCompleted?.Invoke();
                break;
        }
    }

    public void PlayAnim(Anim anim, bool isLoop, Action onCompleted = null)
    {
        switch (anim)
        {
            case Anim.Idle:
                skeletonAnimation.AnimationState.SetAnimation(0, animDic[Anim.Idle].name, isLoop);
                break;
            case Anim.Attack:
                skeletonAnimation.AnimationState.SetAnimation(0, animDic[Anim.Attack].name, isLoop);
                break;
            case Anim.Dead:
                skeletonAnimation.AnimationState.SetAnimation(0, animDic[Anim.Dead].name, isLoop);
                onDeadCompleted = onCompleted;
                break;
            case Anim.Hide:
                skeletonAnimation.AnimationState.SetAnimation(0, animDic[Anim.Hide].name, isLoop);
                onHideCompleted = onCompleted;
                break;
            case Anim.Appear:
                skeletonAnimation.AnimationState.SetAnimation(0, animDic[Anim.Appear].name, isLoop);
                onShowCompleted = onCompleted;
                break;

        }
    }
    private void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
    {
        if (trackEntry.Animation.Name == "action")
        {
            if (e.Data.Name == "begin")
            {
                OnShoot.Invoke();
            }
        }
    }

    public void PlayAnim(string animName, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, animName, isLoop);
    }
    
    public void PlayAnimSequence(string anim1, string anim2, bool isLoop)
    {
        skeletonAnimation.AnimationState.AddAnimation(0, anim1, false, 0);
        skeletonAnimation.AnimationState.AddAnimation(0, anim2, isLoop, 0);
    }
}
