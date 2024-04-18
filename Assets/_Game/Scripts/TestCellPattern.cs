using GridPatternLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCellPattern : MonoBehaviour
{
    public Grid grid;
    public int X;
    public int Y;
    public int Size;
    public CellPattern cellPattern;

    public void ShowPattern()
    {
        foreach (Cell cell in grid.Cells)
        {
            cell.GetComponent<SpriteRenderer>().color = Color.black;
        }

        Cell centerCell = grid.Cells[X, Y];

        List<Cell> pattern = GridHelper.GetCellPattern(grid, cellPattern, centerCell, Size);
    }
}
