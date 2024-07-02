using System;
using System.Collections.Generic;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Grid Info")]
    public class GridConfig : ScriptableObject
    {
        public Cell cell;
        public List<GridInfor> grids = new List<GridInfor>();
    }

    [Serializable]
    public class GridInfor
    {
        public string id;
        public int rows;
        public int cols;
        public Vector2 cellSize;
        public float spacing;
        public Cell[,] cells;
        public List<CellData> listCellsData = new List<CellData>();
        public ItemsReceived ItemsReceived;
        public Transform startPos;

        public int GetIndex(Cell cell)
        {
            if (listCellsData.Count == 0)
            {
                return -1;
            }
            for (int i = 0; i < listCellsData.Count; i++)
            {
                if (listCellsData[i].position == cell.GetPosition())
                    return i;
            }
            return -1;
        }

    }

    [Serializable]
    public class CellData
    {
        public int r;
        public int c;
        public StatusCell statusCell;
        public Vector2 position;
        public Vector2 size;

        public CellData(int r, int c, StatusCell statusCell, Vector2 position, Vector2 size)
        {
            this.r = r;
            this.c = c;
            this.statusCell = statusCell;
            this.position = position;
            this.size = size;
        }
    }

    public enum StatusCell
    {
        None = 0,
        Empty,
        Occupied
    }

}
