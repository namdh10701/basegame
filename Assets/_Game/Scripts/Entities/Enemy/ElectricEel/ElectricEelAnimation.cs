
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class ElectricEelAnimation : SpineAnimationEnemyHandler
{
    [HideInInspector] public UnityEvent<Transform> Attack;
    [SpineEvent] public string AttackEvent;
    public Transform shootPos;

    public void PlayAttack()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "attack", false);
        Attack.Invoke(shootPos);
    }

    public void Appear()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "appear", false);
    }

    protected override void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
    {
        if(e.Data.Name == AttackEvent)
        {
            
        }
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
            PlayAttack();
        }
    }
}
