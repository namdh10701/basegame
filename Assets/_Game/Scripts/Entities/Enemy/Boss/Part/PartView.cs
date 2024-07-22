using _Game.Features.Gameplay;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class PartView : MonoBehaviour
    {
        public static Color slowOnHitColor = new Color(0.1f, 0.1f, 0.8f);
        public static Color onHitColor = new Color(0.6f, 0.6f, 0.6f);
        public static Color slowedColor = new Color(0, 0, 1);

        PartModel partModel;
        [SerializeField] protected SkeletonAnimation skeletonAnim;
        [SpineAnimation] public string entry;
        [SpineAnimation] public string idle;
        [SpineAnimation] public string transforming;
        [SpineAnimation] public string attack;
        [SpineAnimation] public string dead;
        [SpineAnimation] public string hide;
        [SpineAnimation] public string stun;
        [SerializeField] protected PartState lastState;


        public Action OnAttack;
        [SpineEvent] public string attackEvent;

        Coroutine blinkCoroutine;
        MaterialPropertyBlock mpb;
        bool isSlowing;
        public ParticleSystem burnFx;
        public MeshRenderer meshRenderer;
        public virtual void Initnialize(PartModel partModel)
        {
            this.partModel = partModel; 
            mpb = new MaterialPropertyBlock();
            partModel.stats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            lastHP = partModel.stats.HealthPoint.Value;

            partModel.OnSlowedDown += OnSlowed;
            partModel.OnSlowedDownStopped += OnSlowEnded;

            partModel.OnBurned += OnBurned;
            partModel.OnBurnEnded += OnBurnEnded;

            partModel.OnStateEntered += OnStateEntered;
            skeletonAnim.AnimationState.Complete += AnimationState_Completed;
            skeletonAnim.AnimationState.Event += AnimationState_Event;

        }

        protected virtual void AnimationState_Completed(TrackEntry trackEntry)
        {
            if (!string.IsNullOrEmpty(transforming))
            {
                if (trackEntry.Animation.Name == transforming)
                {
                    ExposedList<Skin> listOfSkins = skeletonAnim.skeleton.Data.Skins;
                    List<Skin> bodySkins = new List<Skin>();
                    foreach (Skin skin in listOfSkins)
                    {
                        if (skin.Name.Contains("mad"))
                        {
                            skeletonAnim.Skeleton.SetSkin("mad");
                            skeletonAnim.Skeleton.SetSlotsToSetupPose();
                            skeletonAnim.LateUpdate();
                        }
                        if (skin.Name.Contains("MAD"))
                        {
                            skeletonAnim.Skeleton.SetSkin("MAD");
                            skeletonAnim.Skeleton.SetSlotsToSetupPose();
                            skeletonAnim.LateUpdate();
                        }
                    }
                }
            }
        }
        float lastHP;
        private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
        {
            if (obj.Value < lastHP)
            {
                Blink();
            }
            lastHP = obj.Value;
        }

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
        private void AnimationState_Event(Spine.TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == attackEvent)
            {
                OnAttack?.Invoke();
            }
        }

        bool first;

        public void OnSlowed()
        {
            isSlowing = true;
        }
        public void OnSlowEnded()
        {
            isSlowing = false;
        }
        public void OnBurned()
        {
            burnFx?.Play();
        }
        public void OnBurnEnded()
        {

            burnFx?.Stop();
        }

        protected virtual void OnStateEntered(PartState state)
        {
            if (lastState == state)
                return;
            switch (state)
            {
                case PartState.Entry:
                    StartCoroutine(EntryCoroutine());
                    break;
                case PartState.Idle:
                    HandleIdleEnter();
                    break;
                case PartState.Hidding:
                    StartCoroutine(HideCoroutine());
                    break;
                case PartState.Transforming:
                    if (string.IsNullOrEmpty(transforming))
                    {
                        skeletonAnim.Skeleton.SetSkin("mad");
                        partModel.IsMad = true;
                    }
                    else
                    {
                        StartCoroutine(TransformCoroutine());
                    }
                    break;
                case PartState.Stunning:
                    StunAnim();
                    break;
                case PartState.Attacking:

                    break;
                case PartState.Dead:
                    break;
            }
            lastState = state;
        }
        public virtual void HandleIdleEnter()
        {
            if (lastState == PartState.Attacking)
                skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
            else

                skeletonAnim.AnimationState.SetAnimation(0, idle, true);

        }
        public virtual void StunAnim()
        {
            if (!string.IsNullOrEmpty(stun))
                skeletonAnim.AnimationState.SetAnimation(0, stun, true);
        }

        public virtual void PlayEntry()
        {
            gameObject.SetActive(true);
            skeletonAnim.AnimationState.SetAnimation(0, entry, false);
            skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
        }

        protected IEnumerator EntryCoroutine()
        {
            yield return EntryVisualize();
            skeletonAnim.AnimationState.AddAnimation(0, idle, true, 0);
            partModel.Active();
        }
        protected virtual IEnumerator TransformCoroutine()
        {
            skeletonAnim.AnimationState.SetAnimation(0, transforming, false);
            yield return new WaitForSpineAnimationComplete(skeletonAnim.AnimationState.Tracks.ToArray()[0]);
            partModel.IsMad = true;
        }
        protected virtual IEnumerator EntryVisualize()
        {
            skeletonAnim.AnimationState.SetAnimation(0, entry, false);
            yield return new WaitForSpineAnimationComplete(skeletonAnim.AnimationState.Tracks.ToArray()[0]);
        }

        protected virtual IEnumerator HideCoroutine()
        {
            lastState = PartState.Hidding;
            skeletonAnim.AnimationState.SetAnimation(0, hide, false);
            yield return new WaitForSpineAnimationComplete(skeletonAnim.AnimationState.Tracks.ToArray()[0]);
            partModel.Deactive();
        }

        public void PlayHide()
        {
            StartCoroutine(HideCoroutine());
        }
    }
}