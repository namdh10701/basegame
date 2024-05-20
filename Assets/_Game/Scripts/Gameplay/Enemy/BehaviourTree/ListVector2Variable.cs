
using _Game.Scripts.Battle;
using MBT;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    [AddComponentMenu("[MBT] CustomVariable/ListVector2Variable")]
    public class ListVector2Variable : Variable<List<Vector2>>
    {
        protected override bool ValueEquals(List<Vector2> val1, List<Vector2> val2)
        {
            throw new System.NotImplementedException();
        }   
    }

    [System.Serializable]
    public class ListVector2Reference : VariableReference<ListVector2Variable, List<Vector2>>
    {
        protected override bool isConstantValid
        {
            get { return constantValue != null; }
        }
        public ListVector2Reference(List<Vector2> ship)
        {
            useConstant = true;
            constantValue = ship;
        }

        public List<Vector2> Value
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