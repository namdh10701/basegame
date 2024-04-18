using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGrid : MonoBehaviour
{
    public List<Grid> Grids = new List<Grid>();
    public List<Cell> AllCells
    {
        get
        {
            List<Cell> cells = new List<Cell>();
            foreach (Grid grid in Grids)
            {
                for (int i = 0; i < grid.Row; i++)
                {
                    for (int j = 0; j < grid.Col; j++)
                    {
                        cells.Add(grid.Cells[i, j]);
                    }
                }
            }
            return cells;
        }
    }
    public void AddGrid(Grid grid)
    {
        Grids.Add(grid);

    }

}
