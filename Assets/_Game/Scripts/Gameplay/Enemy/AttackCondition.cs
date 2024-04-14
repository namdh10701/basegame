using _Base.Scripts.RPG.Entities;
using Demo.Scripts.Canon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.EnemyBehaviour
{
    public class AttackCondition : MonoBehaviour, ICondition
    {
        [SerializeField] private CooldownBehaviour cooldownBehaviour;
        [SerializeField] private InRangeTrigger inRangeTrigger;

        private Entity target;
        public bool IsTargetInRange { get; private set; }
        public bool IsInCooldown => cooldownBehaviour.IsInCooldown;
        public bool CanAttack => IsTargetInRange && !IsInCooldown;

        public void Initialize()
        {
            inRangeTrigger.AddListener(OnInRange, OnOutRange);
        }

        public void SetTarget(Entity target)
        {
            this.target = target;
            inRangeTrigger.SetTarget(target.transform);
        }

        void OnInRange()
        {
            IsTargetInRange = true;
        }
        void OnOutRange()
        {
            IsTargetInRange = false;
        }

        public void Attack()
        {
            cooldownBehaviour.StartCooldown();
        }

        public bool IsMet()
        {
            return CanAttack;
        }
    }
}
