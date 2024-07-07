
using _Game.Scripts.Entities;
using MBT;
using UnityEngine;

namespace _Game.Scripts
{
    [AddComponentMenu("EnemyVariable")]
    public class EnemyControllerVariable : Variable<EnemyController>
    {
        protected override bool ValueEquals(EnemyController val1, EnemyController val2)
        {
            return val1 == val2;
        }
    }

    [System.Serializable]
    public class EnemyReference : VariableReference<EnemyControllerVariable, EnemyController>
    {
        protected override bool isConstantValid
        {
            get { return constantValue != null; }
        }
        public EnemyReference(EnemyController ship)
        {
            useConstant = false;
            constantValue = ship;
        }

        public EnemyController Value
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