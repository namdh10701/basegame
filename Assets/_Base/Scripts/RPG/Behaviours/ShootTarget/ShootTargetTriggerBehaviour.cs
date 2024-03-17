using _Base.Scripts.RPG.Attributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Base.Scripts.RPG.Behaviours.ShootTarget
{
    public abstract class ShootTargetTriggerBehaviour: MonoBehaviour
    {
        public ShootTargetBehaviour shootTargetBehaviour;

        public FireRate fireRate;

        public abstract void Pull();
        public abstract void Release();

    }
}