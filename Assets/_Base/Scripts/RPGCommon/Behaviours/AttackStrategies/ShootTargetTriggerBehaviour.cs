using _Base.Scripts.RPG.Attributes;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public abstract class ShootTargetTriggerBehaviour: MonoBehaviour
    {
        public ShootTargetBehaviour shootTargetBehaviour;

        public FireRate fireRate;

        public abstract void Pull();
        public abstract void Release();

    }
}