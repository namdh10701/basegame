
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{

    public class PufferFishModel : EnemyModel
    {
        [Header("Puffer Fish")]
        [Space]
        public PufferFishMove PufferFishMove;
        public DamageArea DamageArea;

        public string SortLayer { set { 
                enemyView.SortLayer = value;     
            } }
       
        public GameObject pufferFishCollider;
        public override void OnSlowed()
        {
            base.OnSlowed();
            PufferFishMove.SetSpeed(.5f);
        }

        public override void OnSlowEnded()
        {
            base.OnSlowEnded();
            PufferFishMove.SetSpeed(1);
        }
        public override void ApplyStats()
        {
            EnemyStats stats = Stats as EnemyStats;
            DamageArea.SetRange(stats.AttackRange.Value);
            DamageArea.SetDamage(stats.AttackDamage.Value, 0);
        }

        public override void DoAttack()
        {
            effectTakerCollider.gameObject.SetActive(false);
            DamageArea da = Instantiate(DamageArea, transform.position, Quaternion.identity);
            da.gameObject.SetActive(true);
            RaycastHit2D[] inRangeColliders = Physics2D.CircleCastAll(transform.position, _stats.AttackRange.Value, Vector2.zero, LayerMask.NameToLayer("Ship"));
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
                    List<Cell> cells = gp.PickCells(transform, attackPatternProfile, out Cell centerCell);
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

        public override void Die()
        {
            StartCoroutine(AttackSequence());
        }

        public override IEnumerator AttackSequence()
        {
            mbtExecutor.enabled = false;
            body.velocity = Vector2.zero;
            ChargingState = ChargeState.Charging;
            yield return new WaitForSeconds(2);
            base.Die();
        }
    }
}