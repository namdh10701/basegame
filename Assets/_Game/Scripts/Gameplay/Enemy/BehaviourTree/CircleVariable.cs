
using _Game.Scripts.Gameplay.Ship;
using MBT;
using UnityEngine;

namespace _Game.Scripts
{
    [AddComponentMenu("CircleVariable")]
    public class CircleVariable : Variable<CircleCollider2D>
    {
        protected override bool ValueEquals(CircleCollider2D val1, CircleCollider2D val2)
        {
            return val1 == val2;
        }
    }

    [System.Serializable]
    public class CircleReference : VariableReference<CircleVariable, CircleCollider2D>
    {
        protected override bool isConstantValid
        {
            get { return constantValue != null; }
        }
        public CircleReference(CircleCollider2D ship)
        {
            useConstant = false;
            constantValue = ship;
        }

        public CircleCollider2D Value
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