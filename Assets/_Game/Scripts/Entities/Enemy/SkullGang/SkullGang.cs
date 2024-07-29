using _Base.Scripts.ImageEffects;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using _Game.Scripts.GD;
using DG.Tweening;
using MBT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public enum SkullGangState
    {
        None, Entry, Idle, Moving, Dead
    }

    public class SkullGang : MonoBehaviour, IEffectTaker, IStatsBearer, IGDConfigStatsTarget
    {
        SkullGangState state;
        public SkullGangState State
        {
            get { return state; }
            set
            {
                if (state == SkullGangState.Dead)
                {
                    return;
                }
                if (state != value)
                {
                    SkullGangState lastState = state;
                    state = value;
                    OnStateEntered?.Invoke(state);
                    OnEnterState();
                }
            }
        }
        public EffectTakerCollider effectTakerCollider;
        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;

        public Stats Stats => stats;

        public string id;
        public GDConfig gdConfig;
        public StatsTemplate statsTemplate;
        public Ship ship;
        public string Id { get => id; set => id = value; }

        public GDConfig GDConfig => gdConfig;

        public StatsTemplate StatsTemplate => statsTemplate;

        protected void OnEnterState()
        {
            switch (state)
            {
                case SkullGangState.Dead:
                    effectCollider.gameObject.SetActive(false);
                    mbtExecutor.enabled = false;
                    break;
            }
        }

        public SkullGangView view;
        public SkullMemberView left;
        public SkullMemberView right;

        public SkullMemberView behind;
        public PufferFishModel pufferFishModel;
        public SkullMemberProjectile SkullMemberProjectile;

        public GridAttackHandler gridAttack;
        public GridPicker gridPicker;
        private void Start()
        {
            effectTakerCollider.Taker = this;
            effectHandler.EffectTaker = this;
            gridAttack = FindAnyObjectByType<GridAttackHandler>();
            gridPicker = FindAnyObjectByType<GridPicker>();
            ship = FindAnyObjectByType<Ship>();
            view.Initialize(this);
            left.OnMeleeAttack += DoMeleeAttack;
            right.OnMeleeAttack += DoMeleeAttack;
            left.OnAttack += DoSpearAttack;
            right.OnAttack += DoSpearAttack;
            behind.OnAttack += DoThrowFishAttack;
            GetComponent<GDConfigStatsApplier>().LoadStats(this);
            blackboard.GetVariable<StatVariable>("MoveSpeed").Value = stats.MoveSpeed;
            blackboard.GetVariable<ShipVariable>("Ship").Value = ship;
            State = SkullGangState.Entry;

            stats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
        }

        bool isDead1;
        bool isDead2;
        public Action OnDead1;
        public Action OnDead2;

        private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
        {
            if (obj.Value < obj.MaxValue / 3 * 2)
            {
                if (!isDead1)
                {
                    //isDead1 = true;
                    //OnDead1?.Invoke();
                }
            }
            if (obj.Value < obj.MaxValue / 3)
            {
                if (!isDead2)
                {
                    //isDead2 = true;
                    //OnDead2?.Invoke();
                }
            }
            if (obj.Value <= 0)
            {
                State = SkullGangState.Dead;
            }
        }
        public AttackPatternProfile patternProfile;
        private void DoThrowFishAttack(Vector3 vector)
        {
            Debug.Log("throw fish");
            PufferFishModel pufferFish = Instantiate(pufferFishModel, null);
            pufferFish.transform.position = vector;
            pufferFish.transform.localScale = Vector3.one;
            //pufferFish.Body.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
            pufferFish.attackPatternProfile = patternProfile;
            Cell cell = gridPicker.PickRandomCell();
            pufferFish.pufferFishCollider.SetActive(false);
            pufferFish.SortLayer = "Projectile";
            pufferFish.transform.DOMove(cell.transform.position, 2f);
        }

        private void DoSpearAttack(Vector3 vector)
        {
            Vector3 targetPos = ship.ShipBound.ClosetPointTo(vector);
            SkullMemberProjectile spear = Instantiate(SkullMemberProjectile, null);
            spear.gameObject.SetActive(true);
            spear.SetData(vector, targetPos, 100);
            spear.Launch();
        }

        void DoMeleeAttack()
        {
            DoDmg();
        }

        void DoDmg()
        {
            Cell cell = gridPicker.PickRandomCell();
            EnemyAttackData enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = new List<Cell>() { cell };
            enemyAttackData.CenterCell = cell;

            Debug.LogError(cell.ToString());

            DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
            decreaseHp.Amount = stats.AttackDamage.Value;
            decreaseHp.ChanceAffectCell = 1;
            decreaseHp.transform.position = cell.transform.position;
            enemyAttackData.Effects = new List<Effect> { decreaseHp };

            gridAttack.ProcessAttack(enemyAttackData);
        }
        public void Active()
        {
            State = SkullGangState.Idle;
            effectCollider.gameObject.SetActive(true);
            mbtExecutor.enabled = true;
        }

        public void ApplyStats()
        {
            findTargetCollider?.SetRadius(stats.AttackRange.Value);
            cooldownBehaviour?.SetCooldownTime(stats.ActionSequenceInterval.Value);
        }

        internal void MelleThust()
        {
            view.MelleThrust();
            cooldownBehaviour.StartCooldown();
        }

        internal void ThrowSpear()
        {
            view.ThrowSpear();
            cooldownBehaviour.StartCooldown();
        }

        internal void ThrowFish()
        {
            view.ThrowFish();
            cooldownBehaviour.StartCooldown();
        }

        public EnemyStats stats;
        public GameObject effectCollider;
        public MBTExecutor mbtExecutor;
        public Blackboard blackboard;
        public EffectHandler effectHandler;
        public ObjectCollisionDetector findTargetCollider;
        public CooldownBehaviour cooldownBehaviour;

        public Action<SkullGangState> OnStateEntered;

        public Action OnSlowedDown;
        public Action OnSlowedDownStopped;
        public Action OnBurned;
        public Action OnBurnEnded;
        public Action OnFeared;
        public Action OnFearEnded;
        public Action OnStuned;
        public Action OnStunEnded;
    }
}
