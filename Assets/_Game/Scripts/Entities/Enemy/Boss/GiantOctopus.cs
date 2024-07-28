using _Base.Scripts.RPG.Effects;
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts;
using MBT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public enum OctopusState
    {
        None, Entry, Stunning, Transforming, State1, State2, Dead
    }
    public class GiantOctopus : MonoBehaviour, IEffectTaker, IStatsBearer
    {
        [SerializeField] OctopusState state;
        [SerializeField] OctopusState lastState;
        public OctopusState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    lastState = state;
                    state = value;
                    OnStateEntered?.Invoke(state);
                    OnEnterState();
                }
            }
        }

        Transform IEffectTaker.Transform => transform;

        public EffectHandler EffectHandler => effectHandler;
        public EffectHandler effectHandler;
        private void OnEnterState()
        {
            if (state == OctopusState.Stunning)
            {
                StopAllCoroutines();
                StartCoroutine(StunCoroutine());

            }
            if (state == OctopusState.Transforming)
            {
                mbtExecutor.enabled = false;
                StopAllAttack();
                StopAllCoroutines();
                StartCoroutine(TransformCoroutine());
                Debug.Log("TRANSFORM");

            }
            if (state == OctopusState.State2 || state == OctopusState.State1)
            {
                mbtExecutor.enabled = true;
                StartCoroutine(AttackCycle());
            }

            if (state == OctopusState.Dead)
            {
                StopAllCoroutines();
                StartCoroutine(DeadCoroutine());
            }
        }

        private IEnumerator DeadCoroutine()
        {
            Coroutine bodyTransform = StartCoroutine(body.DeadCoroutine());
            Coroutine upperTransform = StartCoroutine(upperPartController.DeadCoroutine());
            Coroutine lowerTransform = StartCoroutine(lowerPartController.DeadCoroutine());
            Coroutine behindTransform = StartCoroutine(behindPartController.DeadCoroutine());

            yield return bodyTransform;
            yield return upperTransform;
            yield return lowerTransform;
            yield return behindTransform;
            Destroy(gameObject);
        }

        void StopAllAttack()
        {

        }

        IEnumerator TransformCoroutine()
        {
            Coroutine bodyTransform = StartCoroutine(body.TransformCoroutine());
            Coroutine upperTransform = StartCoroutine(upperPartController.TransformCoroutine());
            Coroutine lowerTransform = StartCoroutine(lowerPartController.TransformCoroutine());
            Coroutine behindTransform = StartCoroutine(behindPartController.TransformCoroutine());

            yield return bodyTransform;
            yield return upperTransform;
            yield return lowerTransform;
            yield return behindTransform;
            Mad = true;
            Debug.Log("COMPLETE TRANSFORM");
            State = OctopusState.State2;
            lastState = OctopusState.State2;

        }

        public Action<OctopusState> OnStateEntered;


        public PartModel[] Parts;


        public GiantOctopusView GiantOctopusView;
        public Action OnDied;

        public MBTExecutor mbtExecutor;
        public Blackboard blackboard;

        public BodyPartModel body;
        public UpperPartController upperPartController;
        public LowerPartController lowerPartController;
        public BehindPartController behindPartController;
        public SpawnPartController spawnPartController;
        public EnemyStats enemyStats;
        private void Start()
        {
            effectHandler.EffectTaker = this;

            GiantOctopusView.Initialize(this);
            foreach (PartModel part in Parts)
            {
                part.Initialize(this);
            }
            GiantOctopusView.OnEntryCompleted += Active;
            body.AttackEnded += OnBodyAttackEnded;
            lowerPartController.OnAttackEnded += OnLowerPartAttackEnded;
            upperPartController.OnAttackEnded += OnUpperPartAttackEnded;
            behindPartController.OnAttackEnded += OnBehindPartAttackEnded;
            spawnPartController.OnAttackEnded += OnSpawnPartAttackEnded;
            State = OctopusState.Entry;
            enemyStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged1;
        }

        private void HealthPoint_OnValueChanged1(_Base.Scripts.RPG.Stats.RangedStat obj)
        {
            if (obj.Value <= obj.MinValue)
            {
                State = OctopusState.Dead;
            }
        }

        void OnBodyAttackEnded()
        {
            State = OctopusState.Stunning;
        }

        IEnumerator StunCoroutine()
        {
            mbtExecutor.enabled = false;
            State = OctopusState.Stunning;
            yield return new WaitForSeconds(6);
            State = lastState;

        }

        void OnLowerPartAttackEnded()
        {

        }
        void OnUpperPartAttackEnded()
        {

        }
        void OnBehindPartAttackEnded()
        {

        }
        void OnSpawnPartAttackEnded()
        {

        }


        private void Awake()
        {
            enemyStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
        }
        bool Mad;
        private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
        {
            if (obj.Value < obj.MaxValue / 2)
            {
                if (!Mad)
                {
                    State = OctopusState.Transforming;
                    Mad = true;
                }
            }
        }



        public void Active()
        {
            State = OctopusState.State1;
            body.Active();
            behindPartController.Active();
        }

        [ContextMenu("Grab Attack")]
        public void GrabAttack()
        {
            if (!lowerPartController.IsAttacking)
            {
                lowerPartController.StartAttack();
            }
        }
        [ContextMenu("Upper Attack")]
        public void UpperAttack()
        {
            if (!upperPartController.IsAttacking)
                upperPartController.StartAttack();
        }
        [ContextMenu("Laser Attack")]
        public void LaserAttack()
        {
            if (!body.IsAttacking)
                body.Attack();
        }
        [ContextMenu("Transform")]
        public void Transform()
        {
            State = OctopusState.Transforming;
        }

        [ContextMenu("BehindAttack")]
        public void BehindAttack()
        {
            if (!behindPartController.IsAttacking)
                behindPartController.StartAttack();
        }

        [ContextMenu("Spawn Attack")]
        public void SpawnAttack()
        {
            if (!spawnPartController.IsAttacking)
                spawnPartController.StartAttack();
        }


        private void Attack(string attackName)
        {
            switch (attackName)
            {
                case "Grab":
                    GrabAttack();
                    break;
                case "Upper":
                    UpperAttack();
                    break;
                case "Laser":
                    LaserAttack();
                    break;
                case "Behind":
                    BehindAttack();
                    break;
                case "Spawn":
                    SpawnAttack();
                    break;
                default:
                    Debug.LogWarning($"Unknown attack type: {attackName}");
                    break;
            }
        }

        public int CurrentActiveAttack
        {
            get
            {
                int ret = 0;
                if (upperPartController.IsAttacking)
                    ret++;
                if (lowerPartController.IsAttacking)
                    ret++;
                if (behindPartController.IsAttacking)
                    ret++;
                if (spawnPartController.IsAttacking)
                    ret++;
                if (body.IsAttacking)
                    ret++;
                return ret;
            }
        }

        public List<string> AvailableAttack
        {
            get
            {
                List<string> ret = new List<string>();
                if (State == OctopusState.State1)
                {
                    if (!upperPartController.IsAttacking)
                        ret.Add("Upper");
                    if (!lowerPartController.IsAttacking)
                        ret.Add("Grab");
                    if (!behindPartController.IsAttacking)
                        ret.Add("Behind");
                    if (!spawnPartController.IsAttacking)
                        ret.Add("Spawn");
                    if (!body.IsAttacking)
                        ret.Add("Laser");
                }
                else
                {
                    if (!upperPartController.IsAttacking)
                        ret.Add("Upper");
                    if (!lowerPartController.IsAttacking)
                        ret.Add("Grab");
                    if (!behindPartController.IsAttacking)
                        ret.Add("Behind");
                    if (!body.IsAttacking)
                        ret.Add("Laser");
                }

                return ret;
            }
        }

        public Stats Stats => enemyStats;

        public int attackIntervalState1 = 5;
        public int attackIntervalState2 = 3;
        private string[] attackPoolState1 = { "Grab", "Upper", "Laser", "Behind", "Spawn" };
        private string[] attackPoolState2 = { "Grab", "Upper", "Laser", "Behind" };
        public int maxState1ActiveAttack = 2;
        public int maxState2ActiveAttack = 3;

        private IEnumerator AttackCycle()
        {
            while (true)
            {
                if (CurrentActiveAttack >= (State == OctopusState.State1 ? maxState1ActiveAttack : maxState2ActiveAttack))
                {
                    yield return null;
                }
                yield return new WaitForSeconds(State == OctopusState.State1 ? attackIntervalState1 / 2 : attackIntervalState2 / 2);
                int maxAttack = State == OctopusState.State1 ? maxState1ActiveAttack : maxState2ActiveAttack;
                int currentAttack = CurrentActiveAttack;
                int remainAttack = maxAttack - currentAttack;

                int numAttacks = UnityEngine.Random.Range(1, 2);


                // Perform attacks
                for (int i = 0; i < numAttacks; i++)
                {
                    Attack(AvailableAttack.GetRandom());
                }
                yield return new WaitForSeconds(State == OctopusState.State1 ? attackIntervalState1 / 2 : attackIntervalState2 / 2); // Wait for 10 
            }
        }

        public void ApplyStats()
        {

        }
    }
}