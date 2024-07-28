using _Game.Features.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperPartController : PartController
{
    public UpperPartModel left;
    public UpperPartModel right;

    private void Awake()
    {
        left.OnAttack += CheckStopAttackLeft;
        right.OnAttack += CheckStopAttackRight;
    }

    void CheckStopAttackLeft()
    {
        left.IsAttacking = false;
        if (!left.IsAttacking && !right.IsAttacking)
        {
            StopAttack();
        }
    }

    void CheckStopAttackRight()
    {
        right.IsAttacking = false;
        if (!left.IsAttacking && !right.IsAttacking)
        {
            StopAttack();
        }
    }
    public override void StartAttack()
    {
        left.stats.HealthPoint.StatValue.BaseValue = left.stats.HealthPoint.MaxValue;
        right.stats.HealthPoint.StatValue.BaseValue = right.stats.HealthPoint.MaxValue;
        float rand = Random.Range(0, 1f);
        float rand1 = Random.Range(0f, 2f);
        float rand2 = Random.Range(.75f, 2f);
        if (rand < .5f)
        {
            Invoke("StartAttackLeft", rand1);
            Invoke("StartAttackRight", rand1 + rand2);
        }
        else
        {
            Invoke("StartAttackLeft", rand1);
            Invoke("StartAttackRight", rand1 + rand2);
        }
        base.StartAttack();
    }
    public override void StopAttack()
    {
        left.IsAttacking = false;
        right.IsAttacking = false;
        left.State = PartState.Hidding;
        right.State = PartState.Hidding;
        base.StopAttack();
    }


    void StartAttackLeft()
    {
        left.IsAttacking = true;
    }

    void StartAttackRight()
    {
        right.IsAttacking = true;
    }

    public override IEnumerator TransformCoroutine()
    {
        Coroutine a = StartCoroutine(left.TransformCoroutine());
        Coroutine b = StartCoroutine(right.TransformCoroutine());
        yield return a;
        yield return b;
    }

    internal IEnumerator DeadCoroutine()
    {
        Coroutine a = StartCoroutine(left.DeadCoroutine());
        Coroutine b = StartCoroutine(right.DeadCoroutine());
        yield return a;
        yield return b;
    }
}
