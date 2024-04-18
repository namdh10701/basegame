using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridPatternLogic
{
    public class GridPatternSolver : MonoBehaviour
    {
        public Grid Grid;

        private void Start()
        {

        }

        public List<Cell> GetCellPattern(Grid grid, CellPattern pattern, Cell centerCell, int size)
        {
            List<Cell> ret = new List<Cell>();

            switch (pattern)
            {
                case CellPattern.X:
                    ret.AddRange(GetXPattern(grid, centerCell, size));
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
            foreach (Cell cell in ret)
            {
                cell.GetComponent<SpriteRenderer>().color = Color.white;
            }
            return ret;
        }

        private List<Cell> GetXPattern(Grid grid, Cell centerCell, int size)
        {
            List<Cell> cells = new List<Cell>();

            // Add center cell
            cells.Add(centerCell);

            // Add cells diagonally
            for (int i = 1; i <= size - 1; i++)
            {
                if (centerCell.Row + i < grid.Row && centerCell.Col + i < grid.Col)
                    cells.Add(grid.Cells[centerCell.Row + i, centerCell.Col + i]);

                if (centerCell.Row - i >= 0 && centerCell.Col - i >= 0)
                    cells.Add(grid.Cells[centerCell.Row - i, centerCell.Col - i]);

                if (centerCell.Row - i >= 0 && centerCell.Col + i < grid.Col)
                    cells.Add(grid.Cells[centerCell.Row - i, centerCell.Col + i]);

                if (centerCell.Row + i < grid.Row && centerCell.Col - i >= 0)
                    cells.Add(grid.Cells[centerCell.Row + i, centerCell.Col - i]);
            }

            return cells;
        }

        private List<Cell> GetPlusPattern(Grid grid, Cell centerCell, int size)
        {
            List<Cell> cells = new List<Cell>();

            // Add center cell
            cells.Add(centerCell);

            // Add cells horizontally
            for (int i = 1; i <= size - 1; i++)
            {
                if (centerCell.Col + i < grid.Col)
                    cells.Add(grid.Cells[centerCell.Row, centerCell.Col + i]);

                if (centerCell.Col - i >= 0)
                    cells.Add(grid.Cells[centerCell.Row, centerCell.Col - i]);
            }

            // Add cells vertically
            for (int i = 1; i <= size - 1; i++)
            {
                if (centerCell.Row + i < grid.Row)
                    cells.Add(grid.Cells[centerCell.Row + i, centerCell.Col]);

                if (centerCell.Row - i >= 0)
                    cells.Add(grid.Cells[centerCell.Row - i, centerCell.Col]);
            }

            return cells;
        }

        private List<Cell> GetHorizontalLinePattern(Grid grid, Cell centerCell, int size)
        {
            List<Cell> cells = new List<Cell>();

            int length = size - 1;
            int lengthCount = 0;
            int startDirection = Random.Range(0, 2); // Randomly choose the starting direction (0 for left, 1 for right)

            // Add the center cell initially
            cells.Add(centerCell);

            while (lengthCount < length)
            {
                // If the start direction is left
                if (startDirection == 0)
                {
                    // Extend the line to the left
                    for (int i = 1; i <= length - lengthCount; i++)
                    {
                        int col = centerCell.Col - i;
                        if (col >= 0)
                        {
                            cells.Add(grid.Cells[centerCell.Row, col]);
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

                    // Extend the line to the right
                    for (int i = 1; i <= length - lengthCount; i++)
                    {
                        int col = centerCell.Col + i;
                        if (col < grid.Col)
                        {
                            cells.Add(grid.Cells[centerCell.Row, col]);
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
                    // Extend the line to the right
                    for (int i = 1; i <= length - lengthCount; i++)
                    {
                        int col = centerCell.Col + i;
                        if (col < grid.Col)
                        {
                            cells.Add(grid.Cells[centerCell.Row, col]);
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

                    // Extend the line to the left
                    for (int i = 1; i <= length - lengthCount; i++)
                    {
                        int col = centerCell.Col - i;
                        if (col >= 0)
                        {
                            cells.Add(grid.Cells[centerCell.Row, col]);
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

        private List<Cell> GetVerticalLinePattern(Grid grid, Cell centerCell, int size)
        {
            List<Cell> cells = new List<Cell>();
            cells.Add(centerCell);

            for (int i = 1; i <= size - 1; i++)
            {
                if (centerCell.Row - i >= 0)
                    cells.Add(grid.Cells[centerCell.Row - i, centerCell.Col]);
            }

            return cells;
        }

        private List<Cell> GetFilledSquarePattern(Grid grid, Cell centerCell, int size)
        {
            List<Cell> cells = new List<Cell>();

            // Add center cell
            cells.Add(centerCell);

            // Add cells in all directions based on size
            for (int i = -size; i <= size - 1; i++)
            {
                for (int j = -size; j <= size - 1; j++)
                {
                    int row = centerCell.Row + i;
                    int col = centerCell.Col + j;
                    if (row >= 0 && row < grid.Row && col >= 0 && col < grid.Col)
                        cells.Add(grid.Cells[row, col]);
                }
            }

            return cells;
        }
    }
}
