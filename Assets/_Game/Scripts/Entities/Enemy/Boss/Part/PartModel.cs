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

        [SerializeField] protected PartState lastpartState;
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
                    Debug.Log("ENTER STATE "+ value + " " + name);
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

        public Stat StatusResist => null;

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
            switch (state)
            {
                case OctopusState.None:
                    if (State != PartState.Hidding)
                        State = PartState.Idle;
                    break;
                case OctopusState.Entry:
                    break;
                case OctopusState.Transforming:

                    if (State != PartState.Hidding)
                    {
                        if (lastpartState == PartState.Entry)
                        {
                            Active();
                        }
                        Debug.Log("TRANSFORM   ! " + name);
                        State = PartState.Transforming;
                    }
                    else
                    {
                        Debug.Log("TRANSFORM   SKIP! " + name);
                        IsMad = true;
                        partView.ChangeSkin();

                    }
                    break;
                case OctopusState.State1:
                case OctopusState.State2:
                    break;
                case OctopusState.Stunning:
                    if (this.state != PartState.Hidding)
                    {
                        {
                            State = PartState.Stunning;
                        }
                    }
                    break;
                case OctopusState.Dead:
                    Deactive();
                    if (State != PartState.Hidding)
                    {
                        State = PartState.Dead;
                    }
                    else
                    {
                        IsDead = true;
                    }
                    break;
            }

            if (state != OctopusState.Stunning && this.state == PartState.Stunning)
            {
                AfterStun();
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
            if (this is BehindPartModel || this is BodyPartModel)
            {
                State = PartState.Idle;
            }
            else
            {
                if (lastpartState != PartState.Hidding)
                    State = PartState.Hidding;
            }
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