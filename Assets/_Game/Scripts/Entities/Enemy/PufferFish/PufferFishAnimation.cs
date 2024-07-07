
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts
{
    public class PufferFishAnimation : SpineAnimationEnemyHandler
    {
        [SpineEvent] public string attack;
        [HideInInspector] public UnityEvent OnAttack;
        public void ChargeExplode()
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "bomb_ship_transform", false);
            skeletonAnimation.AnimationState.AddAnimation(0, "bomb_ship_loop", true, 0);
        }
        protected override void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == attack)
            {
                OnAttack?.Invoke();
            }
        }

        protected override void AnimationState_Complete(TrackEntry trackEntry)
        {
            base.AnimationState_Complete(trackEntry);
        }
    }
}