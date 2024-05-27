using System.Collections;
using _Game.Scripts.Entities;
using UnityEngine;

public class ElectricEel : Enemy
{
    public ElectricEelAnimation Animation;
    public Enemy Enemy;
    public override IEnumerator AttackSequence()
    {
        Animation.ChargeExplode();
        yield return new WaitForSeconds(2);
        // Die();
        yield break;
    }


    public override bool IsReadyToAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator StartActionCoroutine()
    {
        throw new System.NotImplementedException();
    }
}
