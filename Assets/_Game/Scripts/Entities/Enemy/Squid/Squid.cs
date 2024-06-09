using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class Squid : Enemy
    {
        public CooldownBehaviour CooldownBehaviour;
        public SquidAnimation anim;
        protected override IEnumerator Start()
        {
            anim.OnAction.AddListener(DoAction);
            CooldownBehaviour.SetCooldownTime(7f);
            CooldownBehaviour.StartCooldown();
            yield return base.Start();
        }
        public override IEnumerator AttackSequence()
        {
            anim.PlayAttack();
            yield return new WaitForSeconds(2);
            CooldownBehaviour.StartCooldown();
        }

        public override bool IsReadyToAttack()
        {
            return !CooldownBehaviour.IsInCooldown;
        }

        public override void Move()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator StartActionCoroutine()
        {
            anim.PlayIdle();
            yield break;
        }
        public void DoAction()
        {

        }
    }
}