using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using _Game.Features.MyShip.GridSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Features.Inventory
{
    public class ShipGridHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerUpHandler
    {
        public CellRayTracker RayTracker;
        private SlotGrid _grid;
        private ShipSetupItem _shipSetupItem;
        private List<SlotCell> _activeCells = new();

        public SlotGrid Grid
        {
            get
            {
                if (!_grid)
                {
                    _grid = GetComponentInChildren<SlotGrid>(false);
                    if (!_grid)
                    {
                        throw new Exception("Grid in children not found");
                    }
                }

                return _grid;
            }
        }

        public List<SlotCell> ActiveCells => _activeCells.ToList();

        public void OnPointerEnter(PointerEventData eventData)
        {
            

            OnPointerMove(eventData);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            var isDragging = eventData.dragging;
            if (!isDragging) return;

            var draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();

            if (!draggableItem) return;
            
            var inventoryItem = draggableItem.DragDataProvider.GetData<InventoryItem>();
            
            _shipSetupItem = ShipSetupUtils.GetShipSetupItemPrefab(inventoryItem);
            
            if (!_shipSetupItem)
            {
                return;
            }
            
            var mousePos = eventData.position;
            
            // var rect = ((RectTransform)_shipSetupItem.transform);
            //
            // // Convert the screen point to a local point in the raycast target
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(
            //     rect,
            //     eventData.position,
            //     eventData.pressEventCamera, // The camera used to detect the pointer event
            //     out var localPointerPosition
            // );

            var size = Vector2IntArrayUtils.GetDimensions(_shipSetupItem.Shape.Data);

            // var ww = (_shipSetupItem.Shape.Data.Max(v => v.x) + 1) * SlotCell.WIDTH;
            // var hh = (_shipSetupItem.Shape.Data.Max(v => v.y) + 1) * SlotCell.HEIGHT;
            
            // mousePos.x += size.x / 4f;
            // mousePos.y += size.y / 4f;
            
            mousePos.x -= (SlotCell.WIDTH /2f * size.x / 2f);
            mousePos.y += (SlotCell.HEIGHT /2f * size.y / 2f);
            
            var hoveredCell = RayTracker.FindSlotCell(mousePos);
            if (!hoveredCell) return;
            // HandlePointerEvent(eventData);
            var cells = Vector2IntArrayUtils
                .Shift(
                    _shipSetupItem.Shape.Data, hoveredCell.Position
                )
                .Select(c => Grid.GetCell(c))
                .ToList();
            
            if (cells.Contains(null))
            {
                ClearActiveCells();
                return;
            }
            
            SetActiveCells(cells);
        }

        public void SetActiveCells(List<SlotCell> cellsToActive, bool toggleMode = true)
        {
            if (toggleMode)
            {
                // var toRemoves = new List<SlotCell>();
                // foreach (var cell in _activedCells)
                // {
                //     if (cellsToActive.Contains(cell)) continue;
                //     
                //     cell.IsHighLight = false;
                //     toRemoves.Add(cell);
                // }
                // toRemoves.ForEach(v => _activedCells.Remove(v));
                ClearActiveCells();
            }

            foreach (var cell in cellsToActive)
            {
                if (_activeCells.Contains(cell)) continue;
                cell.IsHighLight = true;
                _activeCells.Add(cell);
            }
        }
        
        private void ClearActiveCells()
        {
            _activeCells.ForEach(c => c.IsHighLight = false);
            _activeCells.Clear();
        }

        public void Clear()
        {
            _shipSetupItem = null;
            ClearActiveCells();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // ClearActiveCells();
        }
    }
}