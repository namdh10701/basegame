using _Base.Scripts.Patterns.BuiltInPool;
using Spine.Unity;
using UnityEngine;

public class ShellAnimation : PoolObject
{
    public SkeletonAnimation SkeletonAnimation;
    private void Start()
    {
        SkeletonAnimation.AnimationState.Complete += AnimationState_Complete;
    }

    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "shell_left" || trackEntry.Animation.Name == "shell_right")
            Release();
    }

    public override void OnRelease()
    {
        SkeletonAnimation.gameObject.SetActive(false);
    }

    public void PlayLeftShell()
    {
        SkeletonAnimation.AnimationState.SetAnimation(0, "shell_left", false);
    }
    public void PlayRightShell()
    {
        SkeletonAnimation.AnimationState.SetAnimation(0, "shell_right", false);
    }
}
