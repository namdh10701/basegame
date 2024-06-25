using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish : Enemy
{
    public JellyFishAttack attack;
    public CooldownBehaviour CooldownBehaviour;
    public JellyFishAnimation anim;
    bool isCurrentAttackLeftHand;

    protected override void Awake()
    {
        base.Awake();
        MoveAreaController moveArea = FindAnyObjectByType<MoveAreaController>();
        Area area = moveArea.GetArea(AreaType.All);
        blackboard.GetVariable<AreaVariable>("MoveArea").Value = area;
        anim.Attack.AddListener(Attack);
        anim.AttackMeele.AddListener(AttackMelee);
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
    public TargetInRange targetInRange;
    public bool IsReadyMelleAttack()
    {
        return targetInRange.IsMet;
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
        anim.Appear();
        yield return new WaitForSeconds(4.5f);
    }

    public void Attack()
    {
        if (isCurrentAttackLeftHand)
            attack.DoLeftAttack();
        else
            attack.DoRightAttack();
        isCurrentAttackLeftHand = !isCurrentAttackLeftHand;
    }

    public void AttackMelee()
    {
        if (isAttackLefthand)
        {
            attack.DoLeftMeleeAttack();
        }
        else
        {
            attack.DoRightMelleAttack();
        }
    }

    public IEnumerator MeleeAttackSequence()
    {
        float rand = Random.Range(0, 1f);
        if (rand < .5f)
        {
            yield return LeftAttackSequence();
        }
        else
        {
            yield return RightAttackSequence();
        }
    }
    bool isAttackLefthand;
    IEnumerator LeftAttackSequence()
    {
        CooldownBehaviour.StartCooldown();
        isAttackLefthand = true;
        anim.PlayAttackMeeleLeftHand();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator RightAttackSequence()
    {
        CooldownBehaviour.StartCooldown();
        isAttackLefthand = false;
        anim.PlayAttackMeeleRightHand();
        yield return new WaitForSeconds(1f);
    }
}
