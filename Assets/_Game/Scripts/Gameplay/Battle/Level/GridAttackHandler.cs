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
                foreach (Cell cell in cells)
                {
                    if (effect is DecreaseHealthEffect decrease)
                    {
                        if (cell.GridItem != null)
                        {
                            if (cell.GridItem.IsAbleToTakeHit)
                            {
                                cell.GridItem.EffectHandler.Apply(effect);
                            }
                            else
                            {
                                ship.EffectHandler.Apply(effect);
                            }
                        }
                        continue;
                    }

                    if (cell.GridItem != null)
                    {
                        cell.GridItem.EffectHandler.Apply(effect);
                    }
                    else
                    {
                        ship.EffectHandler.Apply(effect);
                    }
                }

            }
        }
    }
}