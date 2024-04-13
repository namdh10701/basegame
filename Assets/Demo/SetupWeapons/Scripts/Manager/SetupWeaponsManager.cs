using System.Collections.Generic;
using UnityEngine;

public class SetupWeaponsManager : MonoBehaviour
{
    [Header("Config Data")]
    [SerializeField] ShipConfig _shipConfig;

    [SerializeField] List<Transform> PositionGrids = new List<Transform>();
    
    List<List<Cell>> _gridsInfor = new List<List<Cell>>();

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

            _gridsInfor.Add(listCell);

        }

    }










}
