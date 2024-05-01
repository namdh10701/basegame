using _Game.Scripts.Entities;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                Debug.Log(gridItemData.GridId + " " + gridItemData.Def.ToString());
                Transform itemGridTransform = GetGridTransformById(gridItemData.GridId);
                if (itemGridTransform == null)
                {
                    Debug.Log("Ship not have grid with id " + gridItemData.GridId);
                    return;
                }
                SpawnItems(gridItemData, itemGridTransform);
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

        void SpawnItems(GridItemData gridItemData, Transform itemGridTransform)
        {
            GridItem prefab = ItemReferenceHolder.GetItemByIdAndType(gridItemData.Def.Id, gridItemData.Def.Type);
            GridItem gridItem = Instantiate(prefab, itemGridTransform);
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
                    GridItemDatas.Remove(gid);
                }
            }
        }
    }
}
