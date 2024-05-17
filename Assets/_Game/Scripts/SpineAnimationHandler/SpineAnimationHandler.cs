using UnityEngine;
using Spine.Unity;
using Spine;
public class SpineAnimationHandler : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    [SpineAnimation] public string spineShoot;

    public void PlayShootAnim(bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, spineShoot, isLoop);
        skeletonAnimation.AnimationState.End += AnimationState_End;

    }

    private void AnimationState_End(TrackEntry trackEntry)
    {

    }

    public void PlayAnim(string name, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, name, isLoop);
    }

}
