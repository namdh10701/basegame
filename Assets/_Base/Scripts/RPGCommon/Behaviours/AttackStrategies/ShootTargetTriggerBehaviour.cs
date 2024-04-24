using System;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public abstract class ShootTargetTriggerBehaviour : MonoBehaviour
    {
        public AttackTargetBehaviour AttackTargetBehaviour;

        public FireRate fireRate;

        public RangedStat Ammo;

        public abstract void Pull();
        public abstract void Release();

        private void Start()
        {
            Ammo = ((CannonStats)((AttackTargetBehaviour.entity as Cannon).Stats)).Ammo;
        }
    }
}