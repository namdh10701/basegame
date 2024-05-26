using System.Collections;
using _Game.Scripts.Entities;
using UnityEngine;

public class ElectricEel : Enemy
{
    public ElectricEelAnimation Animation;
    public Enemy Enemy;
    public IEnumerator AttackSequence()
    {
        Animation.ChargeExplode();
        yield return new WaitForSeconds(2);
        // Die();
        yield break;
    }
}
