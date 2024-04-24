using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class NormalAttack : AttackStrategy
    {
        public override void DoAttack()
        {

        }

        public override void SetData(Entity shooter, Transform shootPosition, Entity projectilePrefab, FindTargetStrategy findTargetStrategy, Vector3 targetPosition)
        {
        }
    }
}
