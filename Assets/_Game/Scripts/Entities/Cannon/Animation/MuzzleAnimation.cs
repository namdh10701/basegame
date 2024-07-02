using Spine.Unity;
using UnityEngine;

public class MuzzleAnimation : MonoBehaviour
{
    public SkeletonAnimation SkeletonAnimation;
    [SpineAnimation] public string smoke;
    bool isPlayed;
    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        Destroy(gameObject);
    }

    public void Play(float delay)
    {
        if (isPlayed)
            return;
        isPlayed = true;
        SkeletonAnimation.AnimationState.Complete += AnimationState_Complete;
        SkeletonAnimation.AnimationState.AddAnimation(0, smoke, false, delay);
    }
}
