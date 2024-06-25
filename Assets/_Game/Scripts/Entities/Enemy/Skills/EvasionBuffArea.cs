
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class EvasionBuffArea : MonoBehaviour
    {
        public StatModifier statModifier = new StatModifier(.33f, StatModType.Flat);
        List<Enemy> buffedEnemy = new List<Enemy>();

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
            if (collision.TryGetComponent(out EntityProvider entityProvider))
            {
                if (entityProvider.Entity is Enemy enemy)
                {
                    if (!buffedEnemy.Contains(enemy))
                    {
                        ((EnemyStats)enemy.Stats).EvadeChance.AddModifier(statModifier);
                        buffedEnemy.Add(enemy);
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out EntityProvider entityProvider))
            {
                if (entityProvider.Entity is Enemy enemy)
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