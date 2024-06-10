using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish : Enemy
{
    public JellyFishAttack rangedAttack;
    public CooldownBehaviour CooldownBehaviour;
    public JellyFishAnimation anim;
    public Transform spawnPosition;

    bool isCurrentAttackLeftHand;
    protected override void Awake()
    {
        base.Awake();
        anim.Attack.AddListener(Attack);
    }
    protected override IEnumerator Start()
    {
        CooldownBehaviour.SetCooldownTime(7f);
        CooldownBehaviour.StartCooldown();
        yield return base.Start();
    }
    public override IEnumerator AttackSequence()
    {

        if (isCurrentAttackLeftHand)
        {
            anim.PlayIdleToAttackLoopLeftHand();
        }
        else
        {
            anim.PlayIdleToAttackLoopRightHand();
        }
        yield return new WaitForSeconds(2);
        if (isCurrentAttackLeftHand)
        {
            anim.PlayAttackLeftHand();
        }
        else
        {
            anim.PlayAttackRightHand();
        }
        CooldownBehaviour.StartCooldown();
    }

    public override bool IsReadyToAttack()
    {
        return !CooldownBehaviour.IsInCooldown;
    }
    public override void Die()
    {
        base.Die();
        anim.PlayDie(() => Destroy(gameObject));
    }
    public override void Move()
    {
    }

    public override IEnumerator StartActionCoroutine()
    {
        transform.position = spawnPosition.position;
        anim.Appear();
        yield return new WaitForSeconds(3);
    }

    public void Attack(Transform shootPos)
    {
        if (isCurrentAttackLeftHand)
            rangedAttack.DoLeftAttack();
        else
            rangedAttack.DoRightAttack();
        isCurrentAttackLeftHand = !isCurrentAttackLeftHand;
    }
}
