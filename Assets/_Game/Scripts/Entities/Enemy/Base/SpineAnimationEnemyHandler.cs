using UnityEngine;
using Spine.Unity;
using Spine;
using System;
using System.Collections;
using _Game.Scripts.Entities;
using _Game.Scripts;
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
    public EnemyModel enemyModel;


    [Header("Renderer")]
    MaterialPropertyBlock mpb;
    public MeshRenderer meshRenderer;
    public static Color slowOnHitColor = new Color(0.1f, 0.1f, 0.8f);
    public static Color onHitColor = new Color(0.6f, 0.6f, 0.6f);
    public static Color slowedDownColor = new Color(0, 0, 1);
    public float onHitduration = 0.1f;
    Coroutine blinkCoroutine;

    protected virtual void Awake()
    {
        enemyModel.OnSlowedDown += OnSlowedDown;
        enemyModel.OnSlowedDownStopped += OnSlowEnded;
        EnemyStats stats = enemyModel.Stats as EnemyStats;
        stats.AnimationTimeScale.OnValueChanged += AnimationTimeScale_OnValueChanged;
        mpb = new MaterialPropertyBlock();
        skeletonAnimation.AnimationState.Event += AnimationState_Event;
        skeletonAnimation.AnimationState.Complete += AnimationState_Complete;
    }

    private void AnimationTimeScale_OnValueChanged(_Base.Scripts.RPG.Stats.Stat obj)
    {
        skeletonAnimation.timeScale = obj.Value;
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
        enemyModel.OnSlowedDown -= OnSlowedDown;
        enemyModel.OnSlowedDownStopped -= OnSlowEnded;
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
        mpb.SetColor("_Black", isSlowing ? slowOnHitColor : onHitColor);
        meshRenderer.SetPropertyBlock(mpb);
        yield return new WaitForSeconds(onHitduration);
        mpb.SetColor("_Black", isSlowing ? slowedDownColor : Color.black);
        meshRenderer.SetPropertyBlock(mpb);
    }
    bool isSlowing;
    public void OnSlowedDown()
    {
        isSlowing = true;
        mpb.SetColor("_Black", slowedDownColor);
        meshRenderer.SetPropertyBlock(mpb);
    }

    public void OnSlowEnded()
    {
        isSlowing = false;
        mpb.SetColor("_Black", Color.black);
        meshRenderer.SetPropertyBlock(mpb);
    }

}
