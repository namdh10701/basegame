
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts.Entities;
using _Game.Scripts;
using System;
using System.Collections.Generic;

using UnityEngine;
namespace _Game.Scripts
{
    public class DragController : MonoBehaviour
    {
        [SerializeField] GridItemReferenceHolder gridItemReferenceHolder;
        public Action<GridItemDef> OnGridItemPlaced;
        public Action<GridItemDef> OnGridItemUp;

        public LayerMask layerMask;
        public LayerMask dragGameObjectMask;


        List<Cell> cells = new List<Cell>();

        public ShipSetup ShipSetup;
        public Grid Grid;
        private Camera _camera;
        private GameObject _draggingObject;
        private IGridItem _draggingGridItem;
        private Cell _selectCell;
        private GridItemDef _selectItemDef;
        private bool _isAceppted;
        private Vector3 _finalPos;
        private Vector3 _anchorOffset;

        void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
#if UNITY_EDITOR
            HandleMouse();
#else
            HandleTouch();
#endif

        }

        void HandleMouse()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, dragGameObjectMask);
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out ItemClickDetector icd))
                    {
                        _draggingObject = icd.Item;
                        _draggingGridItem = icd.Item.GetComponent<IGridItem>();
                        _selectItemDef = _draggingGridItem.Def;
                        _draggingGridItem.Behaviour.gameObject.SetActive(false);
                        Grid.RemoveGridItem(_draggingGridItem.Def);

                        List<Cell> cells = _draggingGridItem.OccupyCells;
                        if (cells != null)
                        {
                            foreach (Cell cell in cells)
                            {
                                cell.GridItem = null;
                            }
                        }

                    }
                }
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                if (_draggingObject != null)
                    OnPointerUp(_selectItemDef);
            }

            if (_draggingObject != null)
            {
                Vector3 mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);

                if (hit.collider != null)
                {
                    // Check if the hit object has the "Cell" component
                    if (hit.collider.TryGetComponent(out Cell cell))
                    {
                        if (_selectCell != null)
                        {
                            if (_selectCell != cell)
                            {
                                ResetHighlightCell();
                                _selectCell.CellRenderer.ToggleHighlight(HighlightType.Normal);
                                _selectCell = cell;
                                if (_draggingGridItem.Def.ShapeId != 0)
                                {
                                    int[,] itemShape = Shape.ShapeDic[_draggingGridItem.Def.ShapeId];
                                    cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(itemShape, _selectCell);
                                    if (GridHelper.IsCellsCoveredShape(cells, itemShape))
                                    {
                                        Vector3 position = GridHelper.GetAveragePosition(cells);
                                        _draggingObject.transform.position = position;
                                        _finalPos = position;

                                        if (IsCellsEmpty(cells))
                                        {
                                            _isAceppted = true;
                                            ToggleHighlight(cells, HighlightType.Accepted);
                                        }
                                        else
                                        {
                                            _isAceppted = false;
                                            ToggleHighlight(cells, HighlightType.Denied);
                                        }
                                    }
                                    else
                                    {
                                        _isAceppted = false;
                                        ToggleHighlight(cells, HighlightType.Denied);
                                    }
                                }
                                else
                                {
                                    Vector3 position = _selectCell.transform.position;
                                    _draggingObject.transform.position = position;
                                    _finalPos = position;
                                    cells = new List<Cell>() { _selectCell };
                                    if (_selectCell.GridItem == null)
                                    {
                                        _selectCell.CellRenderer.ToggleHighlight(HighlightType.Accepted);
                                        _isAceppted = true;
                                    }
                                    else
                                    {
                                        _selectCell.CellRenderer.ToggleHighlight(HighlightType.Denied);
                                        _isAceppted = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            _selectCell = cell;
                            ResetHighlightCell();
                            _selectCell.CellRenderer.ToggleHighlight(HighlightType.Normal);
                            _selectCell = cell;
                            if (_draggingGridItem.Def.ShapeId != 0)
                            {
                                int[,] itemShape = Shape.ShapeDic[_draggingGridItem.Def.ShapeId];
                                cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(itemShape, _selectCell);
                                if (GridHelper.IsCellsCoveredShape(cells, itemShape))
                                {
                                    Vector3 position = GridHelper.GetAveragePosition(cells);
                                    _draggingObject.transform.position = position;
                                    _finalPos = position;

                                    if (IsCellsEmpty(cells))
                                    {
                                        _isAceppted = true;
                                        ToggleHighlight(cells, HighlightType.Accepted);
                                    }
                                    else
                                    {
                                        _isAceppted = false;
                                        ToggleHighlight(cells, HighlightType.Denied);
                                    }
                                }
                                else
                                {
                                    _isAceppted = false;
                                    ToggleHighlight(cells, HighlightType.Denied);
                                }
                            }
                            else
                            {
                                cells = new List<Cell>() { _selectCell };
                                Vector3 position = _selectCell.transform.position;
                                _draggingObject.transform.position = position;
                                _finalPos = position;
                                if (_selectCell.GridItem == null)
                                {
                                    _selectCell.CellRenderer.ToggleHighlight(HighlightType.Accepted);
                                    _isAceppted = true;
                                }
                                else
                                {
                                    _selectCell.CellRenderer.ToggleHighlight(HighlightType.Denied);
                                    _isAceppted = false;
                                }
                            }

                        }
                    }
                    else
                    {
                        _isAceppted = false;
                    }
                }
                else
                {
                    ResetHighlightCell();
                    _isAceppted = false;
                }

                if (!_isAceppted)
                {
                    mousePosition.z = 0;
                    Vector3 dragPos = mousePosition - _anchorOffset;
                    _draggingObject.transform.position = dragPos;
                }
            }
        }

        void HandleTouch()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                Vector3 mousePosition;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        mousePosition = _camera.ScreenToWorldPoint(touch.position);
                        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, dragGameObjectMask);
                        if (hit.collider != null)
                        {
                            if (hit.collider.TryGetComponent(out ItemClickDetector icd))
                            {
                                _draggingObject = icd.Item;
                                _draggingGridItem = icd.Item.GetComponent<IGridItem>();
                                _selectItemDef = _draggingGridItem.Def;
                                _draggingGridItem.Behaviour.gameObject.SetActive(false);



                                Grid.RemoveGridItem(_draggingGridItem.Def);
                                List<Cell> cells = _draggingGridItem.OccupyCells;
                                if (cells != null)
                                {
                                    foreach (Cell cell in cells)
                                    {
                                        cell.GridItem = null;
                                    }
                                }
                            }
                        }
                        break;
                    case TouchPhase.Moved:
                        mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                        RaycastHit2D hit1 = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);

                        if (hit1.collider != null)
                        {
                            // Check if the hit object has the "Cell" component
                            if (hit1.collider.TryGetComponent(out Cell cell))
                            {
                                if (_selectCell != null)
                                {
                                    if (_selectCell != cell)
                                    {
                                        ResetHighlightCell();
                                        _selectCell.CellRenderer.ToggleHighlight(HighlightType.Normal);
                                        _selectCell = cell;
                                        if (_draggingGridItem.Def.ShapeId != 0)
                                        {
                                            int[,] itemShape = Shape.ShapeDic[_draggingGridItem.Def.ShapeId];
                                            cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(itemShape, _selectCell);
                                            Debug.Log(cells.Count);
                                            if (GridHelper.IsCellsCoveredShape(cells, itemShape))
                                            {
                                                Vector3 position = GridHelper.GetAveragePosition(cells);
                                                _draggingObject.transform.position = position;
                                                _finalPos = position;

                                                if (IsCellsEmpty(cells))
                                                {
                                                    _isAceppted = true;
                                                    ToggleHighlight(cells, HighlightType.Accepted);
                                                }
                                                else
                                                {
                                                    _isAceppted = false;
                                                    ToggleHighlight(cells, HighlightType.Denied);
                                                }
                                            }
                                            else
                                            {
                                                _isAceppted = false;
                                                ToggleHighlight(cells, HighlightType.Denied);
                                            }
                                        }
                                        else
                                        {
                                            Vector3 position = _selectCell.transform.position;
                                            _draggingObject.transform.position = position;
                                            cells = new List<Cell>() { _selectCell };
                                            _finalPos = position;
                                            if (_selectCell.GridItem == null)
                                            {
                                                _selectCell.CellRenderer.ToggleHighlight(HighlightType.Accepted);
                                                _isAceppted = true;
                                            }
                                            else
                                            {
                                                _selectCell.CellRenderer.ToggleHighlight(HighlightType.Denied);
                                                _isAceppted = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    _selectCell = cell;
                                    ResetHighlightCell();
                                    _selectCell.CellRenderer.ToggleHighlight(HighlightType.Normal);
                                    _selectCell = cell;
                                    if (_draggingGridItem.Def.ShapeId != 0)
                                    {
                                        int[,] itemShape = Shape.ShapeDic[_draggingGridItem.Def.ShapeId];
                                        cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(itemShape, _selectCell);
                                        if (GridHelper.IsCellsCoveredShape(cells, itemShape))
                                        {
                                            Vector3 position = GridHelper.GetAveragePosition(cells);
                                            _draggingObject.transform.position = position;
                                            _finalPos = position;

                                            if (IsCellsEmpty(cells))
                                            {
                                                _isAceppted = true;
                                                ToggleHighlight(cells, HighlightType.Accepted);
                                            }
                                            else
                                            {
                                                _isAceppted = false;
                                                ToggleHighlight(cells, HighlightType.Denied);
                                            }
                                        }
                                        else
                                        {
                                            _isAceppted = false;
                                            ToggleHighlight(cells, HighlightType.Denied);
                                        }
                                    }
                                    else
                                    {
                                        Vector3 position = _selectCell.transform.position;
                                        _draggingObject.transform.position = position;
                                        _finalPos = position;
                                        if (_selectCell.GridItem == null)
                                        {
                                            _selectCell.CellRenderer.ToggleHighlight(HighlightType.Accepted);
                                            _isAceppted = true;
                                        }
                                        else
                                        {
                                            _selectCell.CellRenderer.ToggleHighlight(HighlightType.Denied);
                                            _isAceppted = false;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                _isAceppted = false;
                            }
                        }
                        else
                        {
                            ResetHighlightCell();
                            _isAceppted = false;
                        }

                        if (!_isAceppted)
                        {
                            mousePosition.z = 0;
                            Vector3 dragPos = mousePosition - _anchorOffset;
                            _draggingObject.transform.position = dragPos;
                        }
                        break;
                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                        if (_draggingObject != null)
                            OnPointerUp(_selectItemDef);
                        break;
                }
            }
        }
        bool IsCellsEmpty(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                if (cell.GridItem != null)
                {
                    return false;
                }
            }
            return true;
        }


        void ToggleHighlight(List<Cell> cells, HighlightType type)
        {
            foreach (Cell cell in cells)
            {
                cell.CellRenderer.ToggleHighlight(type);
            }
        }

        void ResetHighlightCell()
        {
            foreach (Cell cell in cells)
            {
                cell.CellRenderer.ToggleHighlight(HighlightType.Normal);
            }
        }
        public void OnPointerDown(GridItemDef itemRef)
        {
            this._selectItemDef = itemRef;
            _draggingObject = Instantiate(ResourceLoader.LoadGridItemPrefab(itemRef));
#if UNITY_EDITOR
            _draggingObject.transform.position = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
#else

            draggingObject.transform.position = _camera.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position);
#endif
            _draggingGridItem = _draggingObject.GetComponent<IGridItem>();
            _draggingGridItem.Behaviour.gameObject.SetActive(false);
            _anchorOffset = _draggingObject.transform.Find("Anchor").localPosition;
        }

        public void OnPointerUp(GridItemDef itemRef)
        {
            if (_draggingGridItem.Def.ShapeId == 0 && _selectCell != null)
            {
                _selectCell.CellRenderer.ToggleHighlight(HighlightType.Normal);
            }

            if (_isAceppted)
            {
                SpawnGridItem(_finalPos);
                _selectCell = null;
            }
            else
            {
                Debug.Log("UP " + itemRef.Id);
                OnGridItemUp?.Invoke(itemRef);

            }

            ResetHighlightCell();

            Destroy(_draggingObject.gameObject);
            _draggingObject = null;
            _draggingGridItem = null;
        }


        void SpawnGridItem(Vector3 position)
        {
            GameObject gameObject = Instantiate(_draggingObject, _selectCell.Grid.GridItemRoot);

            float scale = Vector3.one.x / gameObject.transform.parent.lossyScale.x;
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
            gameObject.transform.position = position;
            List<Cell> occupyCells = new List<Cell>();

            foreach (Cell cell in cells)
            {
                occupyCells.Add(cell);
            }

            gameObject.GetComponent<IGridItem>().OccupyCells = occupyCells;
            gameObject.GetComponent<IGridItem>().GridId = cells[0].Grid.Id;
            if (_draggingGridItem.Def.ShapeId == 0)
            {
                _selectCell.GridItem = gameObject.GetComponent<IGridItem>();
            }
            else
            {
                foreach (Cell cell in cells)
                {
                    cell.GridItem = gameObject.GetComponent<IGridItem>();
                }
            }
            GridItemData gridItemData = new GridItemData();
            gridItemData.GridId = _selectCell.Grid.Id;
            gridItemData.position = gameObject.transform.localPosition;
            gridItemData.Def = gameObject.GetComponent<IGridItem>().Def;
            List<Vector2Int> cellPoses = new List<Vector2Int>();
            foreach (Cell cell in cells)
            {
                cellPoses.Add(new Vector2Int(cell.X, cell.Y));
            }
            gridItemData.OccupyCells = cellPoses;
            OnGridItemPlaced?.Invoke(_selectItemDef);
            Grid.AddNewGridItem(gridItemData);
        }
    }
}

