using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts
{
    public class SquidAnimation : SpineAnimationEnemyHandler
    {
        [SpineEvent] public string action;
        [HideInInspector] public UnityEvent OnAction;
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
    }
}