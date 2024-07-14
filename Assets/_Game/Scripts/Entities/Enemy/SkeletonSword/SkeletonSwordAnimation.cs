using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class SkeletonSwordAnimation : MonoBehaviour
    {
        [SpineAnimation] public string Entry;
        [SpineAnimation] public string Idle;
        [SpineAnimation] public string Moving;
        [SpineAnimation] public string Attacking;
        [SpineAnimation] public string HidingEntry;
        [SpineAnimation] public string HidingIdle;
        [SpineAnimation] public string Dead;

        [SpineEvent] public string attack;
        public SkeletonAnimation SkeletonAnimation;
        public EnemyState lastState;
        MaterialPropertyBlock mpb;
        public MeshRenderer meshRenderer;
        public Action OnAttack;
        Coroutine blinkCoroutine;

        public SkeletonSword skeletonSword;
        private void Start()
        {
            SkeletonAnimation.AnimationState.Event += AnimationState_Event;
            SkeletonAnimation.AnimationState.Complete += AnimationState_Complete;
            mpb = new MaterialPropertyBlock();
            skeletonSword.OnSlowedDown += OnSlowedDown;
            skeletonSword.OnSlowedDownStopped += OnSlowEnded;
        }

        private void AnimationState_Complete(TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == Dead)
            {
                Destroy(skeletonSword.gameObject);
            }
        }

        public void Blink()
        {
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
            }
            blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }
        bool isSlowing;
        public static Color slowOnHitColor = new Color(0.1f, 0.1f, 0.8f);
        public static Color onHitColor = new Color(0.6f, 0.6f, 0.6f);
        public static Color slowedDownColor = new Color(0, 0, 1);
        public void OnSlowedDown()
        {
            isSlowing = true;
            mpb.SetColor("_Black", slowedDownColor);
            meshRenderer.SetPropertyBlock(mpb);
        }

        public void OnSlowEnded()
        {
            isSlowing = false;
            mpb.SetColor("_Black", Color.black);
            meshRenderer.SetPropertyBlock(mpb);
        }
        IEnumerator BlinkCoroutine()
        {
            mpb.SetColor("_Black", isSlowing ? slowOnHitColor : onHitColor);
            meshRenderer.SetPropertyBlock(mpb);
            yield return new WaitForSeconds(.1f);
            mpb.SetColor("_Black", isSlowing ? slowedDownColor : Color.black);
            meshRenderer.SetPropertyBlock(mpb);
        }
        private void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == attack)
            {
                OnAttack?.Invoke();
            }
        }

        private void OnEnable()
        {
            skeletonSword.OnStateChanged += OnStateEntered;
        }

        private void OnDisable()
        {
            skeletonSword.OnStateChanged -= OnStateEntered;
        }

        void OnStateEntered(EnemyState enemyState)
        {
            switch (enemyState)
            {
                case EnemyState.Entry:
                    SkeletonAnimation.AnimationState.SetAnimation(0, Entry, false);
                    SkeletonAnimation.AnimationState.AddAnimation(0, Idle, true, 0);
                    break;
                case EnemyState.Idle:
                    SkeletonAnimation.AnimationState.SetAnimation(0, Idle, true);
                    break;
                case EnemyState.Moving:
                    if (lastState == EnemyState.Hiding)
                    {
                        SkeletonAnimation.AnimationState.SetAnimation(0, Entry, false);
                        SkeletonAnimation.AnimationState.AddAnimation(0, Moving, true, 0);
                    }
                    else
                    {
                        SkeletonAnimation.AnimationState.SetAnimation(0, Moving, true);
                    }
                    break;
                case EnemyState.Attacking:

                    break;
                case EnemyState.Hiding:
                    SkeletonAnimation.AnimationState.SetAnimation(0, HidingEntry, false);
                    SkeletonAnimation.AnimationState.AddAnimation(0, HidingIdle, true, 0);
                    break;
                case EnemyState.Dead:
                    SkeletonAnimation.AnimationState.SetAnimation(0, Dead, true);
                    break;
            }
            lastState = enemyState;
        }

        public void PlayAttack()
        {
            SkeletonAnimation.AnimationState.SetAnimation(0, Attacking, false);
        }


    }

}