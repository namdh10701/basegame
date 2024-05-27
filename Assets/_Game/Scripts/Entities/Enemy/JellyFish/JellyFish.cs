using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish : Enemy
{
    public RangedAttack rangedAttack;
    public CooldownBehaviour CooldownBehaviour;
    public JellyFishAnimation anim;
    public Transform spawnPosition;
    protected override void Awake()
    {
        base.Awake();
        anim.Attack.AddListener(AttackLeftHand);
        anim.AttackCharge.AddListener(ChargeAttack);
    }
    protected override IEnumerator Start()
    {
        CooldownBehaviour.SetCooldownTime(7f);
        CooldownBehaviour.StartCooldown();
        yield return base.Start();
    }
    public override IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(2);
        CooldownBehaviour.StartCooldown();
    }

    public override bool IsReadyToAttack()
    {
        return !CooldownBehaviour.IsInCooldown;
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

    public void AttackLeftHand(Transform shootPos)
    {
        rangedAttack.ShootPos = shootPos;
        rangedAttack.DoAttack();
    }
    public void ChargeAttack()
    {
        rangedAttack.SelectCells();
    }


}
