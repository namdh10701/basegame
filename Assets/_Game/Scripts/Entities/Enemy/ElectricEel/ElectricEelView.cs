
using _Game.Features.Gameplay;
using Spine;
using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ElectricEelView : EnemyView
{
    [Header("Ellectric Eel")]
    public GameObject rings;
    public Renderer[] visuals;

    public override void Initialize(EnemyModel enemyModel)
    {
        base.Initialize(enemyModel);
        ElectricEelModel eel = enemyModel as ElectricEelModel;
        eel.OnChargeStateStateEntered += OnElectricEelEntered;
    }

    private void OnElectricEelEntered(ChargeState state)
    {
        switch (state)
        {
            case ChargeState.Charging:
                skeletonAnim.AnimationState.SetAnimation(0, "begin_charge", false);
                skeletonAnim.AnimationState.AddAnimation(0, "charge", true, 0);
                break;
        }
    }

    public void PlayAttack()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "attack_toidle", false);
        OnAttack.Invoke();
    }


    public void Appear()
    {
        foreach (Renderer renderer in visuals)
        {
            renderer.enabled = true;
        }
        skeletonAnim.AnimationState.AddAnimation(0, "appear", false, 0);
    }

    public void Hide()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "hide", false);
        rings.gameObject.SetActive(false);
    }

    protected override void AnimationState_Complete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "hide")
        {
            foreach (Renderer renderer in visuals)
            {
                renderer.enabled = false;
            }
        }
        if (trackEntry.Animation.Name == "appear")
        {
            rings.SetActive(true);
        }
    }
}
