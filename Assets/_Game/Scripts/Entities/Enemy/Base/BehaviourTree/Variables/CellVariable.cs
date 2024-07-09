using _Game.Features.Gameplay;
using MBT;
using UnityEngine;

namespace _Game.Scripts
{
    [AddComponentMenu("ShipVariable")]
    public class CellVariable : Variable<Cell>
    {
        protected override bool ValueEquals(Cell val1, Cell val2)
        {
            return val1 == val2;
        }
    }

    [System.Serializable]
    public class CellReference : VariableReference<CellVariable, Cell>
    {
         protected override bool isConstantValid
         {
             get { return constantValue != null; }
         }
        public CellReference(Cell ship)
        {
            useConstant = false;
            constantValue = ship;
        }

        public Cell Value
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