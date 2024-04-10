using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class SetupWeaponsManager : MonoBehaviour
{
    [Header("Config Data")]
    [SerializeField] ShipConfig _shipConfig;
    [SerializeField] UnityEngine.Camera mainCamera;

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
            _shipConfig.Grids[i].Transform = PositionGrids[i].transform;
        }
        CreateGrids();
    }

    private void CreateGrids()
    {
        foreach (var grid in _shipConfig.Grids)
        {
            var listCell = new List<Cell>();
            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Cols; j++)
                {
                    var go = Instantiate(_shipConfig.Cell, grid.Transform);
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







    private bool _isDragActive = false;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;


    private GameObject _gameObjectSlected;
    public void Update()
    {
        if (_isDragActive)
        {
            if ((Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
            {
                Drop();
                return;

            }
        }
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            _screenPosition = new Vector2(mousePosition.x, mousePosition.y);
        }
        else if (Input.touchCount > 0)
        {
            _screenPosition = Input.GetTouch(0).position;
        }
        else
            return;

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
        if (_isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                _gameObjectSlected = hit.collider.gameObject;
                Debug.Log("_gameObjectSlected: " + _gameObjectSlected.name);
                InitDrag();
            }
        }

    }
    private void InitDrag()
    {
        _isDragActive = true;
    }

    private void Drag()
    {
        _gameObjectSlected.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    private void Drop()
    {
        _isDragActive = false;

    }


}
