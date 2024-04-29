using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class SpineAnimation : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    public void PlayAnim(string name, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, name, false);
    }

}
