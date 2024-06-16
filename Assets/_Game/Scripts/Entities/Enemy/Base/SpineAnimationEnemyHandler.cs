using UnityEngine;
using Spine.Unity;
using Spine;
using System;
using System.Collections;
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
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;
    [SpineAnimation] public string idle;
    [SpineAnimation] public string dead;
    [SpineAnimation] public string move;
    protected Action onDead;


    [Header("Renderer")]
    public MeshRenderer meshRenderer;
    public Color onHitColor = new Color(180, 180, 180);
    public float onHitduration = 0.1f;
    Coroutine blinkCoroutine;

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
        {
        PlayIdle();
        }
           
    }
    public void PlayMove()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name != move)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, move, true);
        }
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
    public void Blink()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        mpb.SetColor("_Black", onHitColor);
        meshRenderer.SetPropertyBlock(mpb);
        yield return new WaitForSeconds(onHitduration);
        mpb.SetColor("_Black", Color.black);
        meshRenderer.SetPropertyBlock(mpb);
    }
}
