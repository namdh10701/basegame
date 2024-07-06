using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts.Battle;
using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.GD;
using MBT;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public abstract class Enemy : Entity, IEffectTaker, ISlowable
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




        [Header("Enemy")]
        [SerializeField] string enemyId;
        [SerializeField] protected EnemyStats _stats;
        [SerializeField] private EnemyStatsTemplate _statsTemplate;

        [Header("Behaviour")]
        [SerializeField] public EffectTakerCollider EffectTakerCollider;
        [SerializeField] protected EffectHandler effectHandler;
        [SerializeField] protected Collider2D pushCollider;
        [SerializeField] protected Blackboard blackboard;
        [SerializeField] protected MBTExecutor MBTExecutor;

        [SerializeField] protected SpineAnimationEnemyHandler spineAnimationEnemyHandler;
        public override Stats Stats => _stats;
        public Transform Transform => transform;
        public EffectHandler EffectHandler => effectHandler;

        public List<Stat> SlowableStats => new List<Stat>() { _stats.MoveSpeed, _stats.AnimationTimeScale };
        public ObjectCollisionDetector FindTargetCollider;
        protected virtual IEnumerator Start()
        {
            EffectTakerCollider.Taker = this;
            Ship ship = FindAnyObjectByType<Ship>();
            if (ship == null || blackboard == null)
                yield break;
            blackboard.GetVariable<ShipVariable>("Ship").Value = ship;
            MBTExecutor.enabled = false;
            yield return StartActionCoroutine();
            MBTExecutor.enabled = true;

            _stats.AttackDamage.BaseValue *= (1 + dmgModifer[EnemyManager.stageId]);
            _stats.HealthPoint.StatValue.BaseValue *= (1 + hpModifer[EnemyManager.stageId]);
            _stats.HealthPoint.MaxStatValue.BaseValue *= (1 + hpModifer[EnemyManager.stageId]);
        }
        public virtual void Die()
        {
            body.velocity = Vector3.zero;
            pushCollider.enabled = false;
            EffectTakerCollider.gameObject.SetActive(false);
            MBTExecutor.gameObject.SetActive(false);
        }

        public abstract IEnumerator StartActionCoroutine();
        public abstract bool IsReadyToAttack();
        public abstract IEnumerator AttackSequence();
        public abstract void Move();

        protected override void LoadStats()
        {
            if (GDConfigLoader.Instance == null)
            {
                _statsTemplate.ApplyConfig(_stats);
            }
            else
            {
                if (GDConfigLoader.Instance.Enemies.TryGetValue(enemyId, out EnemyConfig enemyConfig))
                {
                    enemyConfig.ApplyGDConfig(_stats);
                }
                else
                {
                    _statsTemplate.ApplyConfig(_stats);
                }
            }
        }

        protected override void ApplyStats()
        {
            FindTargetCollider?.SetRadius(_stats.AttackRange.Value);
        }

        protected override void LoadModifiers()
        {

        }

        public virtual void OnSlowed()
        {
            spineAnimationEnemyHandler.OnSlowedDown();
        }

        public virtual void OnSlowEnded()
        {
            spineAnimationEnemyHandler.OnSlowEnded();
        }
    }
}
