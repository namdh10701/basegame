using System;
using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Features.Gameplay;
using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using MBT;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public enum EnemyState
    {
        None, Entry, Idle, Moving, Attacking, Hiding, Dead
    }
    public enum ChargeState
    {
        None, Charging
    }

    public abstract class EnemyModel : Entity, IGDConfigStatsTarget, IEffectTaker, IPhysicsEffectTaker, ISlowable, IBurnable, IStunable, IFearable
    {
        public static Dictionary<string, float> dmgModifer = new Dictionary<string, float>()
        {
            {"0001",0 },
            {"0002",0.02f },
            {"0003",0.0404f },
            {"0004",0.061208f },
            {"0005",0.08243216f },
            {"0006",0.1040808032f },
            {"0007",0.1261624193f },
            {"0008",0.1486856676f },
            {"0009",0.171659381f },
            {"0010",0.1950925686f }
        };

        public static Dictionary<string, float> hpModifer = new Dictionary<string, float>()
        {
            {"0001",0 },
            {"0002",0.12f },
            {"0003",0.2544f },
            {"0004",0.404928f },
            {"0005",0.57351936f },
            {"0006",0.7623416832f },
            {"0007",0.9738226852f },
            {"0008",1.210681407f },
            {"0009",1.475963176f },
            {"0010",1.773078757f }
        };

        [Header("Enemy Stats")]
        [SerializeField] public EnemyStats _stats;

        [Header("Configuration")]
        [SerializeField] private string id;
        [SerializeField] private GDConfig gdConfig;
        [SerializeField] private StatsTemplate statsTemplate;

        [Header("Physics")]
        [SerializeField] protected Rigidbody2D body;

        [Header("Effects")]
        [SerializeField] protected EffectTakerCollider effectTakerCollider;
        [SerializeField] protected EffectHandler effectHandler;

        [Header("Behaviour")]
        [SerializeField] protected Blackboard blackboard;
        [SerializeField] protected MBTExecutor mbtExecutor;

        [Header("Controller and View")]
        [SerializeField] protected EnemyController enemyController;
        [SerializeField] protected EnemyView enemyView;

        [Header("Attack")]
        public GridAttackHandler atkHandler;
        public GridPicker gridPicker;
        [SerializeField] public AttackPatternProfile attackPatternProfile;
        [SerializeField] protected FindTargetBehaviour findTargetBehaviour;
        [SerializeField] protected ObjectCollisionDetector findTargetCollider;
        [SerializeField] protected CooldownBehaviour cooldownBehaviour;
        protected EnemyAttackData enemyAttackData;
        // State management
        protected EnemyState state = EnemyState.None;
        // Properties
        public override Stats Stats => _stats;
        public Transform Transform => transform;
        public EffectHandler EffectHandler => effectHandler;
        public string Id { get => id; set => id = value; }
        public GDConfig GDConfig => gdConfig;
        public StatsTemplate StatsTemplate => statsTemplate;
        public Rigidbody2D Body => body;
        public List<Stat> SlowableStats => new List<Stat>() { _stats.MoveSpeed, _stats.AnimationTimeScale };
        public float Poise => _stats.Poise.Value;
        public EffectTakerCollider EffectTakerCollider => effectTakerCollider;

        public Action OnSlowedDown;
        public Action OnSlowedDownStopped;
        public Action OnBurned;
        public Action OnBurnEnded;
        public Action OnFeared;
        public Action OnFearEnded;
        public Action OnStuned;
        public Action OnStunEnded;

        private EnemyStatsConfigLoader _configLoader;

        public EnemyStatsConfigLoader ConfigLoader
        {
            get
            {
                if (_configLoader == null)
                {
                    _configLoader = new EnemyStatsConfigLoader();
                }

                return _configLoader;
            }
        }
        protected virtual void Awake()
        {
            var conf = GameData.MonsterTable.FindById(id);
            ConfigLoader.LoadConfig(_stats, conf);
            ApplyStats();

            enemyController.Initialize(this);
            enemyView.Initialize(this);

            effectHandler.EffectTaker = this;
            effectTakerCollider.Taker = this;

            Ship ship = FindAnyObjectByType<Ship>();
            blackboard.GetVariable<ShipVariable>("Ship").Value = ship;
            Debug.Log("HERE");
            blackboard.GetVariable<StatVariable>("MoveSpeed").Value = _stats.MoveSpeed;
            _stats.AttackDamage.BaseValue *= (1 + dmgModifer[EnemyWaveManager.stageId]);
            _stats.HealthPoint.StatValue.BaseValue *= (1 + hpModifer[EnemyWaveManager.stageId]);
            _stats.HealthPoint.MaxStatValue.BaseValue *= (1 + hpModifer[EnemyWaveManager.stageId]);


            gridPicker = FindAnyObjectByType<GridPicker>();
            atkHandler = FindAnyObjectByType<GridAttackHandler>();
            State = EnemyState.Entry;
        }
        public override void ApplyStats()
        {
            findTargetCollider?.SetRadius(_stats.AttackRange.Value);
            cooldownBehaviour?.SetCooldownTime(_stats.ActionSequenceInterval.Value);
        }
        public void Active()
        {
            State = EnemyState.Idle;
            mbtExecutor.enabled = true;
            cooldownBehaviour?.StartCooldown();
        }

        private void OnDestroy()
        {
            GlobalEvent<EnemyStats>.Send("EnemyDied", _stats);
        }
        public ChargeState chargeState;

        public ChargeState ChargingState
        {
            get { return chargeState; }
            set
            {
                if (chargeState != value)
                {
                    ChargeState lastState = chargeState;
                    chargeState = value;
                    OnChargeStateStateEntered?.Invoke(chargeState);
                }
            }
        }
        public Action<ChargeState> OnChargeStateStateEntered;
        public EnemyState State
        {
            get { return state; }
            set
            {
                if (state == EnemyState.Dead)
                {
                    return;
                }
                if (state != value)
                {
                    EnemyState lastState = state;
                    state = value;
                    OnStateEntered?.Invoke(state);
                    OnEnterState();
                }
            }
        }

        public Stat StatusResist => null;

        public Action<EnemyState> OnStateEntered;
        protected virtual void OnEnterState()
        {
            switch (state)
            {
                case EnemyState.Hiding:
                    effectTakerCollider.gameObject.SetActive(false);
                    break;
                default:
                    effectTakerCollider.gameObject.SetActive(true);
                    break;
            }
        }

        public virtual void Die()
        {
            StopAllCoroutines();
            State = EnemyState.Dead;
            effectHandler.Clear();
            body.velocity = Vector3.zero;
            effectTakerCollider.gameObject.SetActive(false);
            mbtExecutor.gameObject.SetActive(false);
        }
        public virtual void OnAttackEnd()
        {
            if (cooldownBehaviour != null)
            {
                cooldownBehaviour.StartCooldown();
            }
        }

        public virtual bool IsInCooldown()
        {
            if (cooldownBehaviour == null)
            {
                return false;
            }
            else
            {
                return cooldownBehaviour.IsInCooldown;
            }
        }

        public virtual bool HasTarget()
        {
            if (findTargetBehaviour == null)
            {
                return false;
            }
            else
            {
                return findTargetBehaviour.MostTargets.Count > 0;
            }
        }
        public abstract void DoAttack();

        public void PerformAttack()
        {
            DoAttack();
            OnAttackEnd();
        }

        public abstract IEnumerator AttackSequence();

        #region Effects
        public virtual void OnSlowed()
        {
            OnSlowedDown?.Invoke();
        }

        public virtual void OnSlowEnded()
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

        public void OnStun()
        {
            OnStuned?.Invoke();
        }

        public void OnAfterStun()
        {
            OnStunEnded?.Invoke();
        }

        public void OnFear()
        {
            OnFeared?.Invoke();
        }

        public void OnFearEnd()
        {
            OnFearEnded?.Invoke();
        }
        #endregion
    }
}
