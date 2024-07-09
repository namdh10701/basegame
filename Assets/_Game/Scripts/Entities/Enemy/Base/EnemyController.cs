using _Base.Scripts.RPG;
using _Game.Scripts.Battle;
using _Game.Scripts;
using _Game.Scripts.Entities;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public abstract class EnemyController : MonoBehaviour
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

        protected EnemyModel enemyModel;
        protected Blackboard blackboard;
        protected MBTExecutor mbtExecutor;
        protected EffectTakerCollider effectTakerCollider;
        protected Rigidbody2D body;
        protected SpineAnimationEnemyHandler anim;

        public Rigidbody2D Body => body;

        public abstract bool IsReadyToAttack();
        public virtual void Initialize(EnemyModel enemyModel, EffectTakerCollider effectTakerCollider, Blackboard blackboard, MBTExecutor mbtExecutor, Rigidbody2D body, SpineAnimationEnemyHandler anim)
        {
            this.enemyModel = enemyModel;
            this.blackboard = blackboard;
            this.effectTakerCollider = effectTakerCollider;
            this.mbtExecutor = mbtExecutor;
            this.body = body;
            this.anim = anim;

            effectTakerCollider.Taker = enemyModel;
            Ship ship = FindAnyObjectByType<Ship>();
            blackboard.GetVariable<ShipVariable>("Ship").Value = ship;

            EnemyStats _stats = enemyModel.Stats as EnemyStats;

            blackboard.GetVariable<StatVariable>("MoveSpeed").Value = _stats.MoveSpeed;
            _stats.AttackDamage.BaseValue *= (1 + dmgModifer[EnemyManager.stageId]);
            _stats.HealthPoint.StatValue.BaseValue *= (1 + hpModifer[EnemyManager.stageId]);
            _stats.HealthPoint.MaxStatValue.BaseValue *= (1 + hpModifer[EnemyManager.stageId]);
            StartCoroutine(OnInitializedCompleted());
        }

        protected virtual IEnumerator OnInitializedCompleted()
        {
            mbtExecutor.enabled = false;
            yield return StartActionCoroutine();
            mbtExecutor.enabled = true;
        }

        public abstract IEnumerator StartActionCoroutine();
        public abstract IEnumerator AttackSequence();
        public void OnSlowedDown()
        {

        }
        public void OnSlowedDownEnded()
        {

        }

        public virtual void Die()
        {
            enemyModel.EffectHandler.Clear();
            body.velocity = Vector3.zero;
            effectTakerCollider.gameObject.SetActive(false);
            mbtExecutor.gameObject.SetActive(false);
        }
    }
}