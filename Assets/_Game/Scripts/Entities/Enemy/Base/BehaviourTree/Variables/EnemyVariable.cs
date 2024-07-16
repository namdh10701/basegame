
using _Game.Features.Gameplay;
using _Game.Scripts.Entities;
using MBT;
using UnityEngine;

namespace _Game.Scripts
{
    [AddComponentMenu("EnemyVariable")]
    public class EnemyModelVariable : Variable<EnemyModel>
    {
        protected override bool ValueEquals(EnemyModel val1, EnemyModel val2)
        {
            return val1 == val2;
        }
    }

    [System.Serializable]
    public class EnemyReference : VariableReference<EnemyModelVariable, EnemyModel>
    {
        protected override bool isConstantValid
        {
            get { return constantValue != null; }
        }
        public EnemyReference(EnemyModel ship)
        {
            useConstant = false;
            constantValue = ship;
        }

        public EnemyModel Value
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