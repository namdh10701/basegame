using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using System.Collections;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class SquidModel : EnemyModel
    {
        [Header("Squid")]
        [Space]
        public CooldownBehaviour CooldownBehaviour;
        public EvasionBuffArea EvasionBuffArea;
        protected override void Awake()
        {
            base.Awake();
            MoveAreaController moveArea = FindAnyObjectByType<MoveAreaController>();
            blackboard.GetVariable<AreaVariable>("MoveArea").Value = moveArea.GetArea(AreaType.All);
            _Game.Scripts.BehaviourTree.Wander wander = mbtExecutor.GetComponent<_Game.Scripts.BehaviourTree.Wander>();
            float rand = Random.Range(0, 1f);
            if (rand < .5f)
            {
                wander.ToLeft();

            }
            else
            {
                wander.ToRight();

            }
            wander.UpdateTargetDirection(-50, 50);
            cooldownBehaviour.StartCooldown();
        }
        public override void ApplyStats()
        {
            base.ApplyStats();
            EvasionBuffArea.SetRange(_stats.AttackRange.Value);
        }

        public override IEnumerator AttackSequence()
        {
            enemyView.PlayAttack();
            yield break;
        }

        public override void DoAttack()
        {
            cooldownBehaviour.StartCooldown();
        }

        void SpawnSkill()
        {
            EvasionBuffArea a = Instantiate(EvasionBuffArea, null);
            a.transform.position = transform.position;
            a.gameObject.SetActive(true);
        }
    }
}