using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class SkullMemberView : MonoBehaviour
    {
        [SerializeField] SkeletonAnimation skeletonAnimation;

        [SpineAnimation] public string Idle;
        [SpineAnimation] public string Move;
        [SpineAnimation] public string mixIdle;
        [SpineAnimation] public string mixMove;
        [SpineAnimation] public string dead;
        [SpineAnimation] public string deadIdle;

        public AnimationReferenceAsset thrust;
        public AnimationReferenceAsset throwFish;
        public AnimationReferenceAsset throwSpear;

        [SpineEvent] public string attackEvent;
        public Transform shootPos;
        public Action<Vector3> OnAttack;
        public Action OnMeleeAttack;
        public Action OnDead;
        public void Initialize(SkullGang skullGang, SkullGangView view)
        {
            skullGang.OnStateEntered += OnStateEntered;
            skeletonAnimation.AnimationState.Complete += AnimationState_Complete;
        }

        private void AnimationState_Complete(Spine.TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == deadIdle)
            {
                OnDead?.Invoke();
            }
        }

        private void OnStateEntered(SkullGangState state)
        {
            if (isDead)
            {
                return;
            }
            switch (state)
            {
                case SkullGangState.Entry:
                    break;
                case SkullGangState.Idle:
                    skeletonAnimation.AnimationState.SetAnimation(0, Idle, true);
                    if (!string.IsNullOrEmpty(mixIdle))
                        skeletonAnimation.AnimationState.SetAnimation(1, mixIdle, true);
                    break;
                case SkullGangState.Moving:
                    skeletonAnimation.AnimationState.SetAnimation(0, Move, true);
                    if (!string.IsNullOrEmpty(mixIdle))
                        skeletonAnimation.AnimationState.SetAnimation(1, mixMove, true);
                    break;
            }
        }

        private void Awake()
        {
            skeletonAnimation.AnimationState.Event += AnimationState_Event;
        }

        private void AnimationState_Event(Spine.TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == attackEvent)
            {
                OnAttack?.Invoke(shootPos.position);
            }
        }
        public bool isDead;
        internal void PlayDead()
        {
            if (!isDead)
            {
                Debug.Log(name);
                skeletonAnimation.AnimationState.SetAnimation(0, dead, false);
                isDead = true;
            }
        }

        internal void PlayThrust()
        {
            if (isDead)
            {
                return;
            }
            skeletonAnimation.AnimationState.SetAnimation(0, thrust, false);
            skeletonAnimation.AnimationState.AddAnimation(0, Idle, true, 0);
        }

        internal void PlayThrowSpear()
        {
            if (isDead)
            {
                return;
            }
            skeletonAnimation.AnimationState.SetAnimation(0, throwSpear, false);
            skeletonAnimation.AnimationState.AddAnimation(0, Idle, true, 0);
        }

        internal void PlayThrowFish()
        {
            if (isDead)
            {
                return;
            }
            skeletonAnimation.AnimationState.SetAnimation(0, throwFish, false);
            skeletonAnimation.AnimationState.AddAnimation(0, Idle, true, 0);
        }
    }
}