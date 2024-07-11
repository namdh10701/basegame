using _Base.Scripts.RPG.Behaviours.FindTarget;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class PhunGaiController : EnemyController
    {
        public CooldownBehaviour cooldownBehaviour;
        public FindTargetBehaviour FindTargetBehaviour;
        public override IEnumerator AttackSequence()
        {
            yield break;
        }

        public override bool IsReadyToAttack()
        {
            return !cooldownBehaviour.IsInCooldown && FindTargetBehaviour.MostTargets.Count > 0;
        }

        public override IEnumerator StartActionCoroutine()
        {
            yield break;
        }

    }
}