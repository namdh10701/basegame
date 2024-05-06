
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts.Entities;
using _Game.Scripts.Input;
using System;
using System.Collections.Generic;

using UnityEngine;
namespace _Game.Scripts
{
    public class DragController : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] GridItemReferenceHolder gridItemReferenceHolder;
        public Action<GridItemDef> OnGridItemPlaced;
        public Action<GridItemDef> OnGridItemUp;

        public LayerMask layerMask;
        public LayerMask dragGameObjectMask;


        List<Cell> cells = new List<Cell>();

        public ShipSetup ShipSetup;
        GameObject draggingObject;
        IGridItem draggingGridItem;
        Cell selectCell;
        GridItemDef selectItemDef;
        bool IsAceppted;
        Vector3 finalPos;
        Vector3 anchorOffset;

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
                        draggingObject = icd.Item;
                        draggingGridItem = icd.Item.GetComponent<IGridItem>();
                        selectItemDef = draggingGridItem.Def;
                        draggingGridItem.Behaviour.gameObject.SetActive(false);
                        ShipSetup.RemoveGridItem(draggingGridItem.Def);

                        List<Vector2Int> cells = draggingGridItem.OccupyCells;
                        if (cells != null)
                        {
                            foreach (Vector2Int cell in cells)
                            {
                                ShipSetup.Grids[int.Parse(draggingGridItem.GridId) - 1].Cells[cell.y, cell.x].GridItem = null;
                            }
                        }

                    }
                }
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                if (draggingObject != null)
                    OnPointerUp(selectItemDef);
            }

            if (draggingObject != null)
            {
                Vector3 mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layerMask);

                if (hit.collider != null)
                {
                    // Check if the hit object has the "Cell" component
                    if (hit.collider.TryGetComponent(out Cell cell))
                    {
                        if (selectCell != null)
                        {
                            if (selectCell != cell)
                            {
                                ResetHighlightCell();
                                selectCell.CellRenderer.ToggleHighlight(HighlightType.Normal);
                                selectCell = cell;
                                if (draggingGridItem.Def.ShapeId != 0)
                                {
                                    int[,] itemShape = Shape.ShapeDic[draggingGridItem.Def.ShapeId];
                                    cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(itemShape, selectCell);
                                    if (GridHelper.IsCellsCoveredShape(cells, itemShape))
                                    {
                                        Vector3 position = GridHelper.GetAveragePosition(cells);
                                        draggingObject.transform.position = position;
                                        finalPos = position;

                                        if (IsCellsEmpty(cells))
                                        {
                                            IsAceppted = true;
                                            ToggleHighlight(cells, HighlightType.Accepted);
                                        }
                                        else
                                        {
                                            IsAceppted = false;
                                            ToggleHighlight(cells, HighlightType.Denied);
                                        }
                                    }
                                    else
                                    {
                                        IsAceppted = false;
                                        ToggleHighlight(cells, HighlightType.Denied);
                                    }
                                }
                                else
                                {
                                    Vector3 position = selectCell.transform.position;
                                    draggingObject.transform.position = position;
                                    finalPos = position;
                                    cells = new List<Cell>() { selectCell };
                                    if (selectCell.GridItem == null)
                                    {
                                        selectCell.CellRenderer.ToggleHighlight(HighlightType.Accepted);
                                        IsAceppted = true;
                                    }
                                    else
                                    {
                                        selectCell.CellRenderer.ToggleHighlight(HighlightType.Denied);
                                        IsAceppted = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            selectCell = cell;
                            ResetHighlightCell();
                            selectCell.CellRenderer.ToggleHighlight(HighlightType.Normal);
                            selectCell = cell;
                            if (draggingGridItem.Def.ShapeId != 0)
                            {
                                int[,] itemShape = Shape.ShapeDic[draggingGridItem.Def.ShapeId];
                                cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(itemShape, selectCell);
                                if (GridHelper.IsCellsCoveredShape(cells, itemShape))
                                {
                                    Vector3 position = GridHelper.GetAveragePosition(cells);
                                    draggingObject.transform.position = position;
                                    finalPos = position;

                                    if (IsCellsEmpty(cells))
                                    {
                                        IsAceppted = true;
                                        ToggleHighlight(cells, HighlightType.Accepted);
                                    }
                                    else
                                    {
                                        IsAceppted = false;
                                        ToggleHighlight(cells, HighlightType.Denied);
                                    }
                                }
                                else
                                {
                                    IsAceppted = false;
                                    ToggleHighlight(cells, HighlightType.Denied);
                                }
                            }
                            else
                            {
                                cells = new List<Cell>() { selectCell };
                                Vector3 position = selectCell.transform.position;
                                draggingObject.transform.position = position;
                                finalPos = position;
                                if (selectCell.GridItem == null)
                                {
                                    selectCell.CellRenderer.ToggleHighlight(HighlightType.Accepted);
                                    IsAceppted = true;
                                }
                                else
                                {
                                    selectCell.CellRenderer.ToggleHighlight(HighlightType.Denied);
                                    IsAceppted = false;
                                }
                            }

                        }
                    }
                    else
                    {
                        IsAceppted = false;
                    }
                }
                else
                {
                    ResetHighlightCell();
                    IsAceppted = false;
                }

                if (!IsAceppted)
                {
                    mousePosition.z = 0;
                    Vector3 dragPos = mousePosition - anchorOffset;
                    draggingObject.transform.position = dragPos;
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
                                draggingObject = icd.Item;
                                draggingGridItem = icd.Item.GetComponent<IGridItem>();
                                selectItemDef = draggingGridItem.Def;
                                draggingGridItem.Behaviour.gameObject.SetActive(false);



                                ShipSetup.RemoveGridItem(draggingGridItem.Def);
                                List<Vector2Int> cells = draggingGridItem.OccupyCells;
                                if (cells != null)
                                {
                                    foreach (Vector2Int cell in cells)
                                    {
                                        ShipSetup.Grids[int.Parse(draggingGridItem.GridId) - 1].Cells[cell.y, cell.x].GridItem = null;
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
                                if (selectCell != null)
                                {
                                    if (selectCell != cell)
                                    {
                                        ResetHighlightCell();
                                        selectCell.CellRenderer.ToggleHighlight(HighlightType.Normal);
                                        selectCell = cell;
                                        if (draggingGridItem.Def.ShapeId != 0)
                                        {
                                            int[,] itemShape = Shape.ShapeDic[draggingGridItem.Def.ShapeId];
                                            cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(itemShape, selectCell);
                                            Debug.Log(cells.Count);
                                            if (GridHelper.IsCellsCoveredShape(cells, itemShape))
                                            {
                                                Vector3 position = GridHelper.GetAveragePosition(cells);
                                                draggingObject.transform.position = position;
                                                finalPos = position;

                                                if (IsCellsEmpty(cells))
                                                {
                                                    IsAceppted = true;
                                                    ToggleHighlight(cells, HighlightType.Accepted);
                                                }
                                                else
                                                {
                                                    IsAceppted = false;
                                                    ToggleHighlight(cells, HighlightType.Denied);
                                                }
                                            }
                                            else
                                            {
                                                IsAceppted = false;
                                                ToggleHighlight(cells, HighlightType.Denied);
                                            }
                                        }
                                        else
                                        {
                                            Vector3 position = selectCell.transform.position;
                                            draggingObject.transform.position = position;
                                            cells = new List<Cell>() { selectCell };
                                            finalPos = position;
                                            if (selectCell.GridItem == null)
                                            {
                                                selectCell.CellRenderer.ToggleHighlight(HighlightType.Accepted);
                                                IsAceppted = true;
                                            }
                                            else
                                            {
                                                selectCell.CellRenderer.ToggleHighlight(HighlightType.Denied);
                                                IsAceppted = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    selectCell = cell;
                                    ResetHighlightCell();
                                    selectCell.CellRenderer.ToggleHighlight(HighlightType.Normal);
                                    selectCell = cell;
                                    if (draggingGridItem.Def.ShapeId != 0)
                                    {
                                        int[,] itemShape = Shape.ShapeDic[draggingGridItem.Def.ShapeId];
                                        cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(itemShape, selectCell);
                                        if (GridHelper.IsCellsCoveredShape(cells, itemShape))
                                        {
                                            Vector3 position = GridHelper.GetAveragePosition(cells);
                                            draggingObject.transform.position = position;
                                            finalPos = position;

                                            if (IsCellsEmpty(cells))
                                            {
                                                IsAceppted = true;
                                                ToggleHighlight(cells, HighlightType.Accepted);
                                            }
                                            else
                                            {
                                                IsAceppted = false;
                                                ToggleHighlight(cells, HighlightType.Denied);
                                            }
                                        }
                                        else
                                        {
                                            IsAceppted = false;
                                            ToggleHighlight(cells, HighlightType.Denied);
                                        }
                                    }
                                    else
                                    {
                                        Vector3 position = selectCell.transform.position;
                                        draggingObject.transform.position = position;
                                        finalPos = position;
                                        if (selectCell.GridItem == null)
                                        {
                                            selectCell.CellRenderer.ToggleHighlight(HighlightType.Accepted);
                                            IsAceppted = true;
                                        }
                                        else
                                        {
                                            selectCell.CellRenderer.ToggleHighlight(HighlightType.Denied);
                                            IsAceppted = false;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                IsAceppted = false;
                            }
                        }
                        else
                        {
                            ResetHighlightCell();
                            IsAceppted = false;
                        }

                        if (!IsAceppted)
                        {
                            mousePosition.z = 0;
                            Vector3 dragPos = mousePosition - anchorOffset;
                            draggingObject.transform.position = dragPos;
                        }
                        break;
                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                        if (draggingObject != null)
                            OnPointerUp(selectItemDef);
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
            this.selectItemDef = itemRef;
            draggingObject = Instantiate(ResourceLoader.LoadGridItemPrefab(itemRef));
#if UNITY_EDITOR
            draggingObject.transform.position = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
#else

            draggingObject.transform.position = _camera.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position);
#endif
            draggingGridItem = draggingObject.GetComponent<IGridItem>();
            draggingGridItem.Behaviour.gameObject.SetActive(false);
            anchorOffset = draggingObject.transform.Find("Anchor").localPosition;
        }

        public void OnPointerUp(GridItemDef itemRef)
        {
            if (draggingGridItem.Def.ShapeId == 0 && selectCell != null)
            {
                selectCell.CellRenderer.ToggleHighlight(HighlightType.Normal);
            }

            if (IsAceppted)
            {
                SpawnGridItem(finalPos);
                selectCell = null;
            }
            else
            {
                Debug.Log("UP " + itemRef.Id);
                OnGridItemUp.Invoke(itemRef);

            }

            ResetHighlightCell();

            Destroy(draggingObject.gameObject);
            draggingObject = null;
            draggingGridItem = null;
        }


        void SpawnGridItem(Vector3 position)
        {
            GameObject gameObject = Instantiate(draggingObject, selectCell.Grid.GridItemRoot);

            float scale = Vector3.one.x / gameObject.transform.parent.lossyScale.x;
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
            gameObject.transform.position = position;
            List<Vector2Int> occupyCells = new List<Vector2Int>();

            foreach (Cell cell in cells)
            {
                occupyCells.Add(new Vector2Int(cell.X, cell.Y));
            }

            gameObject.GetComponent<IGridItem>().OccupyCells = occupyCells;
            gameObject.GetComponent<IGridItem>().GridId = cells[0].Grid.Id;
            if (draggingGridItem.Def.ShapeId == 0)
            {
                selectCell.GridItem = gameObject.GetComponent<IGridItem>();
            }
            else
            {
                foreach (Cell cell in cells)
                {
                    cell.GridItem = gameObject.GetComponent<IGridItem>();
                }
            }
            GridItemData gridItemData = new GridItemData();
            gridItemData.GridId = selectCell.Grid.Id;
            gridItemData.position = gameObject.transform.localPosition;
            gridItemData.Def = gameObject.GetComponent<IGridItem>().Def;
            gridItemData.OccupyCells = occupyCells;
            OnGridItemPlaced.Invoke(selectItemDef);
            ShipSetup.AddNewGridItem(gridItemData);
        }
    }
}

