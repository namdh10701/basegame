using _Base.Scripts.RPG.Behaviours.AttackTarget;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : AttackTargetBehaviour
{
    public float ChargeTime;
    public override void PlayAttackAnimation()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        Animation.PlayChargeAnim();
        yield return new WaitForSeconds(ChargeTime);
        Animation.PlayShootAnim(false);
    }
}
