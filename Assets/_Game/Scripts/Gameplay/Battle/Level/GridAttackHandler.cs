using _Base.Scripts.RPG.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class GridAttackHandler : MonoBehaviour
    {
        public _Game.Scripts.Gameplay.Ship.Ship ship;
        public void ProcessAttack(EnemyAttackData enemyAttackData)
        {
            ProcessAttack(enemyAttackData.TargetCells, enemyAttackData.Effects);
        }

        void ProcessAttack(List<Cell> cells, List<Effect> effects)
        {
            foreach (Effect effect in effects)
            {
                ship.EffectHandler.Apply(effect);
                foreach (Cell cell in cells)
                {
                    if (cell.GridItem != null)
                    {
                        cell.GridItem.EffectHandler.Apply(effect);
                    }
                }
            }
        }
    }
}