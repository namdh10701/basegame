using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AnimationState = Spine.AnimationState;

namespace _Game.Scripts
{
    public class SpineEventHandler : MonoBehaviour
    {
        [System.Serializable]
        public class EventPair
        {
            [SpineEvent] public string spineEvent;
            public UnityEvent unityHandler;
            public AnimationState.TrackEntryEventDelegate eventDelegate;
        }

        public List<EventPair> events = new List<EventPair>();

        ISkeletonComponent skeletonComponent;
        IAnimationStateComponent animationStateComponent;

        void Start()
        {
            if (skeletonComponent == null)
                skeletonComponent = GetComponent<ISkeletonComponent>();
            if (skeletonComponent == null) return;
            if (animationStateComponent == null)
                animationStateComponent = skeletonComponent as IAnimationStateComponent;
            if (animationStateComponent == null) return;
            Skeleton skeleton = skeletonComponent.Skeleton;
            if (skeleton == null) return;


            SkeletonData skeletonData = skeleton.Data;
            AnimationState state = animationStateComponent.AnimationState;
            foreach (EventPair ep in events)
            {
                EventData eventData = skeletonData.FindEvent(ep.spineEvent);
                ep.eventDelegate = ep.eventDelegate ?? delegate (TrackEntry trackEntry, Spine.Event e) { if (e.Data == eventData) Debug.Log(trackEntry.Animation.Name); ep.unityHandler.Invoke(); };
                state.Event += ep.eventDelegate;
            }
        }

        void OnDestroy()
        {
            if (animationStateComponent == null) animationStateComponent = GetComponent<IAnimationStateComponent>();
            if (animationStateComponent.IsNullOrDestroyed()) return;

            AnimationState state = animationStateComponent.AnimationState;
            foreach (EventPair ep in events)
            {
                if (ep.eventDelegate != null) state.Event -= ep.eventDelegate;
                ep.eventDelegate = null;
            }
        }

    }
}