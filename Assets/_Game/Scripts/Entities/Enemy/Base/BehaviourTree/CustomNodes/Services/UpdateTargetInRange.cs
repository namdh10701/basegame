using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("My Services/Update Is Has Target")]
    public class UpdateTargetInRange : Service
    {
        public BoolReference IsHasTarget;
        public EnemyReference enemyReference;
        public FindTargetBehaviour FindTargetBehaviour;
        public override void Task()
        {
            if (FindTargetBehaviour == null)
                IsHasTarget.Value = enemyReference.Value.HasTarget();
            else
                IsHasTarget.Value = FindTargetBehaviour.MostTargets.Count > 0;
        }
    }
}
