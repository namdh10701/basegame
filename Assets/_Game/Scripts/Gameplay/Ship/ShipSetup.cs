using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class ShipSetup : MonoBehaviour
    {
        public static List<GridItemData> GridItemDatas = new List<GridItemData>();
        public GridItemReferenceHolder ItemReferenceHolder;
        public ShipGridProfile ShipGridProfile;
        public List<Grid> Grids = new List<Grid>();
        public List<Cell> AllCells
        {
            get
            {
                List<Cell> cells = new List<Cell>();
                foreach (Grid grid in Grids)
                {
                    for (int i = 0; i < grid.Row; i++)
                    {
                        for (int j = 0; j < grid.Col; j++)
                        {
                            cells.Add(grid.Cells[i, j]);
                        }
                    }
                }
                return cells;
            }
        }

        private void Awake()
        {
            for (int i = 0; i < Grids.Count; i++)
            {
                Grids[i].Initialize(ShipGridProfile.GridDefinitions[i]);
            }
        }
        public void LoadShipItems()
        {
            foreach (GridItemData gridItemData in GridItemDatas)
            {
                Debug.Log(gridItemData.Def.Id);
                Transform itemGridTransform = GetGridTransformById(gridItemData.GridId);
                if (itemGridTransform == null)
                {
                    Debug.Log("Ship not have grid with id " + gridItemData.GridId);
                    return;
                }
                SpawnItems(gridItemData, itemGridTransform.GetComponent<Grid>());
            }
        }
        Transform GetGridTransformById(string id)
        {
            foreach (Grid grid in Grids)
            {
                if (grid.Id == id)
                {
                    return grid.transform;
                }
            }
            return null;
        }

        void SpawnItems(GridItemData gridItemData, Grid grid)
        {
            GameObject prefab = ResourceLoader.LoadGridItemPrefab(gridItemData.Def);
            GameObject gridItem = Instantiate(prefab, grid.GridItemRoot);
            gridItem.GetComponent<IGridItem>().GridId = gridItemData.GridId;
            gridItem.GetComponent<IGridItem>().OccupyCells = gridItemData.OccupyCells;

            float scale = Vector3.one.x / gridItem.transform.parent.lossyScale.x;
            gridItem.transform.localScale = new Vector3(scale, scale, scale);
            gridItem.transform.localPosition = gridItemData.position;
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
                    Debug.LogWarning("ASDDS");
                    GridItemDatas.Remove(gid);
                }
            }
        }
    }
}
