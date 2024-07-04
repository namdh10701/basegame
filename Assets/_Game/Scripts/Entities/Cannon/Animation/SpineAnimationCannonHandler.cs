using _Game.Scripts;
using Spine;
using Spine.Unity;
using System;

public class SpineAnimationCannonHandler : SpineAnimationHandler
{
    [SpineAnimation] public string spineShoot;
    [SpineEvent] public string shootEvent;
    public Action OnShoot;
    public ShellAnimationPool ShellAnimationPool;
    public MuzzleAnimation muzzleAnimationPrefab;

    ShellAnimation shellAnimation;
    public float muzzleDelay;
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

    public void PlayBroken()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "broken", true);
    }
    public void PlayNormal()
    {
        skeletonAnimation.AnimationState.AddEmptyAnimation(0, 0, 0);
    }
    protected void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == shootEvent)
        {
            OnShoot?.Invoke();
            isLeft = !isLeft;
            shellAnimation = (ShellAnimation)ShellAnimationPool.Pull();
            if (isLeft) shellAnimation.PlayLeftShell();
            else shellAnimation.PlayRightShell();
            if (muzzleAnimationPrefab != null)
            {
                MuzzleAnimation muzzle = Instantiate(muzzleAnimationPrefab, muzzleAnimationPrefab.transform.position, muzzleAnimationPrefab.transform.rotation, null);
                muzzle.Play(muzzleDelay);
            }
        }
    }


}
