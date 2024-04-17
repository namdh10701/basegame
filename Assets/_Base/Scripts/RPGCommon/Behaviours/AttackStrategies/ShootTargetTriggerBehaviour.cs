using System;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public abstract class ShootTargetTriggerBehaviour: MonoBehaviour
    {
        public AttackTargetBehaviour AttackTargetBehaviour;

        public FireRate fireRate;

        public abstract void Pull();
        public abstract void Release();

        private void Start()
        {
        }
    }
}