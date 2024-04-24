using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts
{
    public class SetupWeaponsManager : SingletonMonoBehaviour<SetupWeaponsManager>
    {
        [Header("Config Data")]
        [SerializeField] DataShips _dataShips;

        [Header("Prefab DragItem")]
        [SerializeField] DragItem _prefabDragItem;

        [Header("Prefab WeaponItem")]
        [SerializeField] WeaponItem _prefabWeaponItem;

        [Header("MENU MANAGER")]
        [SerializeField] MenuPreBattle _menuManager;

        [SerializeField] List<Transform> PositionGrids = new List<Transform>();

        public List<WeaponItem> _weaponItems = new List<WeaponItem>();
        private Dictionary<string, List<Cell>> _gridsInfor = new Dictionary<string, List<Cell>>();

        public TypeShip _curentSkin = TypeShip.Normal;
        public ShipConfig _curentShip;

        public void Start()
        {
            Initialize();
            Application.quitting += QuitGame;
        }

        private void QuitGame()
        {
            Debug.Log("Quit game");
            ResetData();
        }

        public void ResetData()
        {
            if (_dataShips == null)
            {
                return;
            }
            foreach (var ship in _dataShips.ships)
            {
                ship.weaponItemDatas.Clear();
                ship.typeShip = TypeShip.None;
            }
        }

        public void Initialize()
        {
            GetPositionGrids();
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

        private void CreateGrids(ShipConfig shipConfig)
        {
            var idCell = 0;
            foreach (var grid in shipConfig.grids)
            {
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
                        go.transform.localPosition = new Vector2(posX, posY);
                        listCell.Add(cell);
                        idCell++;
                    }
                }
                _gridsInfor.Add(grid.id, listCell);
            }
        }

        public void SetDataToCells(string gridId, List<Cell> cellSelected, ItemMenuData itemMenuData)
        {
            if (!_gridsInfor.TryGetValue(gridId, out var cellsInGrid))
            {
                Debug.LogWarning($"Grid {gridId} not found in grids information.");
                return;
            }

            // Calculate the total position of selected cells
            Vector2 totalPosition = Vector2.zero;
            foreach (var selectedCell in cellSelected)
            {
                var cellInGrid = cellsInGrid.FirstOrDefault(cell => cell.Id == selectedCell.Id);
                if (cellInGrid != null)
                {
                    totalPosition += cellInGrid.GetPositionCell();
                    cellInGrid.SetItemType(selectedCell.GetItemType());
                    cellInGrid.EnableCell(false);
                }
            }

            // Calculate the center position
            Vector2 center = totalPosition / cellSelected.Count;

            // Instantiate weapon item
            foreach (var grid in _curentShip.grids)
            {
                if (gridId == grid.id)
                {
                    var itemWeapon = Instantiate(_prefabWeaponItem, grid.transform);
                    itemWeapon.transform.localPosition = center;
                    _weaponItems.Add(itemWeapon);

                    // Add weapon item data to ship if not exists
                    foreach (var ship in _dataShips.ships.Where(ship => _curentSkin == ship.typeShip))
                    {
                        var existingWeapon = ship.weaponItemDatas.FirstOrDefault(w => w.itemMenuData.id == itemMenuData.id && w.itemMenuData.itemType == itemMenuData.itemType);
                        if (existingWeapon == null)
                        {
                            var weaponItemData = new WeaponItemData
                            {
                                previousPosition = center,
                                previousGridID = gridId,
                                itemMenuData = itemMenuData
                            };
                            ship.weaponItemDatas.Add(weaponItemData);
                            itemWeapon.Setup(weaponItemData);
                            _menuManager.EnableDragItem(weaponItemData.itemMenuData, false);
                        }
                    }
                }
            }

            _menuManager.EnableScrollRect(true);
        }


        public void OnChangeDataByMoveWeaponItem(string gridId, List<Cell> cellSelected, WeaponItem weaponItem)
        {
            if (!_gridsInfor.TryGetValue(gridId, out var cellsInGrid))
            {
                Debug.LogWarning($"Grid {gridId} not found in grids information.");
                return;
            }

            // Calculate the total position of selected cells
            Vector2 totalPosition = Vector2.zero;
            foreach (var selectedCell in cellSelected)
            {
                var cellInGrid = cellsInGrid.FirstOrDefault(cell => cell.Id == selectedCell.Id);
                if (cellInGrid != null)
                {
                    totalPosition += cellInGrid.GetPositionCell();
                    cellInGrid.SetItemType(selectedCell.GetItemType());
                    cellInGrid.CheckCellsEmty(false);
                    cellInGrid.EnableCell(false);
                }
            }

            // Calculate the center position
            Vector2 center = totalPosition / cellSelected.Count;

            // Update weapon item data
            foreach (var ship in _dataShips.ships)
            {
                var weaponItemData = ship.weaponItemDatas.FirstOrDefault(w => w == weaponItem.GetWeaponItemData());
                if (weaponItemData != null)
                {
                    weaponItemData.itemMenuData = weaponItem.GetItemMenuData();
                    weaponItemData.previousPosition = center;
                    weaponItemData.previousGridID = gridId;
                }
            }

            // Move weapon item to the new grid
            foreach (var grid in _curentShip.grids)
            {
                if (gridId == grid.id)
                {
                    weaponItem.transform.parent = grid.transform;
                    weaponItem.transform.localPosition = center;
                    break;
                }
            }
        }

        public void ReturnWeaponItemToPreviousPosition(WeaponItem weaponItem)
        {
            var previousGridID = weaponItem.GetWeaponItemData().previousGridID;
            var previousPosition = weaponItem.GetWeaponItemData().previousPosition;

            foreach (var grid in _curentShip.grids)
            {
                if (grid.id == previousGridID)
                {
                    weaponItem.transform.parent = grid.transform;
                    weaponItem.transform.localPosition = previousPosition;
                    break;
                }
            }
        }


        public void RemoveDataWeaponItem(ItemMenuData itemMenuData)
        {
            foreach (var ship in _dataShips.ships)
            {
                ship.weaponItemDatas.RemoveAll(weaponItemData => weaponItemData.itemMenuData == itemMenuData);
            }

            _menuManager.EnableDragItem(itemMenuData, true);
        }

    }
}