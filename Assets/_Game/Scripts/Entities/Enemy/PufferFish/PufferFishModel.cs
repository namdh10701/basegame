using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Gameplay.Ship;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{

    public class PufferFishModel : EnemyModel
    {
        [Header("Puffer Fish")]
        [Space]
        public PufferFishMove PufferFishMove;
        public DamageArea DamageArea;


        public override void ApplyStats()
        {
            base.ApplyStats();
            EnemyStats stats = Stats as EnemyStats;
            DamageArea.SetRange(stats.AttackRange.Value);
            DamageArea.SetDamage(stats.AttackDamage.Value, 0);
        }
    }
}