using System;
using System.Collections.Generic;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.FindTarget
{
    [Serializable]
    public abstract class FindTargetStrategy: MonoBehaviour
    {
        // public abstract bool IsTarget(GameObject gameObject);
        public abstract List<EffectTakerCollider> FindTheMostTargets(List<EffectTakerCollider> foundTargets);

        // public abstract Entity GetTarget(GameObject collisionGameObject);
        public abstract bool TryGetTargetEntity(GameObject go, out EffectTakerCollider entity);
    }
}