
using _Game.Scripts.Battle;
using _Game.Scripts.Gameplay.Ship;
using MBT;
using UnityEngine;

namespace _Game.Scripts
{
    [AddComponentMenu("[MBT] CustomVariable/ShipVariable")]
    public class AreaVariable : Variable<Area>
    {
        protected override bool ValueEquals(Area val1, Area val2)
        {
            return val1 == val2;
        }
    }

    [System.Serializable]
    public class AreaReference : VariableReference<AreaVariable, Area>
    {
         protected override bool isConstantValid
         {
             get { return constantValue != null; }
         }
        public AreaReference(Area ship)
        {
            useConstant = true;
            constantValue = ship;
        }

        public Area Value
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