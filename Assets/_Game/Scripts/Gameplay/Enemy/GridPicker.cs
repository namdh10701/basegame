using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay;
using _Game.Scripts.Gameplay.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickType
{
    ClosetCell, RandomCell
}
public class GridPicker : MonoBehaviour
{
    public ShipSetup ShipGrid;
    public List<Cell> PickCells(Transform enemy, PickType pickType, CellPattern pattern, int size, out Cell centerCell)
    {
        List<Cell> cells = null;
        centerCell = null;
        switch (pickType)
        {
            case PickType.RandomCell:
                centerCell = ShipGrid.AllCells.GetRandom();
                cells = GridHelper.GetCellPattern(centerCell.Grid, pattern, centerCell, size);
                break;
            case PickType.ClosetCell:
                centerCell = GridHelper.GetClosetCellToPoint(ShipGrid.AllCells, enemy.position);
                cells = GridHelper.GetCellPattern(centerCell.Grid, pattern, centerCell, size);
                break;
        }
        return cells;
    }

}
