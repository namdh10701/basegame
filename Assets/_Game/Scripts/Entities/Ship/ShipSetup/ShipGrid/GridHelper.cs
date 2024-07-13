using _Game.Features.Gameplay;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

namespace _Game.Features.Gameplay
{
    public enum CellPattern
    {
        X, Plus, HorizontalLine, VerticalLine, FilledSquare, Single, Rectangle
    }
    public static class GridHelper
    {
        public static List<Cell> GetCellPattern(Grid grid, CellPattern pattern, Cell centerCell, int size, int size2)
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
                case CellPattern.Rectangle:
                    ret.AddRange(GetRectanglePattern(grid, centerCell, size, size2));
                    break;
                case CellPattern.Single:
                    ret.Add(centerCell);
                    break;
            }
            return ret;
        }

        private static List<Cell> GetRectanglePattern(Grid grid, Cell centerCell, int sizeX, int sizeY)
        {
            List<Cell> cells = new List<Cell>();
            int startX = centerCell.X - (sizeX / 2);
            int startY = centerCell.Y - (sizeY / 2);
            int endX = startX + sizeX - 1;
            int endY = startY + sizeY - 1;

            Debug.Log(endX + " end " + endY);
            Debug.Log(grid.Col + " bound " + grid.Row);

            Debug.Log(grid.Cells.GetLength(0) + " bound2 " + grid.Cells.GetLength(1));

            // Add cells within the rectangle bounds
            for (int row = startY; row <= endY; row++)
            {
                for (int col = startX; col <= endX; col++)
                {
                    Debug.Log(col + " " + row);
                    if (row >= 0 && row < grid.Row && col >= 0 && col < grid.Col)
                    {
                        Debug.Log(col + " " + row);
                        cells.Add(grid.Cells[row, col]);
                    }
                }
            }
            return cells;
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
                if (centerCell.X + i < grid.Col)
                    cells.Add(grid.Cells[centerCell.Y, centerCell.X + i]);

                if (centerCell.X - i >= 0)
                    cells.Add(grid.Cells[centerCell.Y, centerCell.X - 1]);
            }

            // Add cells verticallX
            for (int i = 1; i <= size - 1; i++)
            {
                if (centerCell.Y + i < grid.Row)
                    cells.Add(grid.Cells[centerCell.Y + i, centerCell.X]);

                if (centerCell.Y - i >= 0)
                    cells.Add(grid.Cells[centerCell.Y - i, centerCell.X]);
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
                            cells.Add(grid.Cells[centerCell.Y, col]);
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
                            cells.Add(grid.Cells[centerCell.Y, col]);
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
                            cells.Add(grid.Cells[centerCell.Y, col]);
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
                            cells.Add(grid.Cells[centerCell.Y, col]);
                            lengthCount++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return cells.Distinct().ToList();
        }

        private static List<Cell> GetVerticalLinePattern(Grid grid, Cell centerCell, int size)
        {
            List<Cell> cells = new List<Cell>();
            cells.Add(centerCell);

            for (int i = 1; i <= size - 1; i++)
            {
                if (centerCell.Y - i >= 0)
                {
                    cells.Add(grid.Cells[centerCell.Y - i, centerCell.X]);

                }
            }

            return cells.Distinct().ToList();
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

            return cells.Distinct().ToList();
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

        public static List<Cell> GetCellsAroundShape(Cell[,] grid, List<Cell> shape)
        {
            List<Cell> ret = new List<Cell>();

            foreach (var cell in shape)
            {
                Debug.Log("COUNT");
                // Get adjacent cells (up, down, left, right)
                List<Cell> adjacentCells = GetAdjacentCells(grid, cell);

                // Check each adjacent cell
                foreach (var adjCell in adjacentCells)
                {
                    if (!shape.Contains(adjCell))
                    {
                        ret.Add(adjCell);
                    }
                }
            }


            return ret;
        }

        public static Cell GetClosetAvailableCellSurroundShape(Cell[,] grid, List<Cell> shape, Vector3 position)
        {

            Cell closestCell = null;
            float closestDistance = float.MaxValue;

            Debug.Log(shape.Count + " totalAmount");
            // Iterate through each cell in the shape
            foreach (var cell in shape)
            {
                Debug.Log("COUNT");
                // Get adjacent cells (up, down, left, right)
                List<Cell> adjacentCells = GetAdjacentCells(grid, cell);

                // Check each adjacent cell
                foreach (var adjCell in adjacentCells)
                {
                    // Calculate distance to the position
                    float dist = Vector3.Distance(adjCell.transform.position, position);

                    // Check if the cell is closer and has a null GridItem
                    if (dist < closestDistance && adjCell.GridItem == null)
                    {
                        closestDistance = dist;
                        closestCell = adjCell;
                    }
                }
            }

            return closestCell;
        }
        private static List<Cell> GetAdjacentCells(Cell[,] grid, Cell cell)
        {
            List<Cell> adjacentCells = new List<Cell>();

            // Define possible directions (up, down, left, right)
            Vector2Int[] directions = {
                new Vector2Int(0, 1),   // Up
                new Vector2Int(0, -1),  // Down
                new Vector2Int(1, 0),   // Right
                new Vector2Int(-1, 0)   // Left
            };

            // Get grid dimensions
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            // Iterate through each direction
            foreach (var dir in directions)
            {
                int newX = cell.X + dir.x;
                int newY = cell.Y + dir.y;

                // Check bounds
                if (newY >= 0 && newY < rows && newX >= 0 && newX < cols)
                {
                    adjacentCells.Add(grid[newY, newX]);
                }
            }

            return adjacentCells;
        }

        public static Crew GetClosetCrewToWorkLocation(List<Crew> crews, IWorkLocation workLocation)
        {
            // Initialize variables to track the closest crew and the minimum distance
            Crew closestCrew = null;
            float minDistance = float.MaxValue;

            // Iterate through each WorkingSlot in the work location
            foreach (var slot in workLocation.WorkingSlots)
            {
                foreach (var crew in crews)
                {
                    // Calculate distance to the cell based on crew's position
                    float distance = Vector3.Distance(crew.transform.position, slot.cell.transform.position);

                    // Check if this crew is closer than the current closest crew
                    if (distance < minDistance)
                    {
                        // Update closest crew and minimum distance
                        closestCrew = crew;
                        minDistance = distance;
                    }
                }
            }

            // Return the closest crew found
            return closestCrew;
        }
    }
}