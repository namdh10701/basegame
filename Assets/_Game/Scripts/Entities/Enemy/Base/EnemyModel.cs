using System;
using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Features.Gameplay;
using _Game.Scripts.GD;
using MBT;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public abstract class EnemyModel : Entity, IGDConfigStatsTarget, IEffectTaker, IPhysicsEffectTaker, ISlowable
    {
        [Header("Enemy")]
        [SerializeField] protected EnemyStats _stats;
        [SerializeField] SpineAnimationEnemyHandler anim;

        [Header("GD Config Stats Target")]
        [SerializeField] private string id;
        [SerializeField] private GDConfig gdConfig;
        [SerializeField] private StatsTemplate statsTemplate;

        [Header("Physics Effect Taker")]
        [SerializeField] protected Rigidbody2D body;

        public string Id { get => id; set => id = value; }

        public GDConfig GDConfig => gdConfig;

        public StatsTemplate StatsTemplate => statsTemplate;

        [Header("Effect Taker")]
        [SerializeField] public EffectTakerCollider EffectTakerCollider;
        [SerializeField] protected EffectHandler effectHandler;

        [Header("Behaviour")]
        [SerializeField] protected Blackboard blackboard;
        [SerializeField] protected MBTExecutor MBTExecutor;

        public Action OnSlowedDown;
        public Action OnSlowedDownStopped;

        public override Stats Stats => _stats;
        public Transform Transform => transform;
        public EffectHandler EffectHandler => effectHandler;

        public List<Stat> SlowableStats => new List<Stat>() { _stats.MoveSpeed, _stats.AnimationTimeScale };

        public Rigidbody2D Body => body;

        public float Poise => _stats.Poise.Value;


        [SerializeField] EnemyController enemyController;

        public override void ApplyStats()
        {
            enemyController.Initialize(this, EffectTakerCollider, blackboard, MBTExecutor, Body, anim);
        }

        private void Start()
        {
            EffectHandler.EffectTaker = this;
            EffectTakerCollider.Taker = this;
        }

        public void OnSlowed()
        {
            OnSlowedDown?.Invoke();
        }

        public void OnSlowEnded()
        {
            OnSlowedDownStopped?.Invoke();
        }

        private void OnDestroy()
        {
            GlobalEvent<EnemyModel>.Send("EnemyDied", this);
        }
    }
}
