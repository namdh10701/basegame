using _Game.Scripts;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Enemy/Is Ready To Attack")]
public class IsReadyToAttack : Condition
{
    public EnemyReference enemyReference;
    public BoolReference isReadyToAttack;
    public override bool Check()
    {
        isReadyToAttack.Value = enemyReference.Value.IsReadyToAttack();
        return isReadyToAttack.Value;
    }


}
