using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Behaviours.FindTargetStrategies;
using UnityEngine;

namespace _Game.Scripts.Behaviours.FindTarget
{
    [AddComponentMenu("RPG/FindTargetStrategy/[FindTargetStrategy] Closest enemy")]
    public class ClosestEnemy: Closest
    {
        public override bool TryGetTargetEntity(GameObject go, out Entity entity)
        {
            entity = null;
            if (!base.TryGetTargetEntity(go, out var found) || found is not Enemy)
            {
                return false;
            }

            entity = found;
            return true;
        }
    }
}