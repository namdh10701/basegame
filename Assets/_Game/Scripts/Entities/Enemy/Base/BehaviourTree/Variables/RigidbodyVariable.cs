
using _Game.Scripts.Battle;
using MBT;
using UnityEngine;

namespace _Game.Scripts
{
    [AddComponentMenu("[MBT] CustomVariable/Rigidbody Variable")]
    public class RigidbodyVariable : Variable<Rigidbody2D>
    {
        protected override bool ValueEquals(Rigidbody2D val1, Rigidbody2D val2)
        {
            return val1 == val2;
        }
    }

    [System.Serializable]
    public class RigidbodyReference : VariableReference<RigidbodyVariable, Rigidbody2D>
    {
        protected override bool isConstantValid
        {
            get { return constantValue != null; }
        }
        public RigidbodyReference(Rigidbody2D ship)
        {
            useConstant = true;
            constantValue = ship;
        }

        public Rigidbody2D Value
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
}