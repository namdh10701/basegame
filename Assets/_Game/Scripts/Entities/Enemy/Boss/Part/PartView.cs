using _Game.Features.Gameplay;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.ParticleSystem;

namespace _Game.Features.Gameplay
{
    public class PartView : MonoBehaviour
    {
        PartModel partModel;
        [SerializeField] SkeletonAnimation skeletonAnim;
        [SpineAnimation] public string entry;
        [SpineAnimation] public string idle;
        [SpineAnimation] public string transforming;
        [SpineAnimation] public string attack;
        [SpineAnimation] public string dead;

        [SerializeField] PartState lastState;
        public void Initnialize(PartModel partModel)
        {
            this.partModel = partModel;
            partModel.OnStateEntered += OnStateEntered;
        }

        bool first;
        private void OnStateEntered(PartState state)
        {
            if (lastState == state)
                return;
            switch (state)
            {
                case PartState.Entry:
                    if (!first)
                    {
                        StartCoroutine(EntryCoroutine());
                        first = true;
                    }
                    else
                    {
                        skeletonAnim.AnimationState.SetAnimation(0, entry, false);
                        skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
                    }
                    break;
                case PartState.Idle:
                    break;
                case PartState.Transforming:
                    break;
                case PartState.Attacking:
                    break;
                case PartState.Dead:
                    break;
            }
        }
        protected IEnumerator EntryCoroutine()
        {
            yield return EntryVisualize();
            skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
            partModel.Active();
        }
        protected virtual IEnumerator EntryVisualize()
        {
            skeletonAnim.AnimationState.SetAnimation(0, entry, false);
            yield return new WaitForSpineAnimationComplete(skeletonAnim.AnimationState.Tracks.ToArray()[0]);
        }
    }
}