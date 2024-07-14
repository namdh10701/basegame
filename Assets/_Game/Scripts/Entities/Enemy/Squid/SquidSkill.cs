using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidSkill : MonoBehaviour
{
    private void OnEnable()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationState.SetAnimation(0, "begin", false);
        skeletonAnimation.AnimationState.AddAnimation(0, "loop", true, 0);
    }
}
