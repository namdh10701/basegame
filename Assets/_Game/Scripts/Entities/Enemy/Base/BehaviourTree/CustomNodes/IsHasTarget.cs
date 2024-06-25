using _Base.Scripts.RPG.Behaviours.FindTarget;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Enemy/Is Has Target")]
public class IsHasTarget : Condition
{
    public FindTargetBehaviour FindTargetBehaviour;
    public Abort abort;
    public BoolReference isHasTarget = new BoolReference(VarRefMode.DisableConstant);

    public override bool Check()
    {
        Debug.Log("CHECK");
        isHasTarget.Value = FindTargetBehaviour.MostTargets.Count > 0;
        return isHasTarget.Value;
    }

    public override void OnAllowInterrupt()
    {
        // Do not listen any changes if abort is disabled
        if (abort != Abort.None)
        {
            // This method cache current tree state used later by abort system
            ObtainTreeSnapshot();
            // If somePropertyRef is constant, then null exception will be thrown.
            // Use somePropertyRef.isConstant in case you need constant enabled.
            // Constant variable is disabled here, so it is safe to do this.
            isHasTarget.GetVariable().AddListener(OnVariableChange);
        }
    }

    public override void OnDisallowInterrupt()
    {
        if (abort != Abort.None)
        {
            // Remove listener
            isHasTarget.GetVariable().RemoveListener(OnVariableChange);
        }
    }

    private void OnVariableChange(bool oldValue, bool newValue)
    {
        // Reevaluate Check() and abort tree when needed
        EvaluateConditionAndTryAbort(abort);
    }
}
