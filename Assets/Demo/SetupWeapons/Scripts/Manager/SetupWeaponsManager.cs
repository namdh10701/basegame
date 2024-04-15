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

    public void SetDataToCells(string gridId, List<Cell> cellSelected)
    {
        if (_gridsInfor.ContainsKey(gridId))
        {
            var cellsInGrid = _gridsInfor[gridId];

            foreach (var selectedCell in cellSelected)
            {
                foreach (var cellInGrid in cellsInGrid)
                {
                    if (cellInGrid.Position == selectedCell.Position)
                    {
                        cellInGrid.itemType = selectedCell.itemType;
                        cellInGrid._spriteRenderer.enabled = false;
                    }
                }
            }
        }
    }











}
