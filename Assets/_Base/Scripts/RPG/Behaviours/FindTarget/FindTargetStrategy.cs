using System;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.FindTarget
{
    [Serializable]
    public abstract class FindTargetStrategy: MonoBehaviour
    {
        // public abstract bool IsTarget(GameObject gameObject);
        public abstract Entity FindTheMostTarget(FindTargetBehaviour findTargetBehaviour);

        public abstract Entity GetTarget(GameObject collisionGameObject);
    }
}