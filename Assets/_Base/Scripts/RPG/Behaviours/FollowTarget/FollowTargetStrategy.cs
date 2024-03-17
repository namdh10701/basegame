using System;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.FollowTarget
{
    [Serializable]
    public abstract class FollowTargetStrategy: MonoBehaviour
    {
        public abstract bool Follow(FindTargetBehaviour findTargetBehaviour);

        // public static FollowTargetStrategy Rotate = new Rotate();
    }
}