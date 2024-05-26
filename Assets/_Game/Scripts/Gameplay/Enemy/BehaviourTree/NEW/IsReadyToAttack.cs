using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Enemy/Is Ready To Attack")]
public class IsReadyToAttack : Condition
{
    public EnemyReference enemyReference;
    public override bool Check()
    {
        return enemyReference.Value.IsReadyToAttack();
    }
}
