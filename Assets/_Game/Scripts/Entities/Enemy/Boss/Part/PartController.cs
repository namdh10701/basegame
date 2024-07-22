using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PartController : MonoBehaviour
{
    public Action OnAttackEnded;
    public Action OnAttackStarted;

    public bool IsAttacking;
    public virtual void StartAttack()
    {
        IsAttacking = true;
        OnAttackStarted?.Invoke();
    }


    public virtual void StopAttack()
    {
        IsAttacking = false;
        OnAttackEnded.Invoke();
    }

    public abstract IEnumerator TransformCoroutine();

}
