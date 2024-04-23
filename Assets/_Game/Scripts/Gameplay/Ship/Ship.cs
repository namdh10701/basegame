using _Base.Scripts.RPG.Entities;
﻿using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.Utils;
using _Game.Scripts.Entities;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Ship
{
    public class Ship : Entity
    {
        public static Ship Instance;
        [SerializeField] ShipStats stats;
        public override Stats Stats => stats;
        public ShipSetup ShipSetup;

        [Header("Config Data")]
        [SerializeField] DataShips _dataShips;
        [SerializeField] List<Transform> PositionGrids = new List<Transform>();
        [SerializeField] WeaponItem _prefabWeaponItem;
        [SerializeField] BulletsMenu _prefabBulletsMenu;
        [SerializeField] GameObject _outSizes;

        private Dictionary<string, List<Cell>> _gridsInfor = new Dictionary<string, List<Cell>>();
        private List<WeaponItem> _weaponItems = new List<WeaponItem>();
        private List<WeaponItemData> _bulletItemData = new List<WeaponItemData>();
        BulletsMenu _bulletsMenu;
        private TypeShip _curentSkin = TypeShip.Normal;
        private ShipConfig _curentShip;

        public Cannon[] prefabs;
        protected override void Awake()
        {
            base.Awake();
            Instance = this;
            ShipSetup.GetPositionGrids();
            ShipSetup.LoadWeaponItems();
        }

        private void Update()
        {
            RegenMP();
            RegenHP();
        }

        void RegenMP()
        {
            if (!stats.ManaPoint.IsFull)
            {
                stats.ManaPoint.StatValue.BaseValue += (stats.ManaRegenerationRate.Value * Time.deltaTime);
            }
        }

        void RegenHP()
        {
            if (!stats.HealthPoint.IsFull)
            {
                stats.HealthPoint.StatValue.BaseValue += (stats.HealthRegenerationRate.Value * Time.deltaTime);
            }
        }

        private void GetPositionGrids()
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
                ShipGrid?.AddGrid(g);
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
                                Debug.Log(weaponItemData.itemMenuData.id);

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

        public void CreateBulletsMenu()
        {
            if (_bulletsMenu == null)
            {
                _bulletsMenu = Instantiate(_prefabBulletsMenu, this.transform);
                _bulletsMenu.transform.position = new Vector3(0, 0, -3);
                _bulletsMenu.Setup(_bulletItemData);
            }
        }

        public void DetroyBulletsMenu()
        {
            Destroy(_bulletsMenu.gameObject);
        }


    }
}
