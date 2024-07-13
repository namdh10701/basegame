using System;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.AttackTarget
{
    public interface IAttackStrategy
    {
        public Stat NumOfProjectile { get; set; }
        void SetData(Cannon shooter, Transform shootPosition, CannonProjectile projectilePrefab, Vector3 shootDirection);
        void DoAttack();
        void Consume(RangedStat ammo);
    }
    [Serializable]
    public abstract class AttackStrategy : MonoBehaviour, IAttackStrategy
    {
        public Stat numOfProjectile;
        public Stat NumOfProjectile { get => numOfProjectile; set => numOfProjectile = value; }

        public int ActualNumOfProjectile;

        public abstract void SetData(Cannon shooter, Transform shootPosition, CannonProjectile projectilePrefab,
            Vector3 targetPosition);

        public abstract void DoAttack();
        public abstract void Consume(RangedStat ammo);
    }
}