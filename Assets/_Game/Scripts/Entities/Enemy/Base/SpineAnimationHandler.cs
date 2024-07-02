using UnityEngine;
using Spine.Unity;

namespace _Game.Scripts
{
    public class SpineAnimationHandler : MonoBehaviour
    {
        public SkeletonAnimation skeletonAnimation;

        public void PlayAnim(string name, bool isLoop)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, name, isLoop);
        }

    }
}