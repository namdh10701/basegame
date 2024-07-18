using _Game.Features.Gameplay;
using Spine;
using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.Events;

public class JellyFishView : EnemyView
{
    public Action OnAttackMeele;
    
    [SpineEvent] public string eventattackMelee;
    public Transform leftHandShootPos;
    public Transform rightHandShootPos;

    public void PlayIdleToAttackLoopRightHand()
    {
        skeletonAnim.AnimationState.AddAnimation(0, "attack_r_hand_idletoready", false, 0);
        skeletonAnim.AnimationState.AddAnimation(0, "attack_r_hand_readyloop", true, 0);
    }

    public void PlayAttackRightHand()
    {
        skeletonAnim.AnimationState.AddAnimation(0, "attack_r_hand_attacktoidle", false, 0);
        skeletonAnim.AnimationState.AddAnimation(0, "idle", true, -.4f);
    }

    public void PlayIdleToAttackLoopLeftHand()
    {
        skeletonAnim.AnimationState.AddAnimation(0, "attack_l_hand_idletoready", false, 0);
        skeletonAnim.AnimationState.AddAnimation(0, "attack_l_hand_readyloop", true, 0);
    }

    public void PlayAttackLeftHand()
    {
        skeletonAnim.AnimationState.AddAnimation(0, "attack_l_hand_attacktoidle", false, 0);
        skeletonAnim.AnimationState.AddAnimation(0, "idle", true, -.4f);

    }
    public void PlayAttackMeeleRightHand()
    {
        skeletonAnim.AnimationState.AddAnimation(0, "attack_r_hand_melee", false, 0);
        skeletonAnim.AnimationState.AddAnimation(0, "idle", true, 0f);
    }

    public void PlayAttackMeeleLeftHand()
    {

        skeletonAnim.AnimationState.AddAnimation(0, "attack_l_hand_melee", false, 0);
        skeletonAnim.AnimationState.AddAnimation(0, "idle", true, 0f);
    }

    protected override void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == eventattackMelee)
        {
            OnAttackMeele?.Invoke();
        }
    }

}
