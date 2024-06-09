
using Spine;
using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ElectricEelAnimation : SpineAnimationEnemyHandler
{
    [HideInInspector] public UnityEvent Attack;
    [SpineEvent] public string AttackEvent;
    public Action OnHide;
    public GameObject rings;
    public GameObject shadow;
    public void PlayAttack()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "attack_toidle", false);
        Attack.Invoke();
    }

    public void Charge()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "begin_charge", false);
        skeletonAnimation.AnimationState.AddAnimation(0, "charge", true, 0);
    }

    public void Appear()
    {
        meshRenderer.enabled = true;

        shadow.gameObject.SetActive(false);
        skeletonAnimation.AnimationState.AddAnimation(0, "appear", false, 0);

    }

    public void Hide()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "hide", false);
        rings.gameObject.SetActive(false);
    }

    protected override void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == AttackEvent)
        {

        }
    }

    protected override void AnimationState_Complete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == dead)
        {
            onDead?.Invoke();
            onDead = null;
        }
        if (!trackEntry.Loop && trackEntry.Next == null && trackEntry.Animation.Name != "hide")
            PlayIdle();

        if (trackEntry.Animation.Name == "hide")
        {
            meshRenderer.enabled = false;
            shadow.gameObject.SetActive(true);
            OnHide?.Invoke();
        }
        if (trackEntry.Animation.Name == "appear")
        {
            rings.SetActive(true);
        }
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
        if (Input.GetKeyDown(KeyCode.V))
        {
            Hide();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Charge();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAttack();
        }
    }
}
