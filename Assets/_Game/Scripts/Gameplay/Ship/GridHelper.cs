using BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace _Game.Scripts
{
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
                {
                    Debug.Log(centerCell.Y - i);
                    cells.Add(grid.Cells[centerCell.Y - i, centerCell.X]);

                }
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

        public static bool IsCellsCoveredShape(List<Cell> cells, int[,] shape)
        {
            for (int i = 0; i <= cells.Max(c => c.Y) - shape.GetLength(0) + 1; i++)
            {
                for (int j = 0; j <= cells.Max(c => c.X) - shape.GetLength(1) + 1; j++)
                {
                    if (IsShapeMatch(cells, j, i, shape))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsShapeMatch(List<Cell> cells, int x, int y, int[,] shape)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    int gridY = y + i;
                    int gridX = x + j;

                    if (shape[i, j] == 1 && !IsCellCovered(cells, gridX, gridY))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsCellCovered(List<Cell> cells, int x, int y)
        {
            foreach (var cell in cells)
            {
                if (cell.X == x && cell.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<Cell> GetCoveredCellsIfPutShapeAtCell(int[,] itemShape, Cell cell)
        {
            List<Cell> coveredCells = new List<Cell>();
            Grid grid = cell.Grid;
            for (int i = 0; i < itemShape.GetLength(0); i++)
            {
                for (int j = 0; j < itemShape.GetLength(1); j++)
                {
                    if (itemShape[i, j] == 1)
                    {
                        int coveredRow = cell.Y + i;
                        int coveredColumn = cell.X + j;
                        if (coveredRow < grid.Row && coveredColumn < grid.Col)
                            coveredCells.Add(grid.Cells[coveredRow, coveredColumn]);
                    }
                }
            }

            return coveredCells;
        }

        public static Vector3 GetAveragePosition(List<Cell> cells)
        {
            if (cells == null || cells.Count == 0)
            {
                return Vector3.zero;
            }
            Cell mostLeftTop = cells[0];
            Cell mostDownRight = cells[0];

            foreach (Cell cell in cells)
            {
                if (cell.transform.position.x < mostLeftTop.transform.position.x ||
                    (cell.transform.position.x == mostLeftTop.transform.position.x && cell.transform.position.y > mostLeftTop.transform.position.y))
                {
                    mostLeftTop = cell;
                }

                if (cell.transform.position.x > mostDownRight.transform.position.x ||
                    (cell.transform.position.x == mostDownRight.transform.position.x && cell.transform.position.y < mostDownRight.transform.position.y))
                {
                    mostDownRight = cell;
                }
            }
            Vector3 sum = mostLeftTop.transform.position + mostDownRight.transform.position;
            return new Vector3(sum.x / 2f, sum.y / 2f, 0);
        }
    }
}