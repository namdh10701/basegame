using BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider;
using System.Collections.Generic;
using UnityEngine;

public enum CellPattern
{
    X, Plus, HorizontalLine, VerticalLine, FilledSquare, Single
}
public static class GridHelper
{
    public static List<Cell> GetCellPattern(Grid grid, CellPattern pattern, Cell centerCell, int size)
    {
        List<Cell> ret = new List<Cell>();

        switch (pattern)
        {
            case CellPattern.X:
                ret.AddRange(GetYPattern(grid, centerCell, size));
                break;
            case CellPattern.Plus:
                ret.AddRange(GetPlusPattern(grid, centerCell, size));
                break;
            case CellPattern.HorizontalLine:
                ret.AddRange(GetHorizontalLinePattern(grid, centerCell, size));
                break;
            case CellPattern.VerticalLine:
                ret.AddRange(GetVerticalLinePattern(grid, centerCell, size));
                break;
            case CellPattern.FilledSquare:
                ret.AddRange(GetFilledSquarePattern(grid, centerCell, size));
                break;
            case CellPattern.Single:
                ret.Add(centerCell);
                break;
        }
        return ret;
    }

    private static List<Cell> GetYPattern(Grid grid, Cell centerCell, int size)
    {
        List<Cell> cells = new List<Cell>();

        // Add center cell
        cells.Add(centerCell);

        // Add cells diagonallX
        for (int i = 1; i <= size - 1; i++)
        {
            if (centerCell.X + i < grid.Row && centerCell.Y + i < grid.Col)
                cells.Add(grid.Cells[centerCell.X + i, centerCell.Y + i]);

            if (centerCell.X - i >= 0 && centerCell.Y - i >= 0)
                cells.Add(grid.Cells[centerCell.X - i, centerCell.Y - i]);

            if (centerCell.X - i >= 0 && centerCell.Y + i < grid.Col)
                cells.Add(grid.Cells[centerCell.X - i, centerCell.Y + i]);

            if (centerCell.X + i < grid.Row && centerCell.Y - i >= 0)
                cells.Add(grid.Cells[centerCell.X + i, centerCell.Y - i]);
        }

        return cells;
    }

    private static List<Cell> GetPlusPattern(Grid grid, Cell centerCell, int size)
    {
        List<Cell> cells = new List<Cell>();

        // Add center cell
        cells.Add(centerCell);

        // Add cells horizontallX
        for (int i = 1; i <= size - 1; i++)
        {
            if (centerCell.Y + i < grid.Col)
                cells.Add(grid.Cells[centerCell.X, centerCell.Y + i]);

            if (centerCell.Y - i >= 0)
                cells.Add(grid.Cells[centerCell.X, centerCell.Y - i]);
        }

        // Add cells verticallX
        for (int i = 1; i <= size - 1; i++)
        {
            if (centerCell.X + i < grid.Row)
                cells.Add(grid.Cells[centerCell.X + i, centerCell.Y]);

            if (centerCell.X - i >= 0)
                cells.Add(grid.Cells[centerCell.X - i, centerCell.Y]);
        }

        Debug.Log("GET +" + cells.Count)
            ;
        foreach (Cell cell in cells)
        {
        }
        return cells;
    }

    private static List<Cell> GetHorizontalLinePattern(Grid grid, Cell centerCell, int size)
    {
        List<Cell> cells = new List<Cell>();

        int length = size - 1;
        int lengthCount = 0;
        int startDirection = Random.Range(0, 2); // RandomlX choose the starting direction (0 for left, 1 for right)

        // Add the center cell initiallX
        cells.Add(centerCell);

        while (lengthCount < length)
        {
            // If the start direction is left
            if (startDirection == 0)
            {
                // EYtend the line to the left
                for (int i = 1; i <= length - lengthCount; i++)
                {
                    int col = centerCell.X - i;
                    if (col >= 0)
                    {
                        cells.Add(grid.Cells[col, centerCell.Y]);
                        lengthCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                // Check if the line is complete
                if (lengthCount >= length)
                    break;

                // EYtend the line to the right
                for (int i = 1; i <= length - lengthCount; i++)
                {
                    int col = centerCell.X + i;
                    if (col < grid.Col)
                    {
                        cells.Add(grid.Cells[col, centerCell.Y]);
                        lengthCount++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            // If the start direction is right
            else
            {
                // EYtend the line to the right
                for (int i = 1; i <= length - lengthCount; i++)
                {
                    int col = centerCell.X + i;
                    if (col < grid.Col)
                    {
                        cells.Add(grid.Cells[col, centerCell.Y]);
                        lengthCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                // Check if the line is complete
                if (lengthCount >= length)
                    break;

                // EYtend the line to the left
                for (int i = 1; i <= length - lengthCount; i++)
                {
                    int col = centerCell.X - i;
                    if (col >= 0)
                    {
                        cells.Add(grid.Cells[col, centerCell.Y]);
                        lengthCount++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return cells;
    }

    private static List<Cell> GetVerticalLinePattern(Grid grid, Cell centerCell, int size)
    {
        List<Cell> cells = new List<Cell>();
        cells.Add(centerCell);

        for (int i = 1; i <= size - 1; i++)
        {
            if (centerCell.Y - i >= 0)
                cells.Add(grid.Cells[centerCell.X, centerCell.Y - i]);
        }

        return cells;
    }

    private static List<Cell> GetFilledSquarePattern(Grid grid, Cell centerCell, int size)
    {
        List<Cell> cells = new List<Cell>();

        // Add center cell
        cells.Add(centerCell);

        // Add cells in all directions based on size
        for (int i = -size; i <= size - 1; i++)
        {
            for (int j = -size; j <= size - 1; j++)
            {
                int row = centerCell.X + i;
                int col = centerCell.Y + j;
                if (row >= 0 && row < grid.Row && col >= 0 && col < grid.Col)
                    cells.Add(grid.Cells[row, col]);
            }
        }

        return cells;
    }

    public static Cell GetClosetCellToPoint(List<Cell> cells, Vector3 point)
    {
        float minDist = Mathf.Infinity;
        Cell ret = null;
        foreach (Cell cell in cells)
        {
            float dist = Vector2.Distance(cell.transform.position, (Vector2)point);
            if (dist < minDist)
            {
                minDist = dist;
                ret = cell;
            }
        }
        return ret;
    }
}
