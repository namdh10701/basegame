using _Base.Scripts.RPG.Effects;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridAttackHandler : MonoBehaviour
{
    public Fx fxPrefab;
    public Fx fxATKPrefab;
    public Ship ship;
    public void ProcessTargeting(List<Cell> cells)
    {
        PlayTargetingFx(cells);
    }

    public void ProcessAttack(List<Cell> cells, Effect effect)
    {
        List<GridItem> placements = new List<GridItem>();
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
        PlayFx(emptyCells);
    }

    public void ApplyEffectEmptyCells(List<Cell> cells, Effect effect)
    {
        foreach (Cell cell in cells)
        {
            ship.EffectHandler.Apply(effect);
        }
    }

    public void ApplyEffect(List<GridItem> placements, Effect effect)
    {
        foreach (GridItem placement in placements)
        {
            placement.EffectHandler.Apply(effect);
        }
    }
    public void ApplyEffect(GridItem placement, Effect effect)
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