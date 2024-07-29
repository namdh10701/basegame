using _Base.Scripts.Audio;
using DG.Tweening;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class SkullGangView : MonoBehaviour
    {
        #region Const
        public static Color fearedOnHitColor = new Color(.55f, .4f, 1);
        public static Color slowOnHitColor = new Color(0.1f, 0.1f, 0.8f);
        public static Color onHitColor = new Color(0.6f, 0.6f, 0.6f);
        public static Color slowedColor = new Color(0, 0, 1);
        public static Color fearedColor = new Color(.55f, 0, 1);
        #endregion

        public SkeletonAnimation main;
        public SkeletonAnimation left;
        public SkeletonAnimation right;
        public SkeletonAnimation behind;

        [Header("Model")]
        protected SkullGang enemyModel;
        [SerializeField] protected SkullGangState lastState;

        [Header("Anims")]
        [SerializeField] protected SkeletonAnimation skeletonAnim;
        [SpineAnimation] public string Idle;
        [SpineAnimation] public string Moving;
        [SpineAnimation] public string Dead;

        [Header("Events")]
        public SkullMemberView leftEvent;
        public SkullMemberView rightEvent;
        public SkullMemberView behindEvent;

        [Header("Display")]
        MaterialPropertyBlock mpb;
        [SerializeField] MeshRenderer[] meshRenderers;

        [SerializeField] ParticleSystem burnFx;
        [SerializeField] ParticleSystem stunFx;
        Coroutine blinkCoroutine;
        Coroutine spear;
        Coroutine thrust;
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
            AudioManager.Instance.PlayMonsterGetHit();
            blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }
        IEnumerator BlinkCoroutine()
        {
            mpb.SetColor("_Black", isSlowing ? slowOnHitColor : onHitColor);
            foreach (MeshRenderer mr in meshRenderers)
            {
                mr.SetPropertyBlock(mpb);
            }
            yield return new WaitForSeconds(.15f);
            mpb.SetColor("_Black", isSlowing ? slowedColor : Color.black);
            foreach (MeshRenderer mr in meshRenderers)
            {
                mr.SetPropertyBlock(mpb);
            }
        }

        void OnDead1()
        {
            leftEvent.PlayDead();
        }

        void OnDead2()
        {
            rightEvent.PlayDead();
        }

        public virtual void Initialize(SkullGang enemyModel)
        {
            this.enemyModel = enemyModel;
            enemyModel.stats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            lastValue = enemyModel.stats.HealthPoint.Value;
            enemyModel.OnDead1 += OnDead1;
            enemyModel.OnDead2 += OnDead2;
            leftEvent.Initialize(enemyModel, this);
            rightEvent.Initialize(enemyModel, this);
            behindEvent.Initialize(enemyModel, this);
            leftEvent.OnDead += () => leftDead = true;
            rightEvent.OnDead += () => rightDead = true;
            behindEvent.OnDead += () => behindDead = true;
            mpb = new MaterialPropertyBlock();
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

        private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
        {
            if (obj.Value < lastValue)
            {
                Debug.Log("BLINK");
                Blink();
            }
            lastValue = obj.Value;
        }
        float lastValue;

        public void MelleThrust()
        {
            thrust = StartCoroutine(MeleeThrustCoroutine());
        }



        IEnumerator MeleeThrustCoroutine()
        {
            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand < .5f)
            {
                leftEvent.PlayThrust();
                yield return new WaitForSeconds(1f);
                rightEvent.PlayThrust();
            }
            else
            {
                rightEvent.PlayThrust();
                yield return new WaitForSeconds(1f);
                leftEvent.PlayThrust();
            }
        }

        public void ThrowSpear()
        {
            spear = StartCoroutine(ThrowSpearCoroutine());
        }

        IEnumerator ThrowSpearCoroutine()
        {
            float rand = UnityEngine.Random.Range(0, 1f);
            if (rand < .5f)
            {
                leftEvent.PlayThrowSpear();
                yield return new WaitForSeconds(1f);
                rightEvent.PlayThrowSpear();
            }
            else
            {
                rightEvent.PlayThrowSpear();
                yield return new WaitForSeconds(1f);
                leftEvent.PlayThrowSpear();
            }
        }

        public void ThrowFish()
        {
            behindEvent.PlayThrowFish();
        }
        protected virtual void AnimationState_Complete(TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == Dead)
            {
                GameObject.Destroy(enemyModel.gameObject);
            }
        }
        protected virtual void OnStateEntered(SkullGangState enemyState)
        {
            switch (enemyState)
            {
                case SkullGangState.Entry:
                    StartCoroutine(EntryCoroutine());
                    break;
                case SkullGangState.Idle:
                    skeletonAnim.AnimationState.SetAnimation(0, Idle, true);
                    break;
                case SkullGangState.Moving:
                    skeletonAnim.AnimationState.SetAnimation(0, Moving, true);
                    break;
                case SkullGangState.Dead:
                    if (spear != null)
                        StopCoroutine(spear);
                    if (thrust != null)
                        StopCoroutine(thrust);
                    Debug.Log("DEAD ENTER");
                    StartCoroutine(DeadCoroutine());
                    break;
            }
            lastState = enemyState;
        }
        public bool leftDead;
        public bool rightDead;
        public bool behindDead;
        IEnumerator DeadCoroutine()
        {
            behindEvent.PlayDead();
            yield return new WaitUntil(() => behindDead);
            skeletonAnim.AnimationState.SetAnimation(0, Dead, false);
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            behind.gameObject.SetActive(false);
            yield return new WaitForSpineAnimationComplete(skeletonAnim.AnimationState.Tracks.ToArray()[0]);
        }

        protected IEnumerator EntryCoroutine()
        {
            yield return EntryVisualize();
            skeletonAnim.AnimationState.AddAnimation(0, Idle, true, 0);
            enemyModel.Active();

        }

        protected virtual IEnumerator EntryVisualize()
        {
            yield return enemyModel.transform.DOMove(MoveAreaController.Instance.SkullStartPos.position, 2).WaitForCompletion();
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
            foreach (MeshRenderer mr in meshRenderers)
            {
                mr.SetPropertyBlock(mpb);
            }
        }

        private void OnFearEnded()
        {
            isFearing = false;
            ChangeColorOnEndEffect();
        }



        private void OnSlowEnded()
        {
            isSlowing = false;
            ChangeColorOnEndEffect();
        }
        private void OnSlowed()
        {
            isSlowing = true;
            mpb.SetColor("_Black", slowedColor);
            foreach (MeshRenderer mr in meshRenderers)
            {
                mr.SetPropertyBlock(mpb);
            }
        }
        private void ChangeColorOnEndEffect()
        {
            mpb.SetColor("_Black", Color.black);
            foreach (MeshRenderer mr in meshRenderers)
            {
                mr.SetPropertyBlock(mpb);
            }
        }


    }
}