using _Base.Scripts.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickType
{
    ClosetCell, RandomCell
}
public class GridPicker : MonoBehaviour
{
    public ShipGrid ShipGrid;
    public CellPattern CellPattern;
    public PickType PickType;
    public int Size;

    public void PickCell()
    {
        List<Cell> cells = PickCells();
        foreach (Cell cell in cells)
        {
            cell.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    public List<Cell> PickCells()
    {
        List<Cell> cells = null;
        switch (PickType)
        {
            case PickType.RandomCell:
                Cell centerCell = ShipGrid.AllCells.GetRandom();
                cells = GridHelper.GetCellPattern(centerCell.Grid, CellPattern, centerCell, Size);
                break;
            case PickType.ClosetCell:
                Cell centerCell1 = GridHelper.GetClosetCellToPoint(ShipGrid.AllCells, transform.position);
                cells = GridHelper.GetCellPattern(centerCell1.Grid, CellPattern, centerCell1, Size);
                break;
        }
        return cells;
    }
}
