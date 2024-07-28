using _Game.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class Grid : MonoBehaviour
    {
        public string Id;
        public Cell[,] Cells;
        public int Col => Cells.GetLength(1);
        public int Row => Cells.GetLength(0);
        public Ship ship;
        public Transform CellRoot;
        public Transform GridItemRoot;
        public GridDefinition GridDefinition;
        public List<GridItemData> GridItemDatas = new List<GridItemData>();

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

        public void AddNewGridItem(GridItemData gridItemData)
        {
            if (!GridItemDatas.Contains(gridItemData))
            {
                GridItemDatas.Add(gridItemData);
            }
        }

        public void RemoveGridItem(GridItemDef gridItemDef)
        {
            foreach (GridItemData gid in GridItemDatas.ToArray())
            {
                if (gid.Def == gridItemDef)
                {
                    GridItemDatas.Remove(gid);
                }
            }
        }

    }

}
