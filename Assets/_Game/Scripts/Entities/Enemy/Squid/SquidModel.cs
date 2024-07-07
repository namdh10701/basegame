using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class SquidModel : EnemyModel
    {
        [Header("Squid")]
        [Space]
        public CooldownBehaviour CooldownBehaviour;
        public EvasionBuffArea EvasionBuffArea;
        public override void ApplyStats()
        {
            CooldownBehaviour.SetCooldownTime(_stats.ActionSequenceInterval.Value);
        }
    }
}