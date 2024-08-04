using _Base.Scripts.RPG.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class GridAttackHandler : MonoBehaviour
    {
        public Ship ship;
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
                    if (cell == null)
                    {
                        return;
                    }
                    if (effect is DecreaseHealthEffect decrease)
                    {
                        if (cell.GridItem != null)
                        {
                            if (cell.GridItem is IEffectTaker effectTaker)
                            {
                                effectTaker.EffectHandler.Apply(effect);
                            }
                            else
                            {
                                cell.EffectHandler.Apply(effect);
                                ship.EffectHandler.Apply(effect);
                            }
                        }
                        else
                        {
                            cell.EffectHandler.Apply(effect);
                            ship.EffectHandler.Apply(effect);
                        }

                    }
                    else
                    {

                        if (cell.GridItem != null)
                        {
                            if (cell.GridItem is IEffectTaker effectTaker)
                            {
                                effectTaker.EffectHandler.Apply(effect);
                            }
                        }
                        else
                        {
                            ship.EffectHandler.Apply(effect);
                            cell.EffectHandler.Apply(effect);
                        }
                    }
                }

            }
        }
    }
}