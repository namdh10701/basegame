using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Enemy/Is Has Target")]
public class IsHasTarget : Condition
{
    public EnemyReference enemyReference;
    public Abort abort;
    public BoolReference isHasTarget = new BoolReference(VarRefMode.DisableConstant);

    public override bool Check()
    {
        isHasTarget.Value = enemyReference.Value.HasTarget();
        return isHasTarget.Value;
    }

    public override void OnAllowInterrupt()
    {
        // Do not listen any changes if abort is disabled
        if (abort != Abort.None)
        {
            ObtainTreeSnapshot();
            isHasTarget.GetVariable().AddListener(OnVariableChange);
        }
    }

    public override void OnDisallowInterrupt()
    {
        if (abort != Abort.None)
        {
            isHasTarget.GetVariable().RemoveListener(OnVariableChange);
        }
    }

    private void OnVariableChange(bool oldValue, bool newValue)
    {
        // Reevaluate Check() and abort tree when needed
        EvaluateConditionAndTryAbort(abort);
    }
}
