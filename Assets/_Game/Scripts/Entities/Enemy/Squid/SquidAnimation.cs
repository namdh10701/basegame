using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts
{
    public class SquidAnimation : SpineAnimationEnemyHandler
    {
        [Header("Squid")]
        [SpineEvent] public string action;

        [SpineAnimation] public string doAction;
        [HideInInspector] public UnityEvent OnAction;

        protected override void Start()
        {
            base.Start();
        }

        protected override void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == action)
            {
                OnAction?.Invoke();
            }
        }
        protected override void AnimationState_Complete(TrackEntry trackEntry)
        {
            base.AnimationState_Complete(trackEntry);
        }

        public void PlayAttack()
        {
            skeletonAnimation.AnimationState.SetAnimation(0, doAction, false);
        }
       

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.V))
            {

                Blink();

            }
        }
    }
}