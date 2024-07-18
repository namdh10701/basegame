using _Game.Scripts.Entities;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public abstract class EnemyView : MonoBehaviour
    {
        #region Const
        public static Color fearedOnHitColor = new Color(.55f, .4f, 1);
        public static Color slowOnHitColor = new Color(0.1f, 0.1f, 0.8f);
        public static Color onHitColor = new Color(0.6f, 0.6f, 0.6f);
        public static Color slowedColor = new Color(0, 0, 1);
        public static Color fearedColor = new Color(.55f, 0, 1);
        #endregion

        [Header("Model")]
        protected EnemyModel enemyModel;
        [SerializeField] protected EnemyState lastState;

        [Header("Anims")]
        [SerializeField] protected SkeletonAnimation skeletonAnim;
        [SpineAnimation] public string Entry;
        [SpineAnimation] public string Idle;
        [SpineAnimation] public string Moving;
        [SpineAnimation] public string Attacking;
        [SpineAnimation] public string HidingEntry;
        [SpineAnimation] public string HidingIdle;
        [SpineAnimation] public string Dead;

        [Header("Events")]
        [SpineEvent] public string attack;
        public Action OnAttack;

        [Header("Display")]
        MaterialPropertyBlock mpb;
        [SerializeField] MeshRenderer meshRenderer;
        [SerializeField] ParticleSystem burnFx;
        [SerializeField] ParticleSystem stunFx;
        Coroutine blinkCoroutine;
        bool isSlowing;
        bool isStunning;
        bool isFearing;
        bool isBurning;
        bool first;
        public void Blink()
        {
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
            }
            blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }
        IEnumerator BlinkCoroutine()
        {
            mpb.SetColor("_Black", isSlowing ? slowOnHitColor : onHitColor);
            meshRenderer.SetPropertyBlock(mpb);
            yield return new WaitForSeconds(.15f);
            mpb.SetColor("_Black", isSlowing ? slowedColor : Color.black);
            meshRenderer.SetPropertyBlock(mpb);
        }
        public virtual void Initialize(EnemyModel enemyModel)
        {
            this.enemyModel = enemyModel;
            mpb = new MaterialPropertyBlock();
            skeletonAnim.AnimationState.Event += AnimationState_Event;
            skeletonAnim.AnimationState.Complete += AnimationState_Complete;

            enemyModel.OnSlowedDown += OnSlowed;
            enemyModel.OnSlowedDownStopped += OnSlowEnded;

            enemyModel.OnFeared += OnFeared;
            enemyModel.OnFearEnded += OnFearEnded;

            enemyModel.OnBurned += OnBurned;
            enemyModel.OnBurnEnded += OnBurnEnded;

            enemyModel.OnStuned += OnStuned;
            enemyModel.OnStunEnded += OnStunEnded;

            enemyModel.OnStateEntered += OnStateEntered;

        }

        protected virtual void AnimationState_Complete(TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == Dead)
            {
                GameObject.Destroy(enemyModel.gameObject);
            }
        }
        protected virtual void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == attack)
            {
                OnAttack?.Invoke();
            }
        }
        public void PlayAttack()
        {
            skeletonAnim.AnimationState.SetAnimation(0, Attacking, false);
            skeletonAnim.AnimationState.AddAnimation(0, Idle, true,0);
        }

        protected virtual void OnStateEntered(EnemyState enemyState)
        {
            switch (enemyState)
            {
                case EnemyState.Entry:
                    if (!first)
                    {
                        StartCoroutine(EntryCoroutine());
                        first = true;
                    }
                    else
                    {
                        skeletonAnim.AnimationState.SetAnimation(0, Entry, false);
                        skeletonAnim.AnimationState.AddAnimation(0, Idle, true,0);
                    }
                    break;
                case EnemyState.Idle:
                    skeletonAnim.AnimationState.SetAnimation(0, Idle, true);
                    break;
                case EnemyState.Moving:
                    if (lastState == EnemyState.Hiding)
                    {
                        skeletonAnim.AnimationState.SetAnimation(0, Entry, false);
                        skeletonAnim.AnimationState.AddAnimation(0, Moving, true, 0);
                    }
                    else
                    {
                        skeletonAnim.AnimationState.SetAnimation(0, Moving, true);
                    }
                    break;
                case EnemyState.Attacking:

                    break;
                case EnemyState.Hiding:
                    skeletonAnim.AnimationState.SetAnimation(0, HidingEntry, false);
                    if (!string.IsNullOrEmpty(HidingIdle))
                        skeletonAnim.AnimationState.AddAnimation(0, HidingIdle, true, 0);
                    break;
                case EnemyState.Dead:
                    skeletonAnim.AnimationState.SetAnimation(0, Dead, true);
                    break;
            }
            lastState = enemyState;
        }

        protected IEnumerator EntryCoroutine()
        {
            yield return EntryVisualize();
            skeletonAnim.AnimationState.AddAnimation(0, Idle, true, 0);
            enemyModel.Active();

        }

        protected virtual IEnumerator EntryVisualize()
        {
            skeletonAnim.AnimationState.SetAnimation(0, Entry, false);
            yield return new WaitForSpineAnimationComplete(skeletonAnim.AnimationState.Tracks.ToArray()[0]);
        }

        private void OnStuned()
        {
            stunFx.Play();
        }

        private void OnStunEnded()
        {
            stunFx.Stop();
        }

        private void OnBurned()
        {
            burnFx.Play();
        }

        private void OnBurnEnded()
        {
            burnFx.Stop();
        }

        private void OnFeared()
        {
            isFearing = true;
            mpb.SetColor("_Black", fearedColor);
            meshRenderer.SetPropertyBlock(mpb);
        }

        private void OnFearEnded()
        {
            isFearing = false;
            ChangeColorOnEndEffect();
        }

        private void OnSlowed()
        {
            Debug.Log("sloes");
            isSlowing = true;
            mpb.SetColor("_Black", slowedColor);
            meshRenderer.SetPropertyBlock(mpb);
        }

        private void OnSlowEnded()
        {
            isSlowing = false;
            ChangeColorOnEndEffect();
        }

        private void ChangeColorOnEndEffect()
        {
            mpb.SetColor("_Black", Color.black);
            meshRenderer.SetPropertyBlock(mpb);
        }
    }
}