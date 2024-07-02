using System;
using System.Collections.Generic;
using _Game.Features.Inventory;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;

public class StashManager : MonoBehaviour
{
    [SerializeField] DragController _dragController;
    [SerializeField] _Game.Scripts.Grid grid;
    [SerializeField] StashItemData _configData;
    [SerializeField] ShipGridProfile _shipGridProfile;

    List<GameObject> _items = new List<GameObject>();
    public List<Bullet> bullets = new List<Bullet>();
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        grid.Initialize(_shipGridProfile.GridDefinitions[0]);
        foreach (var gridItemData in _configData.gridItemDatas)
        {
            SpawnItems(gridItemData, grid);
        }
    }
    
    void SpawnItems(GridItemData gridItemData, _Game.Scripts.Grid grid)
    {
        GameObject prefab = ResourceLoader.LoadGridItemPrefab(gridItemData.Def);
        GameObject spawned = Instantiate(prefab, grid.GridItemRoot);
        if (gridItemData.Def.Type == ItemType.AMMO)
        {
            bullets.Add(spawned.GetComponent<Bullet>());
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
    }
}
