using _Base.Scripts.Utils.Extensions;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using Fusion;
using Map;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

namespace _Game.Scripts
{
    public class ShipSetup : MonoBehaviour
    {
        public static List<GridItemData> GridItemDatas = new List<GridItemData>();

        public ShipSetupMockup ShipSetupMockup;
        public GridItemReferenceHolder ItemReferenceHolder;
        public ShipGridProfile ShipGridProfile;
        public List<Grid> Grids;
        public List<Bullet> Bullets { get; private set; } = new List<Bullet>();
        public List<Cannon> Cannons { get; private set; } = new List<Cannon>();

        public CrewController CrewController;

        public List<IWorkLocation> WorkLocations { get; private set; } = new List<IWorkLocation>();
        public List<Cell> AllCells { get; private set; } = new List<Cell>();

        public NodeGraph NodeGraph;

        public List<GameObject> spawnedItems = new List<GameObject>();
        private void Awake()
        {
            for (int i = 0; i < Grids.Count; i++)
            {
                Grids[i].Initialize(ShipGridProfile.GridDefinitions[i]);
            }
            Vector2Int missingCell = Vector2Int.zero;
            for (int gridIndex = 0; gridIndex < Grids.Count; gridIndex++)
            {
                for (int i = 0; i < ShipGridProfile.GridDefinitions[gridIndex].Row; i++)
                {
                    for (int j = 0; j < ShipGridProfile.GridDefinitions[gridIndex].Col; j++)
                    {
                        missingCell.x = j;
                        missingCell.y = i;
                        if (!ShipGridProfile.GridDefinitions[gridIndex].MissingCells.Contains(missingCell))
                            AllCells.Add(Grids[gridIndex].Cells[i, j]);
                    }
                }
            }
            GridItemDatas = ShipSetupMockup.Datas;

        }
        public void LoadShipItems()
        {
            foreach (GridItemData gridItemData in GridItemDatas)
            {
                Debug.Log(gridItemData.Def.Id);
                Grid itemGridTransform = GetGridTransformById(gridItemData.GridId);
                SpawnItems(gridItemData, itemGridTransform);
            }

            DefineWorkLocation();
            ReloadCannons();
        }

        void ReloadCannons()
        {
            foreach (Cannon cannon in Cannons)
            {
                cannon.Reloader.Reload(Bullets[0]);
            }
        }

        void DefineWorkLocation()
        {
            foreach (GameObject spawned in spawnedItems)
            {
                if (spawned.TryGetComponent(out IWorkLocation workLocation))
                {
                    WorkLocations.Add(workLocation);
                    workLocation.WorkingSlots = new List<PathFinding.Node>();

                    if (spawned.TryGetComponent(out IGridItem gridItem))
                    {
                        foreach (Cell cell in gridItem.OccupyCells)
                        {
                            foreach (var node in NodeGraph.nodes)
                            {
                                if (node.cell != null && node.cell == cell)
                                {
                                    foreach (var adjNode in node.neighbors)
                                    {
                                        if (adjNode.Walkable)
                                        {
                                            workLocation.WorkingSlots.Add(adjNode);
                                        }
                                    }
                                }
                            }
                        }

                        if (spawned.TryGetComponent(out Cannon cannon))
                        {
                            cannon.DisableWhenActive();
                        }
                    }
                }
            }
            spawnedItems.Clear();
            foreach (Cell cell in AllCells)
            {
                cell.WorkingSlots = new List<PathFinding.Node>();
                foreach (var node in NodeGraph.nodes)
                {
                    if (node.cell != null && node.cell == cell)
                    {
                        cell.WorkingSlots.Add(node);
                        foreach (var neightbor in node.neighbors)
                        {
                            cell.WorkingSlots.Add(neightbor);
                        }
                    }
                }
            }
        }
        Grid GetGridTransformById(string id)
        {
            foreach (Grid grid in Grids)
            {
                if (grid.Id == id)
                {
                    return grid;
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
                Bullets.Add(spawned.GetComponent<Bullet>());
            }
            else if (gridItemData.Def.Type == GridItemType.Cannon)
            {
                Cannons.Add(spawned.GetComponent<Cannon>());
            }
            else if (gridItemData.Def.Type == GridItemType.Crew)
            {
                CrewController.AddCrew(spawned.GetComponent<Crew>());
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
            spawnedItems.Add(spawned);
            if (spawned.TryGetComponent(out INodeOccupier nodeOccupier))
            {
                foreach (Cell cell in gridItem.OccupyCells)
                {
                    foreach (var node in NodeGraph.nodes)
                    {
                        if (node.cell != null && node.cell == cell)
                        {
                            nodeOccupier.OccupyingNodes.Add(node);
                        }
                    }
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
