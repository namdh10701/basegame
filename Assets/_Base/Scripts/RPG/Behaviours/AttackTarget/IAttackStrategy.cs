using System;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.AttackTarget
{
    public interface IAttackStrategy
    {
        public int DefaultNumOfProjectile { get; }
        public int NumOfProjectile { get; set; }
        void SetData(Entity shooter, Transform shootPosition, Entity projectilePrefab, Vector3 shootDirection);
        void DoAttack();
        void Consume(RangedStat ammo);
    }
    [Serializable]
    public abstract class AttackStrategy : MonoBehaviour, IAttackStrategy
    {
        public int defaultNumOfProjectile;
        int numOfProjectile;
        public int NumOfProjectile { get => numOfProjectile; set => numOfProjectile = value; }
        public int DefaultNumOfProjectile { get => defaultNumOfProjectile; }
        public abstract void SetData(Entity shooter, Transform shootPosition, Entity projectilePrefab,
            Vector3 targetPosition);

        public abstract void DoAttack();
        public abstract void Consume(RangedStat ammo);
    }
}