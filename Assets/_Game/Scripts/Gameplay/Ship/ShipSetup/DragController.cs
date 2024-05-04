
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts.Entities;
using _Game.Scripts.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
        GridItem dragObject;
        Cell selectCell;
        GridItemDef selectItemDef;
        bool IsAceppted;
        Vector3 finalPos;
        Vector3 anchorOffset;

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, dragGameObjectMask);
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out ItemClickDetector icd))
                    {
                        dragObject = icd.Item.GetComponent<GridItem>();
                        selectItemDef = dragObject.Def;
                        dragObject.behaviour.gameObject.SetActive(false);
                        List<Cell> cells = dragObject.GetComponent<GridItem>().cells;
                        foreach (Cell cell in cells)
                        {
                            cell.GridItem = null;
                        }
                    }
                }
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                if (dragObject != null)
                    OnPointerUp(selectItemDef);
            }

            if (dragObject != null)
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
                                if (dragObject.Shape != null)
                                {
                                    cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(dragObject.Shape, selectCell);
                                    if (GridHelper.IsCellsCoveredShape(cells, dragObject.Shape))
                                    {
                                        Vector3 position = GridHelper.GetAveragePosition(cells);
                                        dragObject.transform.position = position;
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
                                    dragObject.transform.position = position;
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
                            if (dragObject.Shape != null)
                            {
                                cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(dragObject.Shape, selectCell);
                                if (GridHelper.IsCellsCoveredShape(cells, dragObject.Shape))
                                {
                                    Vector3 position = GridHelper.GetAveragePosition(cells);
                                    dragObject.transform.position = position;
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
                                dragObject.transform.position = position;
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
                    dragObject.transform.position = dragPos;
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
            dragObject = Instantiate(gridItemReferenceHolder.GetItemByDef(itemRef));
            dragObject.behaviour.gameObject.SetActive(false);
            anchorOffset = dragObject.transform.Find("Anchor").localPosition;
        }

        public void OnPointerUp(GridItemDef itemRef)
        {
            if (dragObject.Shape == null && selectCell != null)
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
                ShipSetup.RemoveGridItem(itemRef);
            }

            ResetHighlightCell();

            Destroy(dragObject.gameObject);
            dragObject = null;
        }

        List<Cell> TryPutGridItemAtCell(Cell selectCell)
        {
            Grid grid = selectCell.Grid;
            List<Cell> cells = new List<Cell>();

            dragObject.transform.position = selectCell.transform.position;

            if (dragObject != null && dragObject.Shape != null)
            {
                int rows = dragObject.Shape.GetLength(0);
                int cols = dragObject.Shape.GetLength(1);
                int startX = selectCell.Y;
                int startY = selectCell.X;
                if (startX + rows <= grid.Row && startY + cols <= grid.Col)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            Cell currentCell = grid.Cells[startX + i, startY + j];
                            cells.Add(currentCell);
                        }
                    }
                    return cells;
                }
                return cells;
            }
            return cells;
        }


        void SpawnGridItem(Vector3 position)
        {
            GameObject gameObject = Instantiate(dragObject.gameObject, selectCell.Grid.GridItemRoot);
            float scale = Vector3.one.x / gameObject.transform.parent.lossyScale.x;
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
            gameObject.transform.position = position;
            gameObject.GetComponent<GridItem>().cells = cells;
            if (dragObject.Shape == null)
            {
                selectCell.GridItem = gameObject.GetComponent<GridItem>();
            }
            else
            {
                foreach (Cell cell in cells)
                {
                    cell.GridItem = gameObject.GetComponent<GridItem>();
                }
            }
            GridItemData gridItemData = new GridItemData();
            gridItemData.GridId = selectCell.Grid.Id;
            gridItemData.position = gameObject.transform.localPosition;
            gridItemData.Def = gameObject.GetComponent<GridItem>().Def;
            gridItemData.OccupyCells = cells;
            OnGridItemPlaced.Invoke(selectItemDef);
            ShipSetup.AddNewGridItem(gridItemData);
        }
    }
}

