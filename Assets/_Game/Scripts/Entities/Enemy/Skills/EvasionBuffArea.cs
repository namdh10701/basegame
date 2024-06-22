
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class EvasionBuffArea : MonoBehaviour
    {
        StatModifier statModifier = new StatModifier(.33f, StatModType.Flat);
        List<Enemy> buffedEnemy = new List<Enemy>();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out EntityProvider entityProvider))
            {
                if (entityProvider.Entity is Enemy enemy)
                {
                    if (!buffedEnemy.Contains(enemy))
                    {
                        ((EnemyStats)enemy.Stats).EvadeChance.AddModifier(statModifier);
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
                    }
                }
            }
        }
    }
}