using UnityEngine;
using Spine.Unity;
using Spine;
public class SpineAnimationEnemyHandler : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    void Start()
    {
        //skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);

        skeletonAnimation.AnimationState.Complete += delegate (TrackEntry trackEntry)
        {
            Debug.Log("END ANIM");
            switch (trackEntry.Animation.Name)
            {
                case "action":
                case "appear":
                case "hide":
                    Debug.Log("Play aninm Idle");
                    skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
                    break;
            }
        };
    }

    public void PlayAttackAnim(bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "action", isLoop);
        Debug.Log("Play PlayAttackAnim");
    }

    public void PlayIdleAnim(bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "appear", isLoop);
    }

    public void PlayMoveAnim(bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "hide", isLoop);
    }

    public void PlayAnim(string name, bool isLoop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, name, isLoop);
    }
}
