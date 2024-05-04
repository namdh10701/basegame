
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
        GameObject draggingObject;
        IGridItem draggingGridItem;
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
                        draggingObject = icd.Item;
                        draggingGridItem = icd.Item.GetComponent<IGridItem>();
                        selectItemDef = draggingGridItem.Def;
                        draggingGridItem.Behaviour.gameObject.SetActive(false);
                        List<Cell> cells = draggingGridItem.OccupyCells;
                        foreach (Cell cell in cells)
                        {
                            cell.GridItem = null;
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
                ShipSetup.RemoveGridItem(itemRef);
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
            gameObject.GetComponent<IGridItem>().OccupyCells = cells;
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
            gridItemData.OccupyCells = cells;
            OnGridItemPlaced.Invoke(selectItemDef);
            ShipSetup.AddNewGridItem(gridItemData);
        }
    }
}

