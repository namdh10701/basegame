using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class ShipSetup : MonoBehaviour
    {
        public static List<GridItemData> GridItemDatas = new List<GridItemData>();
        public List<GridItemData> Mockup = new List<GridItemData>();
        public GridItemReferenceHolder ItemReferenceHolder;
        public ShipGridProfile ShipGridProfile;
        public List<Grid> Grids = new List<Grid>();

        public List<Bullet> bullets = new List<Bullet>();
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
            GridItemDatas = Mockup;
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
            GameObject spawned = Instantiate(prefab, grid.GridItemRoot);
            if (gridItemData.Def.Type == GridItemType.Bullet)
            {
                bullets.Add(spawned.GetComponent<Bullet>());
            }
            Debug.Log(gridItemData.Def.name);
            IGridItem gridItem = spawned.GetComponent<IGridItem>();
            gridItem.GridId = gridItemData.GridId;
            List<Cell> occupyCells = gridItem.OccupyCells;
            foreach (Vector2Int cell in gridItemData.OccupyCells)
            {
                Debug.Log(grid.Cells[cell.y, cell.x].ToString() + "ASDSAD");
                grid.Cells[cell.y, cell.x].GridItem = gridItem;
                occupyCells.Add(grid.Cells[cell.y, cell.x]);
            }

            float scale = Vector3.one.x / spawned.transform.parent.lossyScale.x;
            spawned.transform.localScale = new Vector3(scale, scale, scale);
            spawned.transform.localPosition = gridItemData.position;
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
