using _Base.Scripts.RPG.Behaviours.AimTarget;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public abstract class ShootTargetTriggerBehaviour : MonoBehaviour
    {
        public AttackTargetBehaviour AttackTargetBehaviour;
        public AimTargetBehaviour AimTargetBehaviour;
        protected Stat fireRate;

        public Cannon Cannon;

        public abstract void Pull();
        public abstract void Release();

        protected virtual void Awake()
        {
            fireRate = ((CannonStats)Cannon.Stats).AttackSpeed;
        }
    }
}