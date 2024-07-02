using _Game.Scripts;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Enemy/Is Ready To Attack")]
public class IsReadyToAttack : Condition
{
    public EnemyReference enemyReference;
    public Abort abort;
    public BoolReference isReadyToAttack;
    public override bool Check()
    {
        isReadyToAttack.Value = enemyReference.Value.IsReadyToAttack();
        return isReadyToAttack.Value;
    }
    public override void OnAllowInterrupt()
    {
        if (abort != Abort.None)
        {
            ObtainTreeSnapshot();
            isReadyToAttack.GetVariable().AddListener(OnVariableChange);
        }
    }

    public override void OnDisallowInterrupt()
    {
        if (abort != Abort.None)
        {
            isReadyToAttack.GetVariable().RemoveListener(OnVariableChange);
        }
    }

    private void OnVariableChange(bool oldValue, bool newValue)
    {
        EvaluateConditionAndTryAbort(abort);
    }
}
