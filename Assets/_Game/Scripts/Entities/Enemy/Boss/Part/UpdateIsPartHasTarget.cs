using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("Giant Octopus/Update Is Has Target")]
    public class UpdateIsPartHasTarget : Service
    {
        public BoolReference IsHasTarget;
        public FindTargetBehaviour FindTargetBehaviour;
        public override void Task()
        {
            IsHasTarget.Value = FindTargetBehaviour.MostTargets.Count > 0;
        }
    }
}
