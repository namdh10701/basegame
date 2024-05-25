using _Game.Scripts.Battle;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{

    public class PufferFish : Enemy
    {
        public PufferFishAnimation Animation;
        public Enemy Enemy;
        public IEnumerator AttackSequence()
        {
            Animation.ChargeExplode();
            yield return new WaitForSeconds(2);
            Die();
            yield break;
        }
    }
}