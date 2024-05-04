using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class Grid : MonoBehaviour
    {
        public string Id;
        public Cell[,] Cells;
        public int Col => Cells.GetLength(1);
        public int Row => Cells.GetLength(0);
        public Transform CellRoot;
        public Transform GridItemRoot;
        public void Initialize(GridDefinition gridDefinition)
        {
            Id = gridDefinition.Id;
            Cell[] allCells = transform.GetComponentsInChildren<Cell>();
            Cells = new Cell[gridDefinition.Row, gridDefinition.Col];
            foreach (Cell cell in allCells)
            {
                if (Cells[cell.Y, cell.X] == null)
                    Cells[cell.Y, cell.X] = cell;

                else
                    Debug.LogError($"2 Cell with same coordinate {cell.X} {cell.Y}");
            }
        }
    }


}