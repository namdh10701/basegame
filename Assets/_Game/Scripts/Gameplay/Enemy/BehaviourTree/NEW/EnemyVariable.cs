
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using MBT;
using UnityEngine;

namespace _Game.Scripts
{
    [AddComponentMenu("EnemyVariable")]
    public class EnemyVariable : Variable<Enemy>
    {
        protected override bool ValueEquals(Enemy val1, Enemy val2)
        {
            return val1 == val2;
        }
    }

    [System.Serializable]
    public class EnemyReference : VariableReference<EnemyVariable, Enemy>
    {
        protected override bool isConstantValid
        {
            get { return constantValue != null; }
        }
        public EnemyReference(Enemy ship)
        {
            useConstant = false;
            constantValue = ship;
        }

        public Enemy Value
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