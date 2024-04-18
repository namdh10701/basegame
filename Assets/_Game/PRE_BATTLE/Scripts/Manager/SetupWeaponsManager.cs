using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using UnityEngine;

public class SetupWeaponsManager : SingletonMonoBehaviour<SetupWeaponsManager>
{
    [Header("Config Data")]
    [SerializeField] DataShips _dataShips;

    [Header("Prefab DragItem")]
    [SerializeField] DragItem _prefabDragItem;

    [Header("Prefab WeaponItem")]
    [SerializeField] WeaponItem _prefabWeaponItem;

    [Header("MENU MANAGER")]
    [SerializeField] MenuManager _menuManager;

    [SerializeField] List<Transform> PositionGrids = new List<Transform>();

    public List<WeaponItem> _weaponItems = new List<WeaponItem>();
    private Dictionary<string, List<Cell>> _gridsInfor = new Dictionary<string, List<Cell>>();
    private DragItem _dragItem;
    private DragItemUI _dragItemUI;

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
            for (int i = 0; i < grid.rows; i++)
            {
                for (int j = 0; j < grid.cols; j++)
                {
                    var go = Instantiate(shipConfig.cell, grid.transform);
                    go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    var cell = go.GetComponent<Cell>();
                    var size = cell.GetBounds();
                    cell.Setup(new Vector2(i * size.x / 2, j * size.y / 2), idCell);
                    go.transform.localPosition = new Vector2(i * size.x / 2, j * size.y / 2);
                    listCell.Add(cell);
                    idCell++;
                }
            }
            _gridsInfor.Add(grid.id, listCell);
        }

    }

    public void CreateDragItem(ItemMenuData itemMenuData)
    {
        var mousePosition = Input.mousePosition;
        var screenPosition = new Vector3(mousePosition.x, mousePosition.y);
        var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        _menuManager.EnableScrollRect(false);
        _dragItemUI = _menuManager.CreateDragItemUI(itemMenuData, screenPosition);


        if (_dragItem == null)
        {
            _dragItem = Instantiate(_prefabDragItem, this.transform);

        }
        _dragItem.Setup(itemMenuData);
        _dragItem.transform.position = worldPosition;
    }

    public void SetDataToCells(string gridId, List<Cell> cellSelected, ItemMenuData itemMenuData)
    {
        float totalX = 0f;
        float totalY = 0f;

        if (_gridsInfor.ContainsKey(gridId))
        {
            var cellsInGrid = _gridsInfor[gridId];

            foreach (var selectedCell in cellSelected)
            {
                foreach (var cellInGrid in cellsInGrid)
                {
                    if (cellInGrid.Id == selectedCell.Id)
                    {
                        totalX += cellInGrid.GetPositionCell().x;
                        totalY += cellInGrid.GetPositionCell().y;
                        cellInGrid.SetItemType(selectedCell.GetItemType());
                        cellInGrid.EnableCell(false);
                    }
                }
            }
        }
        _menuManager.EnableScrollRect(true);


        var center = new Vector2(totalX / cellSelected.Count, totalY / cellSelected.Count);
        foreach (var grid in _curentShip.grids)
        {
            if (gridId == grid.id)
            {
                var itemWeapon = Instantiate(_prefabWeaponItem, grid.transform);
                itemWeapon.transform.localPosition = center;
                _weaponItems.Add(itemWeapon);
                foreach (var ship in _dataShips.ships)
                {
                    if (_curentSkin == ship.typeShip)
                    {
                        var existingWeapon = ship.weaponItemDatas.FirstOrDefault(w => w.itemMenuData.id == itemMenuData.id);
                        // if (ship.weaponItemDatas.Count <= 0 || )
                        if (existingWeapon == null)
                        {
                            WeaponItemData weaponItemData = new WeaponItemData();
                            weaponItemData.previousPosition = center;
                            weaponItemData.previousGridID = gridId;
                            weaponItemData.itemMenuData = itemMenuData;
                            ship.weaponItemDatas.Add(weaponItemData);
                            itemWeapon.Setup(weaponItemData, gridId, center);
                            _menuManager.EnableDragItem(weaponItemData.itemMenuData, false);

                        }
                    }
                }
            }
        }
    }

    public void OnChangeDataByMoveWeaponItem(string gridId, List<Cell> cellSelected, WeaponItem weaponItem)
    {
        float totalX = 0f;
        float totalY = 0f;
        if (_gridsInfor.ContainsKey(gridId))
        {
            var cellsInGrid = _gridsInfor[gridId];

            foreach (var selectedCell in cellSelected)
            {
                foreach (var cellInGrid in cellsInGrid)
                {
                    if (cellInGrid.Id == selectedCell.Id)
                    {
                        totalX += cellInGrid.GetPositionCell().x;
                        totalY += cellInGrid.GetPositionCell().y;
                        cellInGrid.SetItemType(selectedCell.GetItemType());
                        cellInGrid.CheckCellsEmty(false);
                        cellInGrid.EnableCell(false);
                    }
                }
            }
        }

        var center = new Vector2(totalX / cellSelected.Count, totalY / cellSelected.Count);



        foreach (var grid in _curentShip.grids)
        {
            if (gridId == grid.id)
            {
                foreach (var ship in _dataShips.ships)
                {
                    foreach (var weaponItemData in ship.weaponItemDatas)
                    {
                        if (weaponItemData == weaponItem.GetWeaponItemData())
                        {
                            weaponItemData.itemMenuData = weaponItem.GetItemMenuData();
                            weaponItemData.previousPosition = center;
                            weaponItemData.previousGridID = gridId;
                        }
                    }
                }

                foreach (var item in _weaponItems)
                {
                    if (item == weaponItem)
                    {
                        item.transform.parent = grid.transform;
                        item.transform.localPosition = center;
                    }
                }
            }
        }

    }

    public void ReturnWeaponItemToPreviousPosition(WeaponItem weaponItem)
    {
        foreach (var grid in _curentShip.grids)
        {
            if (weaponItem.PreviousGridID == grid.id)
            {
                foreach (var item in _weaponItems)
                {
                    if (item == weaponItem)
                    {
                        item.transform.parent = grid.transform;
                        item.transform.localPosition = weaponItem.PreviousPosition;
                    }
                }
            }
        }
    }

    public void RemoveDataWeaponItem(ItemMenuData itemMenuData)
    {
        List<WeaponItemData> weaponItemDataList = new List<WeaponItemData>();
        foreach (var ship in _dataShips.ships)
        {
            foreach (var weaponItemData in ship.weaponItemDatas)
            {
                if (weaponItemData.itemMenuData == itemMenuData)
                {
                    weaponItemDataList.Add(weaponItemData);
                }
            }
            foreach (var weaponItemData in weaponItemDataList)
            {
                ship.weaponItemDatas.Remove(weaponItemData);
            }

        }

        _menuManager.EnableDragItem(itemMenuData, true);
    }
}
