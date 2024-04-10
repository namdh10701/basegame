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
    private bool _isDragging = false;
    GameObject _hitObject;
    GameObject _dragObject;

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

                    var offsetX = i * (size.x + 0.02f);
                    var offsetY = j * (size.y + 0.02f);

                    Debug.Log("Vector2: " + offsetX + "," + offsetY);
                    cell.Setup(new Vector2(i * size.x / 2, j * size.y / 2));

                    go.transform.localPosition = new Vector2(i * size.x / 2, j * size.y / 2);
                    listCell.Add(cell);
                }
            }

            _gridsInfor.Add(listCell);

        }

    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _hitObject = hit.collider.gameObject;
                // _hitObject.gameObject.SetActive(false);
                // _dragObject = new GameObject();
                // _isDragging = (_dragObject != null) ? true : false;
                _isDragging = true;

            }
        }
        if (_isDragging)
        {
            Vector3 pos = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            _hitObject.transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            _hitObject.gameObject.SetActive(true);

        }
    }
}
