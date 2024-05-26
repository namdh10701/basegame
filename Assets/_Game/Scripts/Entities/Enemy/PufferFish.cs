using _Game.Scripts.Battle;
using _Game.Scripts.Gameplay.Ship;
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
        public TargetInRange TargetInRange;
        public PufferFishMove PufferFishMove;
        protected override IEnumerator Start()
        {
            TargetInRange.TargetShip = FindAnyObjectByType<Ship>();
            yield return base.Start();
        }
        public override IEnumerator AttackSequence()
        {
            Animation.ChargeExplode();
            yield return new WaitForSeconds(2);
            Die();
            yield break;
        }


        public override bool IsReadyToAttack()
        {
            return true && TargetInRange.IsMet;
        }

        public override void Move()
        {
            PufferFishMove.Move();
        }

        public override IEnumerator StartActionCoroutine()
        {
            yield break;
        }
    }
}