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
        public Action OnMad;
        public EffectTakerCollider[] effectColliders;
        protected GridPicker gridPicker;
        protected GridAttackHandler gridAtk;

        public virtual void OnEnterState()
        {

        }




        public virtual void Active()
        {
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
            else
                State = lastpartState;
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