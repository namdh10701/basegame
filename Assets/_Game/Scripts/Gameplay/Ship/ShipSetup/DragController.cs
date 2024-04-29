
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
        GridItem item;
        DragItem dragObject;
        Cell selectCell;
        int[,] dragObjectShape;
        public LayerMask layerMask;
        public LayerMask dragGameObjectMask;
        GridItemReference itemRef;


        bool IsAceppted;
        Vector3 finalPos;
        List<Cell> cells = new List<Cell>();

        public Action<GridItemReference> OnGridItemPlaced;
        public Action<GridItemReference> OnGridItemUp;

        public ShipSetup ShipSetup;
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
                        dragObject = icd.Item.GetComponent<DragItem>();
                        item = dragObject.GetComponent<GridItem>();
                        itemRef = dragObject.GridItemReference;
                        item.behaviour.gameObject.SetActive(false);
                        dragObjectShape = dragObject.GetComponent<GridItem>().Shape;
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
                    OnPointerUp(itemRef);
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
                                if (dragObjectShape != null)
                                {
                                    cells = GridHelper.GetCoveredCellsIfPutShapeAtCell(item.Shape, selectCell);
                                    Vector3 position = GridHelper.GetAveragePosition(cells);
                                    dragObject.transform.position = position;
                                    finalPos = position;
                                    if (GridHelper.IsCellsCoveredShape(cells, item.Shape))
                                    {
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
                        }
                    }
                    else
                    {
                        IsAceppted = false;
                    }
                }
                else
                {
                    IsAceppted = false;
                    mousePosition.z = 0;
                    dragObject.transform.position = mousePosition;
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
        public void OnPointerDown(GridItemReference itemRef)
        {
            this.itemRef = itemRef;
            dragObject = Instantiate(itemRef.Prefab.GetComponent<DragItem>());
            dragObject.GridItemReference = itemRef;
            item = dragObject.GetComponent<GridItem>();
            item.behaviour.gameObject.SetActive(false);
            dragObjectShape = dragObject.GetComponent<GridItem>().Shape;
        }

        public void OnPointerUp(GridItemReference itemRef)
        {
            if (dragObjectShape == null && selectCell != null)
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
                OnGridItemUp.Invoke(this.itemRef);
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

            if (dragObject != null && dragObjectShape != null)
            {
                int rows = dragObjectShape.GetLength(0);
                int cols = dragObjectShape.GetLength(1);
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
            GameObject gameObject = Instantiate(dragObject.gameObject);
            gameObject.transform.position = position;
            gameObject.transform.parent = selectCell.Grid.GridItemRoot;
            gameObject.GetComponent<GridItem>().cells = cells;
            if (dragObjectShape == null)
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
            OnGridItemPlaced.Invoke(itemRef);
            ShipSetup.AddNewGridItem(gridItemData);
        }
    }
}

