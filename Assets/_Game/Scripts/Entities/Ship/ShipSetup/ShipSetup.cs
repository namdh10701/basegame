using _Game.Scripts.DB;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using UnityEngine;
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts;
using _Game.Scripts.SaveLoad;
using _Base.Scripts.RPGCommon.Entities;

namespace _Game.Features.Gameplay
{
    public class ShipSetup : MonoBehaviour
    {
        public List<GridItemData> UsingGridItemDatas = new List<GridItemData>();

        public Ship Ship;
        public ShipSetupMockup ShipSetupMockup;
        public ShipGridProfile ShipGridProfile;
        public List<Grid> Grids;
        public List<Carpet> Carpets { get; private set; } = new List<Carpet>();
        public List<Ammo> Ammos { get; private set; } = new List<Ammo>();
        public List<Cannon> Cannons { get; private set; } = new List<Cannon>();

        public CrewController CrewController;

        public List<IWorkLocation> WorkLocations { get; private set; } = new List<IWorkLocation>();
        public List<Cell> AllCells { get; private set; } = new List<Cell>();

        public NodeGraph NodeGraph;

        public List<GameObject> spawnedItems = new List<GameObject>();
        public List<IGridItem> allItems = new List<IGridItem>();
        bool initialized;
        public void Initialize()
        {
            if (initialized)
                return;
            initialized = true;
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
            CalculateBuffs();
            BuffEverything();
            AddStatsModifersFromItems();
            ReloadCannons();
        }

        void BuffEverything()
        {
            foreach (Crew crew in CrewController.crews)
            {
                crew.stats.RepairSpeed.AddModifier(new _Base.Scripts.RPG.Stats.StatModifier(Ship.ShipBuffStats.CrewRepairSpeedBoost.Value, _Base.Scripts.RPG.Stats.StatModType.PercentAdd, Ship));
            }
            foreach (Ammo ammo in Ammos)
            {
                ammo.stats.EnergyCost.AddModifier(new _Base.Scripts.RPG.Stats.StatModifier(-Ship.ShipBuffStats.AmmoEnergyCostReduce.Value, _Base.Scripts.RPG.Stats.StatModType.Flat, Ship));
            }
        }

        public ShipStatsController shipStatsController;
        void AddStatsModifersFromItems()
        {
            foreach (var crew in CrewController.crews)
            {
                shipStatsController.RegisterCrew(crew);
            }
            foreach (var carpet in Carpets)
            {
                shipStatsController.RegisterCarpet(carpet);
            }
        }

        void CalculateBuffs()
        {
            foreach (IGridItem gridItem in allItems)
            {
                if (gridItem is IBuffItem buffItem)
                {
                    Debug.Log("START " + buffItem);
                    CalculateBuff(buffItem);
                }

            }
        }
        List<IBuffItem> shipBuff = new List<IBuffItem>();
        public void CalculateBuff(IBuffItem buffItem)
        {
            Debug.Log("Calculate buff 1 " + buffItem);
            IGridItem gridItem = buffItem as IGridItem;
            List<IGridItem> adjItems = gridItem.AdjItems;
            Debug.Log(adjItems.Count + " adj items count");
            if (buffItem.IsBuffable(Ship))
            {

                if (shipBuff.Contains(buffItem))
                {
                    buffItem.RemoveBuff(Ship);
                    shipBuff.Remove(buffItem);
                }
                if (!shipBuff.Contains(buffItem))
                {
                    shipBuff.Add(buffItem);
                    buffItem.Buff(Ship);
                }

            }
            foreach (var adjItem in adjItems)
            {
                if (adjItem is IBuffable buffable)
                {
                    if (buffItem.IsBuffable(buffable))
                    {
                        if (buffItem.BuffedItems.Contains(buffable))
                        {
                            buffItem.RemoveBuff(buffable);
                        }
                        buffItem.Buff(buffable);
                        if (!buffItem.BuffedItems.Contains(buffable))
                        {
                            buffItem.BuffedItems.Add(buffable);
                        }
                        if (adjItem is IBuffItem adjacentBuffItem)
                        {
                            CalculateBuff(adjacentBuffItem);
                        }

                    }
                }
            }
        }

        public void CalculateRemoveBuff(IBuffItem buffItem)
        {
            IGridItem gridItem = buffItem as IGridItem;
            List<IGridItem> adjItems = GridHelper.GetNeighborItems(gridItem);
            foreach (var adjItem in adjItems)
            {
                if (adjItem is IBuffable buffable)
                {
                    if (buffItem.IsBuffable(buffable))
                    {
                        if (buffItem.BuffedItems.Contains(buffable))
                        {
                            buffItem.RemoveBuff(buffable);
                            buffItem.BuffedItems.Remove(buffable);

                            if (buffItem is IBuffItem removedBuffItem)
                            {
                                CalculateRemoveBuff(removedBuffItem);
                            }
                        }
                    }
                }
            }
        }



        public void Refresh()
        {
            ClearItems();
            CrewController.crews.Clear();
            GetLoadOut();
            LoadShipItems();
            CalculateBuffs();
            foreach (Cannon cannon in Cannons)
            {
                cannon.CannonAmmo.OutOfAmmoStateChaged?.Invoke(cannon, false);
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Refresh();
            }
        }
        public void ClearItems()
        {
            foreach (IBuffItem buffItem in shipBuff)
            {
                buffItem.RemoveBuff(Ship);
            }
            foreach (var item in spawnedItems)
            {
                Destroy(item.gameObject);
            }

            Ammos.Clear();
            Cannons.Clear();
            Carpets.Clear();

            allItems.Clear();
            spawnedItems.Clear();

        }

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

            //UsingGridItemDatas = ShipSetupMockup.Datas;
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
            foreach (var carpet in Carpets)
            {
                carpet.Initialize();
            }

            foreach (var gridItem in allItems)
            {
                gridItem.AdjItems = GridHelper.GetNeighborItems(gridItem);
            }


            DefineWorkLocation();

        }


        void ReloadCannons()
        {
            foreach (Cannon cannon in Cannons)
            {
                cannon.Reload(Ammos.GetRandom(), false);
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

                    if (spawned.TryGetComponent(out ICellSplitItem cellSplitItem))
                    {
                        foreach (Cell cell in cellSplitItem.OccupyCells)
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
                    else if (spawned.TryGetComponent(out IGridItem gridItem))
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
                if (cell == null) continue;
                cell.stats.ShipStats = Ship.stats;
                cell.WorkingSlots = new List<Scripts.PathFinding.Node>();
                foreach (var node in NodeGraph.nodes)
                {
                    if (node == null) continue;
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
                case GridItemType.Carpet:
                    SpawnCarpet(gridItemData, grid);
                    break;
            }
        }

        public void SpawnCarpet(GridItemData data, Grid grid)
        {
            Carpet prefab = Database.GetCarpet(data.Id);
            Carpet spawned = Instantiate(prefab, grid.GridItemRoot);
            Carpets.Add(spawned);
            spawned.id = data.Id;
            IGridItem gridItem = spawned.GetComponent<IGridItem>();
            InitOccupyCell(spawned.id, ItemType.CARPET, gridItem, data, grid);

            float scale = Vector3.one.x / spawned.transform.parent.lossyScale.x;
            spawned.transform.localScale = new Vector3(scale, scale, scale);
            spawned.transform.localPosition =
             grid.Cells[data.startY, data.startX].transform.localPosition + Database.GetOffsetCarpetWithStartCell(spawned.id, Ship.Id);
            spawnedItems.Add(spawned.gameObject);
            allItems.Add(spawned);
        }


        void InitOccupyCell(string id, ItemType type, IGridItem gridItem, GridItemData data, Grid grid)
        {
            gridItem.GridId = data.GridId;
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

            IGridItem gridItem = spawned.GetComponent<IGridItem>();
            InitOccupyCell(spawned.Id, ItemType.CANNON, gridItem, data, grid);

            float scale = Vector3.one.x / spawned.transform.parent.lossyScale.x;
            spawned.transform.localScale = new Vector3(scale, scale, scale);
            spawned.transform.localPosition =
            grid.Cells[data.startY, data.startX].transform.localPosition + Database.GetOffsetCannonWithStartCell(spawned.Id, Ship.Id);
            spawnedItems.Add(spawned.gameObject);
            allItems.Add(spawned);
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
            allItems.Add(spawned);
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



        public void HideHUD()
        {
            CannonHUD[] cannonHUDs = transform.GetComponentsInChildren<CannonHUD>();
            AmmoHUD[] ammoHUDs = transform.GetComponentsInChildren<AmmoHUD>();

            foreach (var cannonHUD in cannonHUDs)
            {
                cannonHUD.gameObject.SetActive(false);
            }
            foreach (var ammoHUD in ammoHUDs)
            {
                ammoHUD.gameObject.SetActive(false);
            }
        }

        public void DisableAllItem()
        {
            foreach (Cannon cannon in Cannons)
            {
                cannon.GridItemStateManager.GridItemState = GridItemState.Broken;
            }
        }

        public void OnRevive()
        {
            foreach (var cell in AllCells)
            {
                cell.stats.HealthPoint.StatValue.BaseValue = cell.stats.HealthPoint.MaxValue;
                cell.isBroken = false;
                cell.OnFixed();
            }
            foreach (var item in allItems)
            {
                if (item.Stats is IAliveStats aliveStats)
                {
                    aliveStats.HealthPoint.StatValue.BaseValue = aliveStats.HealthPoint.MaxValue;
                }
                item.GridItemStateManager.GridItemState = GridItemState.Active;
            }

        }
    }
}
