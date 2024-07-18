
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
    protected override void OnStateEntered(EnemyState enemyState)
    {
        base.OnStateEntered(enemyState);
        switch (enemyState)
        {
            case EnemyState.Entry:
                Appear();
                break;
        }
    }
    public void Appear()
    {
        foreach (Renderer renderer in visuals)
        {
            renderer.enabled = true;
        }
    }

    protected override void AnimationState_Complete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "hide")
        {
            foreach (Renderer renderer in visuals)
            {
                renderer.enabled = false;
            }
            rings.SetActive(false);
        }
        if (trackEntry.Animation.Name == "appear")
        {
            rings.SetActive(true);
        }
    }
}
