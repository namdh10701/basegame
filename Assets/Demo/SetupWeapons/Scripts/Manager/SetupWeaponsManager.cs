using System.Collections.Generic;
using _Base.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetupWeaponsManager : SingletonMonoBehaviour<SetupWeaponsManager>
{
    [Header("Config Data")]
    [SerializeField] ShipConfig _shipConfig;

    [Header("Prefab DragItem")]
    [SerializeField] DragItem _prefabDragItem;

    [Header("Prefab WeaponItem")]
    [SerializeField] WeaponItem _prefabWeaponItem;

    [Header("MENU MANAGER")]
    [SerializeField] MenuManager _menuManager;

    [SerializeField] List<Transform> PositionGrids = new List<Transform>();

    private List<WeaponItem> _weaponItems = new List<WeaponItem>();
    private Dictionary<string, List<Cell>> _gridsInfor = new Dictionary<string, List<Cell>>();
    private DragItem _dragItem;
    private DragItemUI _dragItemUI;
    public bool IsSelectedCells;

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        GetPositionGrids();

    }

    private void GetPositionGrids()
    {
        for (int i = 0; i < PositionGrids.Count; i++)
        {
            _shipConfig.grids[i].transform = PositionGrids[i].transform;
            var grid = PositionGrids[i].GetComponent<Grid>();
            grid.Setup(_shipConfig.grids[i]);
        }
        CreateGrids();
    }

    private void CreateGrids()
    {
        foreach (var grid in _shipConfig.grids)
        {
            var listCell = new List<Cell>();
            for (int i = 0; i < grid.rows; i++)
            {
                for (int j = 0; j < grid.cols; j++)
                {
                    var go = Instantiate(_shipConfig.cell, grid.transform);
                    go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    var cell = go.GetComponent<Cell>();
                    var size = cell.GetBounds();
                    cell.Setup(new Vector2(i * size.x / 2, j * size.y / 2));

                    go.transform.localPosition = new Vector2(i * size.x / 2, j * size.y / 2);
                    listCell.Add(cell);
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
                    if (cellInGrid.GetPositionCell() == selectedCell.GetPositionCell())
                    {
                        totalX += cellInGrid.GetPositionCell().x;
                        totalY += cellInGrid.GetPositionCell().y;
                        cellInGrid.SetItemType(selectedCell.GetItemType());
                        cellInGrid.EnableCell(false);
                    }
                }
            }
        }

        var center = new Vector2(totalX / cellSelected.Count, totalY / cellSelected.Count);
        foreach (var grid in _shipConfig.grids)
        {
            if (gridId == grid.id)
            {
                var itemWeapon = Instantiate(_prefabWeaponItem, grid.transform);
                itemWeapon.transform.localPosition = center;
                itemWeapon.Setup(itemMenuData, gridId, center);
                _weaponItems.Add(itemWeapon);
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
                    if (cellInGrid.GetPositionCell() == selectedCell.GetPositionCell())
                    {
                        totalX += cellInGrid.GetPositionCell().x;
                        totalY += cellInGrid.GetPositionCell().y;
                        cellInGrid.SetItemType(selectedCell.GetItemType());
                        cellInGrid.EnableCell(true);
                    }
                }
            }
        }

        var center = new Vector2(totalX / cellSelected.Count, totalY / cellSelected.Count);
        foreach (var grid in _shipConfig.grids)
        {
            if (gridId == grid.id)
            {
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
        foreach (var grid in _shipConfig.grids)
        {
            if (weaponItem.OldGridID == grid.id)
            {
                foreach (var item in _weaponItems)
                {
                    if (item == weaponItem)
                    {
                        item.transform.parent = grid.transform;
                        item.transform.localPosition = weaponItem.OldPosition;
                    }
                }
            }
        }
    }











}
