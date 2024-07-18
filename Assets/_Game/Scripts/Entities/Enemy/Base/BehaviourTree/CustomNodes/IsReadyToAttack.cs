using _Game.Scripts;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Enemy/Is In Cooldown")]
public class IsInCooldown : Condition
{
    public EnemyReference enemyReference;
    public Abort abort;
    public BoolReference isInCooldown;
    public override bool Check()
    {
        isInCooldown.Value = !enemyReference.Value.IsInCooldown();
        return isInCooldown.Value;
    }
    public override void OnAllowInterrupt()
    {
        if (abort != Abort.None)
        {
            ObtainTreeSnapshot();
            isInCooldown.GetVariable().AddListener(OnVariableChange);
        }
    }

    public override void OnDisallowInterrupt()
    {
        if (abort != Abort.None)
        {
            isInCooldown.GetVariable().RemoveListener(OnVariableChange);
        }
    }

    private void OnVariableChange(bool oldValue, bool newValue)
    {
        EvaluateConditionAndTryAbort(abort);
    }
}
