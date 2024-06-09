using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace _Game.Scripts
{
    public class EnemyCollisionHandler : DefaultCollisionHandler
    {
        public EnemyStats stats;
        public EnemyCollisionHandler(EnemyStats stats)
        {
            this.stats = stats;
        }

        public override void Process(Entity mainEntity, Entity collidedEntity)
        {
            if (stats == null)
                throw new System.Exception();
            float evasion = stats.EvasionChance.Value;
            float chance = Random.Range(0, 1f);
            if (chance < evasion)
            {
                return;
            }
            else
            {
                base.Process(mainEntity, collidedEntity);
            }
        }
    }
}