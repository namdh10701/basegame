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
        public InventoryItemsConfig inventoryItemsConfig;

    }

    [Serializable]
    public class CellData
    {
        public StatusCell statusCell;
        public Vector2 position;
        public Vector2 size;

        public CellData(StatusCell statusCell, Vector2 position, Vector2 size)
        {
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
