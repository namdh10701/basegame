using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridPatternLogic
{
    public class Grid : MonoBehaviour
    {
        public Cell[,] Cells;
        public int Row;
        public int Col;
        public GameObject cellPrefab;
        public GridPatternSolver solver;
        public int CenterCellRow;
        public int CenterCellCol;
        public int Size;
        public CellPattern cellPattern;
        private void Start()
        {
            GenerateGrid();
        }

        void GenerateGrid()
        {
            Cells = new Cell[Row, Col];

            // Loop through each row and column to instantiate cell objects
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    GameObject newCell = Instantiate(cellPrefab, new Vector3(j, -i), Quaternion.identity);
                    newCell.transform.parent = transform; // Set the parent to this GameObject

                    Cells[i, j] = newCell.GetComponent<Cell>(); // Assuming Cell is a component of your cellPrefab
                    Cells[i, j].Set(i, j);
                }
            }
        }

        public void GetPattern()
        {
            foreach (Cell cell in Cells)
            {
                cell.GetComponent<SpriteRenderer>().color = Color.black;
            }

            Cell centerCell = Cells[CenterCellRow, CenterCellCol];

            List<Cell> pattern = solver.GetCellPattern(this, cellPattern, centerCell, Size);
        }

    }
}