using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using _Game.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class JellyFishModel : EnemyModel
    {
        [Header("Jelly Fish")]
        [Space]
        [SerializeField] JellyFishAttack attack;
        public CooldownBehaviour cooldownBehaviour;
        public ObjectCollisionDetector FindTargetCollider;

        public override Stats Stats => _stats;

        public override void ApplyStats()
        {
            base.ApplyStats();
            cooldownBehaviour.SetCooldownTime(_stats.ActionSequenceInterval.Value);
            FindTargetCollider.SetRadius(_stats.AttackRange.Value);
        }
    }
}