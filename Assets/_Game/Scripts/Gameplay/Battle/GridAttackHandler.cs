using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class GridAttackHandler : MonoBehaviour
    {
        public Fx fxPrefab;
        public Fx fxATKPrefab;
        public _Game.Scripts.Gameplay.Ship.Ship ship;
        public void ProcessTargeting(List<Cell> cells)
        {
            PlayTargetingFx(cells);
        }

        public void ProcessAttack(List<Cell> cells, Effect effect)
        {
            List<IGridItem> placements = new List<IGridItem>();
            List<Cell> emptyCells = new List<Cell>();
            foreach (Cell cell in cells.ToArray())
            {
                if (cell.GridItem != null)
                {
                    if (!placements.Contains(cell.GridItem))
                    {
                        placements.Add(cell.GridItem);
                    }
                }
                else
                {
                    emptyCells.Add(cell);
                }
            }

            ApplyEffect(placements, effect);
            ApplyEffectEmptyCells(emptyCells, effect);
            PlayFx(cells);
        }

        public void ApplyEffectEmptyCells(List<Cell> cells, Effect effect)
        {
            Debug.Log(ship);
            Debug.Log(ship.EffectHandler);
            Debug.Log(cells);
            foreach (Cell cell in cells)
            {
                ship.EffectHandler.Apply(effect);
            }
        }

        public void ApplyEffect(List<IGridItem> placements, Effect effect)
        {
            foreach (Entity placement in placements)
            {
                placement.EffectHandler.Apply(effect);
            }
        }
        public void ApplyEffect(Entity placement, Effect effect)
        {
            placement.EffectHandler.Apply(effect);
        }

        public void PlayFx(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                Fx go = Instantiate(fxATKPrefab, null);
                go.transform.position = cell.transform.position;
            }
        }

        public void PlayTargetingFx(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                Fx go = Instantiate(fxPrefab, null);
                go.transform.position = cell.transform.position;
            }
        }
    }
}