using Demo.Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCondition : MyCustomCondition
{
    public Enemy enemy;
    public override bool IsMet()
    {
        return enemy.IsAbleToAttack;
    }
}
