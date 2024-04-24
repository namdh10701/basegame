using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class Grid : MonoBehaviour
    {
        private GridData _gridData;
        public string ID;
        public Cell[,] Cells;
        public int Col => Cells.GetLength(1);
        public int Row => Cells.GetLength(0);
        public void Setup(GridData gridData)
        {
            ID = gridData.id;
            _gridData = gridData;
        }

        private void Awake()
        {
            /*Cell[] allCells = transform.GetComponentsInChildren<Cell>();
            Cells = new Cell[_gridData.rows, _gridData.cols];
            foreach (Cell cell in allCells)
            {
                if (Cells[cell.Y, cell.X] == null)
                    Cells[cell.Y, cell.X] = cell;
                else
                    Debug.LogError($"2 Cell with same coordinate {cell.X} {cell.Y}");
            }*/
        }
    }
}
