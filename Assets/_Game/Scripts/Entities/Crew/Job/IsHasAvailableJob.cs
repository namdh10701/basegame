using _Game.Scripts;
using MBT;
using System.Runtime.Serialization;
using UnityEngine;
[AddComponentMenu("")]
[MBTNode(name = "Crew/IsHasAvailableJob")]
public class IsHasAvailableJob : Condition
{
    public Abort abort;
    public BoolReference somePropertyRef = new BoolReference(VarRefMode.DisableConstant);

    public override bool Check()
    {
        return somePropertyRef.Value == true;
    }
    public override void OnAllowInterrupt()
    {
        if (abort != Abort.None)
        {
            ObtainTreeSnapshot();
            somePropertyRef.GetVariable().AddListener(OnVariableChange);
        }
    }

    public override void OnDisallowInterrupt()
    {
        if (abort != Abort.None)
        {
            somePropertyRef.GetVariable().RemoveListener(OnVariableChange);
        }
    }
    private void OnVariableChange(bool oldValue, bool newValue)
    {
        EvaluateConditionAndTryAbort(abort);
    }
}
