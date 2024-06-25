using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JellyFishAnimation : SpineAnimationEnemyHandler
{
    [HideInInspector] public UnityEvent Attack;
    [HideInInspector] public UnityEvent AttackMeele;
    [HideInInspector] public UnityEvent AttackCharge;

    [SpineEvent] public string eventattack;
    [SpineEvent] public string eventattackMelee;
    public Transform leftHandShootPos;
    public Transform rightHandShootPos;

    public void PlayIdleToAttackLoopRightHand()
    {
        skeletonAnimation.AnimationState.AddAnimation(0, "attack_r_hand_idletoready", false, 0);
        skeletonAnimation.AnimationState.AddAnimation(0, "attack_r_hand_readyloop", true, 0);
        AttackCharge?.Invoke();
    }

    public void PlayAttackRightHand()
    {
        skeletonAnimation.AnimationState.AddAnimation(0, "attack_r_hand_attacktoidle", false, 0);
        skeletonAnimation.AnimationState.AddAnimation(0, "idle", true, -.4f);
    }

    public void PlayIdleToAttackLoopLeftHand()
    {
        skeletonAnimation.AnimationState.AddAnimation(0, "attack_l_hand_idletoready", false, 0);
        skeletonAnimation.AnimationState.AddAnimation(0, "attack_l_hand_readyloop", true, 0);
        AttackCharge?.Invoke();
    }

    public void PlayAttackLeftHand()
    {
        skeletonAnimation.AnimationState.AddAnimation(0, "attack_l_hand_attacktoidle", false, 0);
        skeletonAnimation.AnimationState.AddAnimation(0, "idle", true, -.4f);

    }
    public void PlayAttackMeeleRightHand()
    {
        skeletonAnimation.AnimationState.AddAnimation(0, "attack_r_hand_melee", false, 0);
    }

    public void PlayAttackMeeleLeftHand()
    {

        skeletonAnimation.AnimationState.AddAnimation(0, "attack_l_hand_melee", false, 0);
    }

    public void Appear()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "appear", false);
        skeletonAnimation.AnimationState.AddAnimation(0, "idle", true, 0);
    }

    protected override void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
    {
        Debug.Log(e.Data.Name);
        if (e.Data.Name == eventattack)
        {
            Attack?.Invoke();
        }
        if (e.Data.Name == eventattackMelee)
        {
            AttackMeele?.Invoke();
        }
    }

    protected override void AnimationState_Complete(TrackEntry trackEntry)
    {
        base.AnimationState_Complete(trackEntry);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAttackMeeleLeftHand();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayAttackMeeleRightHand();
        }
    }
}
