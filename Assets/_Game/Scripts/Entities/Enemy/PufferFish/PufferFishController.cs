using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using DG.Tweening;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFishController : EnemyController
{
    PufferFishAnimation pufferFishAnim;
    public PufferFishMove pufferFishMove;
    public DamageArea damageArea;

    public override void Initialize(EnemyModel enemyModel, EffectTakerCollider effectTakerCollider, Blackboard blackboard, MBTExecutor mbtExecutor, Rigidbody2D body, SpineAnimationEnemyHandler anim)
    {
        base.Initialize(enemyModel, effectTakerCollider, blackboard, mbtExecutor, body, anim);
        pufferFishAnim = base.anim as PufferFishAnimation;
        pufferFishAnim.OnAttack.AddListener(DoAttack);

    }
    bool isAttacking;
    public override IEnumerator AttackSequence()
    {
        if (!isAttacking)
        {
            mbtExecutor.enabled = false;
            body.velocity = Vector2.zero;
            Invoke("ReducePoise", .3f);
            isAttacking = true;
            pufferFishAnim.ChargeExplode();
            yield return new WaitForSeconds(2);

            pufferFishAnim.PlayDie(() =>
            {
                base.Die();
                Destroy(gameObject);
            });

        }
    }

    void ReducePoise()
    {
        EnemyStats stats = enemyModel.Stats as EnemyStats;
        stats.Poise.BaseValue = 0;
    }

    public void Move()
    {
        pufferFishMove.Move();
    }

    public override bool IsReadyToAttack()
    {
        return true;
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
        body.velocity = Vector2.zero;
        StartCoroutine(AttackSequence());
    }
    public AttackPatternProfile pufferFish;
    public void DoAttack()
    {
        EnemyStats enemyStats = enemyModel.Stats as EnemyStats;
        effectTakerCollider.gameObject.SetActive(false);
        DamageArea da = Instantiate(damageArea, transform.position, Quaternion.identity);
        da.SetRange(enemyStats.AttackRange.Value);
        da.SetDamage(enemyStats.AttackDamage.Value, 0);

        RaycastHit2D[] inRangeColliders = Physics2D.CircleCastAll(transform.position, enemyStats.AttackRange.Value, Vector2.zero, LayerMask.NameToLayer("Ship"));
        if (inRangeColliders != null && inRangeColliders.Length > 0)
        {
            bool found = false;
            foreach (var inRange in inRangeColliders)
            {
                if (inRange.collider.TryGetComponent(out IEffectTakerCollider entityProvider))
                {
                    if (entityProvider.Taker is Ship)
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
                dhe.Amount = enemyStats.AttackDamage.Value;

                enemyAtk.Effects = new List<Effect> { dhe };
                atk.ProcessAttack(enemyAtk);
            }
        }

    }
}
