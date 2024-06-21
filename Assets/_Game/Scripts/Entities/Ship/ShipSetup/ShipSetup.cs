using _Base.Scripts.Utils.Extensions;
using _Game.Scripts.Entities;
using Map;
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
        public List<Cell> AllCells = new List<Cell>();
        public List<Cell> FreeCells = new List<Cell>();
        public List<IWorkLocation> WorkLocations = new List<IWorkLocation>();

        public List<Cannon> Cannons = new List<Cannon>();

        private void Awake()
        {
            for (int i = 0; i < Grids.Count; i++)
            {
                Grids[i].Initialize(ShipGridProfile.GridDefinitions[i]);
            }
            foreach (Grid grid in Grids)
            {
                for (int i = 0; i < grid.Row; i++)
                {
                    for (int j = 0; j < grid.Col; j++)
                    {
                        AllCells.Add(grid.Cells[i, j]);
                        if (grid.Cells[i, j].GridItem == null)
                        {
                            FreeCells.Add(grid.Cells[i, j]);
                        }
                    }
                }
            }

            foreach (Cell cell in AllCells)
            {
                cell.Initialize();
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
            else if (gridItemData.Def.Type == GridItemType.Cannon)
            {
                Cannons.Add(spawned.GetComponent<Cannon>());
            }

            IGridItem gridItem = spawned.GetComponent<IGridItem>();
            gridItem.GridId = gridItemData.GridId;
            List<Cell> occupyCells = gridItem.OccupyCells;
            foreach (Vector2Int cell in gridItemData.OccupyCells)
            {
                grid.Cells[cell.y, cell.x].GridItem = gridItem;
                occupyCells.Add(grid.Cells[cell.y, cell.x]);
            }

            float scale = Vector3.one.x / spawned.transform.parent.lossyScale.x;
            spawned.transform.localScale = new Vector3(scale, scale, scale);
            spawned.transform.localPosition = gridItemData.position;

            if (spawned.TryGetComponent(out IWorkLocation workLocation))
            {
                WorkLocations.Add(workLocation);
                List<Cell> workingCells = GridHelper.GetCellsAroundShape(grid.Cells, gridItem.OccupyCells);
                workLocation.WorkingSlots = new List<WorkingSlot>();
                foreach (Cell cell in workingCells)
                {
                    workLocation.WorkingSlots.Add(new WorkingSlot(cell));
                }
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
