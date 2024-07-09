using _Base.Scripts.RPG.Stats;
using MBT;
using UnityEngine;

[AddComponentMenu("StatVariable")]
public class StatVariable : Variable<Stat>
{
    protected override bool ValueEquals(Stat val1, Stat val2)
    {
        return val1 == val2;
    }
}

[System.Serializable]
public class StatReference : VariableReference<StatVariable, Stat>
{
    protected override bool isConstantValid
    {
        get { return constantValue != null; }
    }
    public StatReference(Stat ship)
    {
        useConstant = false;
        constantValue = ship;
    }

    public Stat Value
    {
        get
        {
            return (useConstant) ? constantValue : this.GetVariable().Value;
        }
        set
        {
            if (useConstant)
            {
                constantValue = value;
            }
            else
            {
                this.GetVariable().Value = value;
            }
        }
    }
}
