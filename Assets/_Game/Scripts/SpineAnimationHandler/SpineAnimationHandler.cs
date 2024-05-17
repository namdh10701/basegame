using UnityEngine;
using Spine.Unity;
public class SpineAnimationHandler : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    [SpineAnimation] public string spineShoot;

    public void PlayShootAnim(bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, spineShoot, isLoop);
    }

    public void PlayAnim(string name, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, name, isLoop);
    }
}
