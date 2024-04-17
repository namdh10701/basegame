using System;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.AttackTarget
{
    public interface IAttackStrategy
    {
        void SetData(Entity shooter, Transform shootPosition, Entity projectilePrefab, FindTargetStrategy findTargetStrategy, Vector3 targetPosition);
        void DoAttack();
    }

    [Serializable]
    public abstract class AttackStrategy : MonoBehaviour, IAttackStrategy
    {
        public abstract void SetData(Entity shooter, Transform shootPosition, Entity projectilePrefab, FindTargetStrategy findTargetStrategy,
            Vector3 targetPosition);

        public abstract void DoAttack();
    }
}