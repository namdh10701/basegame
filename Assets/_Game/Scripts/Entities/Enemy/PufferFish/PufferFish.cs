using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Gameplay.Ship;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{

    public class PufferFish : Enemy
    {
        [Header("Puffer Fish")]
        [Space]

        PufferFishAnimation Animation;
        public PufferFishMove PufferFishMove;
        public DamageArea DamageArea;
        protected override IEnumerator Start()
        {
            Animation = (PufferFishAnimation)spineAnimationEnemyHandler;
            Animation.OnAttack.AddListener(DoAttack);
            yield return base.Start();
        }
        protected override void ApplyStats()
        {
            base.ApplyStats();
        }
        bool isAttacking;
        public override IEnumerator AttackSequence()
        {
            if (!isAttacking)
            {
                MBTExecutor.enabled = false;
                body.velocity = Vector2.zero;
                Invoke("ReducePoise", .3f);
                Destroy(PufferFishMove);
                isAttacking = true;
                Animation.ChargeExplode();
                yield return new WaitForSeconds(2);

                Animation.PlayDie(() =>
                {
                    base.Die();
                    Destroy(gameObject);
                });

            }
        }

        void ReducePoise()
        {
            _stats.Poise.BaseValue = 0;
        }


        public override bool IsReadyToAttack()
        {
            return true;
        }

        public override void Move()
        {
            PufferFishMove?.Move();
        }

        public override IEnumerator StartActionCoroutine()
        {
            transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(1);
            Tween tween = transform.DOScale(1, .5f).SetEase(Ease.OutBack);
            yield return tween.WaitForCompletion();
        }
        public override void Die()
        {
            Destroy(PufferFishMove);
            body.velocity = Vector2.zero;
            StartCoroutine(AttackSequence());
        }
        public AttackPatternProfile pufferFish;
        public void DoAttack()
        {
            pushCollider.gameObject.SetActive(false);
            EffectTakerCollider.gameObject.SetActive(false);
            DamageArea da = Instantiate(DamageArea, transform.position, Quaternion.identity);
            da.SetRange(_stats.AttackRange.Value);
            da.SetDamage(_stats.AttackDamage.Value, 0);

            RaycastHit2D[] inRangeColliders = Physics2D.CircleCastAll(transform.position, _stats.AttackRange.Value, Vector2.zero, LayerMask.NameToLayer("Ship"));
            Debug.LogError(inRangeColliders.Length);
            if (inRangeColliders != null && inRangeColliders.Length > 0)
            {
                bool found = false;
                foreach (var inRange in inRangeColliders)
                {
                    if (inRange.collider.TryGetComponent(out EntityProvider entityProvider))
                    {
                        if (entityProvider.Entity is Ship)
                        {
                            found = true;
                        }
                    }
                }

                if (found)
                {
                    GridAttackHandler atk = FindAnyObjectByType<GridAttackHandler>();
                    GridPicker gp = FindAnyObjectByType<GridPicker>();
                    List<Cell> cells = gp.PickCells(transform, pufferFish, out Cell centerCell);
                    EnemyAttackData enemyAtk = new EnemyAttackData();
                    enemyAtk.CenterCell = centerCell;
                    enemyAtk.TargetCells = cells;
                    DecreaseHealthEffect dhe = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
                    dhe.transform.position = centerCell.transform.position;
                    dhe.Amount = _stats.AttackDamage.Value;

                    enemyAtk.Effects = new List<Effect> { dhe };
                    atk.ProcessAttack(enemyAtk);
                }
            }

        }
    }
}