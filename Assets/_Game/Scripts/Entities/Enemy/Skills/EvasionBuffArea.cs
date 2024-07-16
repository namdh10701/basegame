
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class EvasionBuffArea : MonoBehaviour
    {
        public StatModifier statModifier = new StatModifier(.33f, StatModType.Flat);
        List<EnemyModel> buffedEnemy = new List<EnemyModel>();
        public void SetRange(float range)
        {
            transform.localScale = new Vector3(range, range);
        }
        public float existTime;
        float elapsedTime;

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > existTime)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IEffectTakerCollider entityProvider))
            {
                if (entityProvider.Taker is EnemyModel enemy)
                {
                    if (!buffedEnemy.Contains(enemy))
                    {
                        EnemyStats enemyStats = enemy.Stats as EnemyStats;
                        if (enemyStats.EvadeChance.StatModifiers.Count == 0)
                        {
                            enemyStats.EvadeChance.AddModifier(statModifier);

                            buffedEnemy.Add(enemy);
                        }
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IEffectTakerCollider entityProvider))
            {
                if (entityProvider.Taker is EnemyModel enemy)
                {
                    if (buffedEnemy.Contains(enemy))
                    {
                        ((EnemyStats)enemy.Stats).EvadeChance.RemoveModifier(statModifier);
                        buffedEnemy.Remove(enemy);
                    }
                }
            }
        }
    }
}