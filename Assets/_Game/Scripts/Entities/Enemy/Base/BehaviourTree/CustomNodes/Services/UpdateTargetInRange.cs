using _Base.Scripts.RPG.Behaviours.FindTarget;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("My Services/Update Target In Range")]
    public class UpdateTargetInRange : Service
    {
        public BoolReference IsTargetInRange;
        public FindTargetBehaviour FindTargetBehaviour;
        public override void Task()
        {
            IsTargetInRange.Value = FindTargetBehaviour.MostTargets.Count > 0;
        }
    }
}
