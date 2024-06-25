using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
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
