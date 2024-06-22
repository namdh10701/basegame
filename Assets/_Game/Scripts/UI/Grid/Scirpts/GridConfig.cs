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
        public float spacing;
        public Cell[,] cells;
        public RectTransform parent;
        public InventoryItemsConfig inventoryItemsConfig;
        public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    }

    [Serializable]
    public class CellData
    {
        public StatusCell statusCell;
        public Vector2 position;

        public CellData(StatusCell statusCell, Vector2 position)
        {
            this.statusCell = statusCell;
            this.position = position;
        }
    }

    public enum StatusCell
    {
        None = 0,
        Empty,
        Occupied
    }

}
