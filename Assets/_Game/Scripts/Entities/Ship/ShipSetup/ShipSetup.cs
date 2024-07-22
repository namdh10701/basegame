using _Game.Scripts.DB;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using UnityEngine;
using _Base.Scripts.Utils.Extensions;
using _Base.Scripts.UI;
using _Game.Scripts;
using _Game.Scripts.SaveLoad;
using Unity.VisualScripting;

namespace _Game.Features.Gameplay
{
    public class ShipSetup : MonoBehaviour
    {
        public List<GridItemData> UsingGridItemDatas = new List<GridItemData>();

        public Ship Ship;
        public ShipSetupMockup ShipSetupMockup;
        public ShipGridProfile ShipGridProfile;
        public List<Grid> Grids;
        public List<Ammo> Ammos { get; private set; } = new List<Ammo>();
        public List<Cannon> Cannons { get; private set; } = new List<Cannon>();

        public CrewController CrewController;

        public List<IWorkLocation> WorkLocations { get; private set; } = new List<IWorkLocation>();
        public List<Cell> AllCells { get; private set; } = new List<Cell>();

        public NodeGraph NodeGraph;

        public List<GameObject> spawnedItems = new List<GameObject>();

        public void Initialize()
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
                        if (ShipGridProfile.GridDefinitions[gridIndex].MissingCells == null)
                        {
                            AllCells.Add(Grids[gridIndex].Cells[i, j]);
                        }
                        else if (!ShipGridProfile.GridDefinitions[gridIndex].MissingCells.Contains(missingCell))
                        {
                            AllCells.Add(Grids[gridIndex].Cells[i, j]);
                        }
                    }
                }
            }

            GetLoadOut();
            LoadShipItems();

        }

        public void Refresh()
        {
            ClearItems();
            CrewController.crews.Clear();
            GetLoadOut();
            LoadShipItems();
        }

        private void ClearItems()
        {
            foreach (var item in spawnedItems)
            {
                Destroy(item.gameObject);
            }
        }

        // void GetLoadOut()
        // {
        //     UsingGridItemDatas = new List<GridItemData>();
        //     if (PlayerPrefs.HasKey(Ship.Id))
        //     {
        //         string jsonData = PlayerPrefs.GetString(Ship.Id);
        //         InventoryData data = JsonUtility.FromJson<InventoryData>(jsonData);
        //
        //         List<InventoryItemData> items = data.InventoryItemsOnGrid;
        //
        //         foreach (InventoryItemData item in items)
        //         {
        //             GridItemData itemData = new();
        //             itemData.Id = item.Id;
        //             itemData.startY = item.startX;
        //             itemData.startX = item.startY;
        //             itemData.GridId = "1";
        //
        //             switch (item.Type)
        //             {
        //                 case ItemType.CANNON:
        //                     itemData.GridItemType = GridItemType.Cannon;
        //                     break;
        //                 case ItemType.CREW:
        //                     itemData.GridItemType = GridItemType.Crew;
        //                     break;
        //                 case ItemType.AMMO:
        //                     itemData.GridItemType = GridItemType.Bullet;
        //                     break;
        //             }
        //
        //             UsingGridItemDatas.Add(itemData);
        //
        //         }
        //     }
        // }

        void GetLoadOut()
        {
            UsingGridItemDatas = new List<GridItemData>();
            foreach (var (rawPos, gridItem) in SaveSystem.GameSave.ShipSetupSaveData.GetShipSetup(Ship.Id, SaveSystem.GameSave.ShipSetupSaveData.CurrentProfile).ShipData)
            {
                var xy = rawPos.Split(",").Select(int.Parse).ToList();
                GridItemData itemData = new();
                itemData.Id = gridItem.ItemId;
                int translatedY = ShipGridProfile.GridDefinitions[0].Row - xy[1] - 1;
                itemData.startY = translatedY;
                itemData.startX = xy[0];
                itemData.GridId = "1"; // ????????

                switch (gridItem.ItemType)
                {
                    case ItemType.CANNON:
                        itemData.GridItemType = GridItemType.Cannon;
                        break;
                    case ItemType.CREW:
                        itemData.GridItemType = GridItemType.Crew;
                        break;
                    case ItemType.AMMO:
                        itemData.GridItemType = GridItemType.Bullet;
                        break;
                }

                UsingGridItemDatas.Add(itemData);
            }
        }

        public void LoadShipItems()
        {
            foreach (GridItemData gridItemData in UsingGridItemDatas)
            {
                Grid itemGridTransform = GetGridTransformById(gridItemData.GridId);
                SpawnItems(gridItemData, itemGridTransform);
            }
            foreach (var ammo in Ammos)
            {
                ammo.Initialize();
            }
            foreach (var cannon in Cannons)
            {
                cannon.Initizalize();
            }

            DefineWorkLocation();
            ReloadCannons();
        }

        void ReloadCannons()
        {
            foreach (Cannon cannon in Cannons)
            {
                if (Ammos.Count > 0)
                    cannon.Reload(Ammos.GetRandom());
            }
        }

        void DefineWorkLocation()
        {
            foreach (GameObject spawned in spawnedItems)
            {
                if (spawned.TryGetComponent(out IWorkLocation workLocation))
                {
                    WorkLocations.Add(workLocation);
                    workLocation.WorkingSlots = new List<Scripts.PathFinding.Node>();

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
                    }
                }
            }
            foreach (Cell cell in AllCells)
            {
                cell.WorkingSlots = new List<Scripts.PathFinding.Node>();
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
            switch (gridItemData.GridItemType)
            {
                case GridItemType.Cannon:
                    SpawnCannon(gridItemData, grid);
                    break;
                case GridItemType.Bullet:
                    SpawnBullet(gridItemData, grid);
                    break;
                case GridItemType.Crew:
                    SpawnCrew(gridItemData, grid);
                    break;
            }
            /*GameObject prefab = Database.(gridItemData.Def);
            GameObject spawned = Instantiate(prefab, grid.GridItemRoot);
            if (gridItemData.Def.Type == ItemType.AMMO)
            {
                Bullets.Add(spawned.GetComponent<Bullet>());
            }
            else if (gridItemData.Def.Type == ItemType.CANNON)
            {
                Cannons.Add(spawned.GetComponent<Cannon>());
            }
            else if (gridItemData.Def.Type == ItemType.CREW)
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
            }*/
        }

        void InitOccupyCell(string id, ItemType type, IGridItem gridItem, GridItemData data, Grid grid)
        {
            gridItem.GridId = data.GridId;
            Debug.Log(Database.GetShapeByTypeAndOperationType(id, type).GetLength(1) + id);
            gridItem.OccupyCells =
                GridHelper.GetCoveredCellsIfPutShapeAtCell(
                    Database.GetShapeByTypeAndOperationType(id, type), grid.Cells[data.startY, data.startX]
                    );

            foreach (Cell cell in gridItem.OccupyCells.ToArray())
            {
                if (cell == null)
                {
                    gridItem.OccupyCells.Remove(cell);
                }
                else
                {
                    Debug.Log(cell.ToString());
                    cell.GridItem = gridItem;
                }
            }

        }

        void InitOccupyNode(IGridItem gridItem, INodeOccupier nodeOccupier)
        {
            foreach (Cell cell in gridItem.OccupyCells)
            {
                foreach (var node in NodeGraph.nodes)
                {
                    if (node.cell != null && node.cell == cell)
                    {
                        nodeOccupier.OccupyingNodes.Add(node);
                        node.State = NodeState.Occupied;
                    }
                }
            }
        }

        public void SpawnCannon(GridItemData data, Grid grid)
        {
            Cannon cannonPrefab = Database.GetCannon(data.Id);
            Cannon spawned = Instantiate(cannonPrefab, grid.GridItemRoot);
            spawned.Id = data.Id;

            Cannons.Add(spawned);
            spawned.Def.Type = ItemType.CANNON;

            IGridItem gridItem = spawned.GetComponent<IGridItem>();
            InitOccupyCell(spawned.Id, ItemType.CANNON, gridItem, data, grid);

            float scale = Vector3.one.x / spawned.transform.parent.lossyScale.x;
            spawned.transform.localScale = new Vector3(scale, scale, scale);
            spawned.transform.localPosition =
            grid.Cells[data.startY, data.startX].transform.localPosition + Database.GetOffsetCannonWithStartCell(spawned.Id, Ship.Id);
            spawnedItems.Add(spawned.gameObject);

            INodeOccupier nodeOccupier = spawned.GetComponent<INodeOccupier>();
            InitOccupyNode(gridItem, nodeOccupier);

        }

        public void SpawnBullet(GridItemData data, Grid grid)
        {
            Ammo bulletPrefab = Database.GetBullet(data.Id);
            Ammo spawned = Instantiate(bulletPrefab, grid.GridItemRoot);
            Ammos.Add(spawned);
            spawned.SetId(data.Id);
            IGridItem gridItem = spawned.GetComponent<IGridItem>();
            InitOccupyCell(spawned.id, ItemType.AMMO, gridItem, data, grid);

            float scale = Vector3.one.x / spawned.transform.parent.lossyScale.x;
            spawned.transform.localScale = new Vector3(scale, scale, scale);
            spawned.transform.localPosition =
             grid.Cells[data.startY, data.startX].transform.localPosition + Database.GetOffsetBulletWithStartCell(spawned.id, Ship.Id);
            spawnedItems.Add(spawned.gameObject);

            INodeOccupier nodeOccupier = spawned.GetComponent<INodeOccupier>();
            InitOccupyNode(gridItem, nodeOccupier);
        }

        public void SpawnCrew(GridItemData data, Grid grid)
        {
            Crew crewPrefab = Database.GetCrew(data.Id);
            Crew spawned = Instantiate(crewPrefab, grid.GridItemRoot);

            CrewController.AddCrew(spawned);
            spawned.Id = data.Id;
            float scale = Vector3.one.x / spawned.transform.parent.lossyScale.x;
            spawned.transform.localScale = new Vector3(scale, scale, scale);
            spawned.transform.localPosition = data.position;
            spawnedItems.Add(spawned.gameObject);
            spawned.transform.position =
            grid.Cells[data.startY, data.startX].transform.position;
            INodeOccupier nodeOccupier = spawned.GetComponent<INodeOccupier>();

            foreach (var node in NodeGraph.nodes)
            {
                if (node.cell != null && node.cell.X == data.startX && node.cell.Y == data.startY)
                {
                    nodeOccupier.OccupyingNodes.Add(node);
                }
            }

        }
    }
}
