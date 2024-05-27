
using _Game.Scripts.Gameplay.Ship;
using MBT;
using UnityEngine;

namespace _Game.Scripts
{
    [AddComponentMenu("ShipVariable")]
    public class ShipVariable : Variable<Ship>
    {
        protected override bool ValueEquals(Ship val1, Ship val2)
        {
            return val1 == val2;
        }
    }

    [System.Serializable]
    public class ShipReference : VariableReference<ShipVariable, Ship>
    {
         protected override bool isConstantValid
         {
             get { return constantValue != null; }
         }
        public ShipReference(Ship ship)
        {
            useConstant = false;
            constantValue = ship;
        }

        public Ship Value
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