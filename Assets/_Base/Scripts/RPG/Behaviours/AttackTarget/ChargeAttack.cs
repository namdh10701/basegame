using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Game.Features.Gameplay;
using System.Collections;
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
        if (entity.FeverState != CannonFeverState.None)
        {
            yield return new WaitForSeconds(ChargeTime / 1.5f);
        }
        else
        {
            yield return new WaitForSeconds(ChargeTime);
        }

        Animation.PlayShootAnim(false);
    }
}

