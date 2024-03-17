using System;
using _Base.Scripts.RPG.Behaviours.FollowTarget;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.AimTarget
{
    [Serializable]
    public abstract class AimTargetStrategy: MonoBehaviour
    {
        public abstract bool Aim(FollowTargetBehaviour followTargetBehaviour);

        public abstract void Reset();
    }
}