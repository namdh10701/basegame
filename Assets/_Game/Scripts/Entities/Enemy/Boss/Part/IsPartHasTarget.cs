using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Game.Scripts;
using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Giant Octopus/Is Part Has Target")]
public class IsPartHasTarget : Condition
{
    public Abort abort;
    public BoolReference IsHasTarget = new BoolReference(VarRefMode.DisableConstant);
    public FindTargetBehaviour FindTargetBehaviour;
    public override bool Check()
    {
        IsHasTarget.Value = FindTargetBehaviour.MostTargets.Count > 0;
        return IsHasTarget.Value;
    }

    public override void OnAllowInterrupt()
    {
        if (abort != Abort.None)
        {
            ObtainTreeSnapshot();
            IsHasTarget.GetVariable().AddListener(OnVariableChange);
        }
    }

    public override void OnDisallowInterrupt()
    {
        if (abort != Abort.None)
        {
            IsHasTarget.GetVariable().RemoveListener(OnVariableChange);
        }
    }

    private void OnVariableChange(bool oldValue, bool newValue)
    {
        EvaluateConditionAndTryAbort(abort);
    }
}