using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JellyFishAnimation : SpineAnimationEnemyHandler
{
    [HideInInspector] public UnityEvent<Transform> Attack;
    [HideInInspector] public UnityEvent AttackCharge;

    public Transform leftHandShootPos;
    public Transform rightHandShootPos;

    public void PlayIdleToAttackLoopRightHand()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "attack_r_hand_idletoready", false);
        skeletonAnimation.AnimationState.AddAnimation(0, "attack_r_hand_readyloop", true, 0);
        AttackCharge?.Invoke();
    }

    public void PlayAttackRightHand()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "attack_r_hand_attacktoidle", false);
        Attack?.Invoke(rightHandShootPos);
    }

    public void PlayIdleToAttackLoopLeftHand()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "attack_l_hand_idletoready", false);
        skeletonAnimation.AnimationState.AddAnimation(0, "attack_l_hand_readyloop", true, 0);
        AttackCharge?.Invoke();
    }

    public void PlayAttackLeftHand()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "attack_l_hand_attacktoidle", false);
        Attack?.Invoke(leftHandShootPos);
    }

    public void Appear()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "appear", false);
    }

    public void PlayMove()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name != "move")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "move", true);
        }
    }
    protected override void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
    {
    }

    protected override void AnimationState_Complete(TrackEntry trackEntry)
    {
        base.AnimationState_Complete(trackEntry);
    }

    protected override void Start()
    {
        base.Start();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Appear();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAttackLeftHand();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayIdleToAttackLoopLeftHand();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayAttackRightHand();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayIdleToAttackLoopRightHand();
        }

    }
}
