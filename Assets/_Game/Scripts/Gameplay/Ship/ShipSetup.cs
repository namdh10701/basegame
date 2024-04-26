using _Game.Scripts.Entities;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.Gameplay
{
    public class ShipSetup : MonoBehaviour
    {
        public GameObject[] prefabs;
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

        [Header("Config Data")]
        [SerializeField] DataShips _dataShips;
        [SerializeField] List<Transform> PositionGrids = new List<Transform>();
        [SerializeField] WeaponItem _prefabWeaponItem;


        public Dictionary<string, List<Cell>> _gridsInfor = new Dictionary<string, List<Cell>>();
        public List<WeaponItem> _weaponItems = new List<WeaponItem>();
        public List<WeaponItemData> _bulletItemData = new List<WeaponItemData>();

        private TypeShip _curentSkin = TypeShip.Normal;
        private ShipConfig _curentShip;

        public void GetPositionGrids()
        {
            if (_dataShips == null)
            {
                return;
            }
            foreach (var ship in _dataShips.ships)
            {
                ship.typeShip = _curentSkin;
                if (_curentSkin == ship.typeShip)
                {
                    _curentShip = ship;
                    for (int i = 0; i < PositionGrids.Count; i++)
                    {
                        ship.grids[i].transform = PositionGrids[i].transform;
                        var grid = PositionGrids[i].GetComponent<Grid>();
                        grid.Setup(ship.grids[i]);
                    }
                    CreateGrids(ship);
                }

            }
        }

        /// <summary>
        /// Create grids
        /// </summary>
        /// <param name="shipConfig"></param>
        private void CreateGrids(ShipConfig shipConfig)
        {
            var idCell = 0;
            int index = 0;
            foreach (var grid in shipConfig.grids)
            {
                Grid g = PositionGrids[index].GetComponent<Grid>();
                Cell[,] cells = new Cell[grid.rows, grid.cols];
                var listCell = new List<Cell>();
                var cellTransform = grid.transform; // Store grid's transform reference

                for (int i = 0; i < grid.rows; i++)
                {
                    for (int j = 0; j < grid.cols; j++)
                    {
                        var go = Instantiate(shipConfig.cell, cellTransform);
                        go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        var cell = go.GetComponent<Cell>();
                        var size = cell.GetBounds();

                        // Calculate size.x / 2 and size.y / 2 once
                        var halfSizeX = size.x / 2;
                        var halfSizeY = size.y / 2;

                        // Calculate position once
                        var posX = i * halfSizeX;
                        var posY = j * halfSizeY;

                        cell.Setup(new Vector2(posX, posY), idCell);
                        cell.X = i;
                        cell.Y = j;
                        cell.name = $"Cell ({i}, {j}) {g.ID}";
                        cell.Grid = g;
                        go.transform.localPosition = new Vector2(posX, posY);
                        listCell.Add(cell);
                        idCell++;
                        cells[i, j] = cell;

                    }
                }
                g.Cells = cells;
                Grids.Add(g);
                _gridsInfor.Add(grid.id, listCell);
                index++;
            }
        }

        /// <summary>
        /// Load weapon items form data
        /// </summary>
        public void LoadWeaponItems()
        {
            if (_curentShip == null)
            {
                return;
            }
            foreach (var grid in _curentShip.grids)
            {
                // Filter out only relevant ships that have weapon items on the current grid
                var relevantShips = _dataShips.ships.Where(ship =>
                    ship.weaponItemDatas.Any(weaponItemData => weaponItemData.previousGridID == grid.id));

                foreach (var ship in relevantShips)
                {
                    foreach (var weaponItemData in ship.weaponItemDatas)
                    {
                        if (weaponItemData.previousGridID == grid.id)
                        {
                            // Check if the weapon item is already instantiated
                            bool isInstantiated = _weaponItems.Any(item => item.GetWeaponItemData() == weaponItemData);
                            if (!isInstantiated)
                            {
                                Vector3 pos = (Vector3)weaponItemData.previousPosition + new Vector3(0, 0, -1f);
                                //_weaponItems.Add(itemWeapon);
                                if (weaponItemData.itemMenuData.itemType != ItemType.Gun)
                                {
                                    var itemWeapon1 = Instantiate(_prefabWeaponItem, grid.transform);
                                    itemWeapon1.transform.localPosition = pos;
                                    itemWeapon1.transform.gameObject.tag = weaponItemData.itemMenuData.itemType.ToString();
                                    itemWeapon1.Setup(weaponItemData);
                                    _weaponItems.Add(itemWeapon1);
                                    //Get bullets item data
                                    if (weaponItemData.itemMenuData.itemType == ItemType.Bullet)
                                    {
                                        itemWeapon1.Setup(weaponItemData);
                                        _bulletItemData.Add(weaponItemData);
                                    }
                                }
                                else
                                {
                                    var itemWeapon = Instantiate(prefabs[weaponItemData.itemMenuData.id - 1], grid.transform);
                                    itemWeapon.transform.localPosition = pos;
                                    itemWeapon.transform.gameObject.tag = weaponItemData.itemMenuData.itemType.ToString();
                                }

                            }

                        }
                    }
                }
            }
        }

        string GridItemshipPlacementPrefabsDirectory = "Prefabs/Cannons/Cannon_NormalShot";
        void SpawnItems(WeaponItemData weaponItemData)
        {
            GridItem prefab = Resources.Load<Cannon>(GridItemshipPlacementPrefabsDirectory);
            GridItem cannon = Instantiate(prefab);
            cannon.transform.localPosition = weaponItemData.previousPosition;
            cannon.transform.gameObject.tag = weaponItemData.itemMenuData.itemType.ToString();
        }
    }
}
