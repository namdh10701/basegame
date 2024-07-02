using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class Squid : Enemy
    {
        [Header("Squid")]
        [Space]
        public CooldownBehaviour CooldownBehaviour;
        public SquidAnimation anim;
        public EvasionBuffArea EvasionBuffArea;
        protected override IEnumerator Start()
        {
            anim.OnAction.AddListener(DoAction);
            MoveAreaController moveArea = FindAnyObjectByType<MoveAreaController>();
            Area area = moveArea.GetCloset(transform.position);
            blackboard.GetVariable<AreaVariable>("MoveArea").Value = area;
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

        public BehaviourTree.Wander wander;
        public override IEnumerator StartActionCoroutine()
        {
            wander = MBTExecutor.GetComponent<BehaviourTree.Wander>();
            pushCollider.enabled = false;
            EffectTakerCollider.enabled = false;
            anim.Appear();
            yield return new WaitForSeconds(1.5f);
            float rand = Random.Range(0, 1f);
            if (rand < .5f)
            {
                wander.ToLeft();
            }
            else
            {
                wander.ToRight();
            }
            wander.UpdateTargetDirection(-50, 50);
            pushCollider.enabled = true;
            EffectTakerCollider.enabled = true;
            CooldownBehaviour.SetCooldownTime(_stats.ActionSequenceInterval.Value);
            CooldownBehaviour.StartCooldown();
        }
        public override void Die()
        {
            base.Die();
            anim.PlayDie(() => Destroy(gameObject));
        }
        public void DoAction()
        {
            EvasionBuffArea buff = Instantiate(EvasionBuffArea);
            buff.transform.position = transform.position;
        }
    }
}