using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using _Game.Scripts;
using MBT;

[AddComponentMenu("")]
[MBTNode("Enemy/Move")]
public class Move : Leaf
{
    public EnemyReference enemyReference;

    public override NodeResult Execute()
    {
        enemyReference.Value.Move();
        return NodeResult.success;
    }
}
