using System.Collections.Generic;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Behaviours.FindTargetStrategies;
using _Base.Scripts.Unity.EditorUsedAttributes;
using UnityEngine;

namespace _Game.Scripts.Behaviours.FindTarget
{
    [AddComponentMenu("RPG/FindTargetStrategy/[FindTargetStrategy] Closest enemy")]
    public class ClosestEnemy: Closest
    {
        [MonoScript(typeof(Entity))]
        public List<string> TargetTypeNames;
        public override bool TryGetTargetEntity(GameObject go, out Entity entity)
        {
            entity = null;
            if (!base.TryGetTargetEntity(go, out var found) || !TargetTypeNames.Contains(found.GetType().FullName))
            {
                return false;
            }
            entity = found;
            return true;
        }
    }
}