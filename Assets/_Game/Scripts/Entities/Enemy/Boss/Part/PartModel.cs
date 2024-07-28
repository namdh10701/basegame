using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using MBT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public enum PartState
    {
        None, Entry, Idle, Attacking, Stunning, Transforming, Hidding, Dead, Moving
    }
    public class PartModel : Entity, IEffectTaker, ISlowable, IBurnable
    {

        [SerializeField] PartState lastpartState;
        [SerializeField] PartState state;
        public PartState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    if (state == PartState.Dead)
                    {
                        return;
                    }
                    if (value == PartState.Hidding)
                    {

                    }
                    lastpartState = state;
                    state = value;
                    OnStateEntered?.Invoke(state);
                    OnEnterState();
                }
            }
        }

        public override Stats Stats => stats;

        public Transform Transform => transform;

        public CompositeEffectHandler effectHandler;
        public EffectHandler EffectHandler => effectHandler;

        public List<Stat> SlowableStats => new List<Stat> { stats.MoveSpeed, stats.AnimationTimeScale };

        public Action OnSlowedDown;
        public Action OnSlowedDownStopped;
        public Action OnBurned;
        public Action OnBurnEnded;

        public PartView partView;
        public Action<PartState> OnStateEntered;
        protected GiantOctopus giantOctopus;
        public bool IsMad;
        public bool IsDead;
        public Action OnMad;
        public EffectTakerCollider[] effectColliders;
        protected GridPicker gridPicker;
        protected GridAttackHandler gridAtk;

        public virtual void OnEnterState()
        {

        }




        public virtual void Active()
        {
            if (giantOctopus.State != OctopusState.Entry && giantOctopus.State != OctopusState.None)
                foreach (var item in effectColliders) { item.gameObject.SetActive(true); }
        }

        public virtual void Deactive()
        {
            foreach (var item in effectColliders) { item.gameObject.SetActive(false); }
        }

        public virtual void Initialize(GiantOctopus giantOctopus)
        {
            gridPicker = FindAnyObjectByType<GridPicker>();
            gridAtk = FindAnyObjectByType<GridAttackHandler>();
            this.giantOctopus = giantOctopus;
            if (effectHandler != null)
            {
                effectHandler.Other = giantOctopus;
                effectHandler.EffectTaker = this;
            }
            foreach (var item in effectColliders) { item.Taker = this; }
            giantOctopus.OnStateEntered += OnOctopusStateEntered;
            partView.Initnialize(this);
            partView.OnAttack += DoAttack;
        }

        OctopusState lastState;
        private void OnOctopusStateEntered(OctopusState state)
        {
            if (lastState == state)
                return;
            if (State == PartState.Dead)
            {
                return;
            }
            if (state != OctopusState.Stunning && this.state == PartState.Stunning)
            {
                AfterStun();
            }

            if (this.state != PartState.Hidding)
            {
                if (state == OctopusState.Stunning)
                {
                    Active();
                    State = PartState.Stunning;
                }
            }
            if (this.state == PartState.Hidding)
            {
                if (state == OctopusState.Transforming)
                {
                    IsMad = true;
                    partView.ChangeSkin();
                }
            }
            if (state == OctopusState.Transforming)
            {
                if (State != PartState.Hidding)
                {
                    State = PartState.Transforming;
                }
                else
                {
                    IsMad = true;
                    partView.ChangeSkin();
                }
            }
            if (state == OctopusState.Dead)
            {
                if (State != PartState.Hidding)
                {
                    State = PartState.Dead;
                }
                else
                {
                    IsDead = true;
                }
            }
            lastState = state;
        }

        public virtual void AfterStun()
        {
            State = lastpartState;
        }

        public virtual void DoAttack()
        {

        }

        public virtual IEnumerator TransformCoroutine()
        {
            yield return new WaitUntil(() => IsMad);
            if (lastState == OctopusState.Stunning)
            {
                State = PartState.Idle;
            }
            else if (State != PartState.Hidding)
                State = lastpartState;
        }
        public virtual IEnumerator DeadCoroutine()
        {
            yield return new WaitUntil(() => IsDead);

        }
        public override void ApplyStats()
        {

        }

        public void OnSlowed()
        {
            OnSlowedDown?.Invoke();
        }

        public void OnSlowEnded()
        {
            OnSlowedDownStopped?.Invoke();
        }

        public void OnBurn()
        {
            OnBurned?.Invoke();
        }

        public void OnBurnEnd()
        {
            OnBurnEnded?.Invoke();
        }

        public EnemyStats stats;
    }
}