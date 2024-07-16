using System;
using System.Collections;
using System.Linq;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using _Game.Scripts.BehaviourTree;
using _Game.Scripts.Entities;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class ElectricEelModel : EnemyModel
    {
        [Header("Electric Eel")]
        [Space]
        public ElectricFx electricFx;

        [SerializeField] ElectricEelProjectile Projectile;
        [SerializeField] ObjectCollisionDetector FindTargetCollider;

        [SerializeField] Transform target;

        [Header("Sycn Animation")]
        [SerializeField] float syncAnimationPlayFxTime = .5f;
        [SerializeField] float syncAnimationSpawnProjectileTime = .8f;

        ElectricEelView eelView;
        protected override void Awake()
        {
            base.Awake();
            eelView = enemyView as ElectricEelView;
            MoveAreaController moveArea = FindAnyObjectByType<MoveAreaController>();
            blackboard.GetVariable<AreaVariable>("MoveArea").Value = moveArea.GetArea(AreaType.All);
            _Game.Scripts.BehaviourTree.Wander wander = mbtExecutor.GetComponent<_Game.Scripts.BehaviourTree.Wander>();
            float rand = UnityEngine.Random.Range(0, 1f);
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

        public override void DoAttack()
        {
            Invoke("PlayAttackFx", syncAnimationPlayFxTime);
            Invoke("SpawnProjectile", syncAnimationSpawnProjectileTime);
        }

        void PlayAttackFx()
        {
            electricFx.targetTransform = target;
            electricFx.Play();
        }

        void SpawnProjectile()
        {
            ElectricEelProjectile projectile = Instantiate(Projectile);
            projectile.transform.position = electricFx.transform.position;
            projectile.targetTransform = target;
            projectile.startTransform = transform;
            projectile.targetTransform = target;
        }

        public override IEnumerator AttackSequence()
        {
            if (findTargetBehaviour.MostTargets.Count == 0)
            {
                yield break;
            }
            else
            {
                Crew crew = findTargetBehaviour.MostTargets.First() as Crew;
                target = crew.EffectTakerCollider.transform;
            }
            chargeState = ChargeState.Charging;
            yield return new WaitForSeconds(2);
            enemyView.PlayAttack();
            cooldownBehaviour.StartCooldown();
            yield break;
        }
    }
}