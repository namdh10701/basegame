using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using _Game.Features.Inventory;
using _Game.Features.MyShip.GridSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Features.MyShip
{
    public class ShipConfigManager : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        public CellRayTracker cellTracker;
        public SlotGrid Grid;
        public Transform PlacementPane;

        private void Awake()
        {
            // Grid = GetComponentInChildren<SlotGrid>();
            // PlacementPane = GetComponentInChildren<PlacementPane>().transform;
        }

        public DraggingItem PlaceItem(InventoryItem item, Vector2Int position)
        {
            Debug.Log("cell.Position: " + position);
            var prefab = GetDragItemPrefab(item);
            var instance = Instantiate(prefab, PlacementPane);
            var rect = instance.transform as RectTransform;
            rect.anchoredPosition = new Vector3(position.x * SlotCell.WIDTH, position.y * SlotCell.HEIGHT * -1);
            // instance.transform.localPosition = new Vector3(position.x * SlotCell.WIDTH, position.y * SlotCell.HEIGHT); 
            return instance;
        }

        private void MoveItem(DraggingItem draggingItem, Vector2Int position)
        {
            Debug.Log("cell.Position: " + position);
            
            var rect = draggingItem.transform as RectTransform;
            rect.anchoredPosition = new Vector3(position.x * SlotCell.WIDTH, position.y * SlotCell.HEIGHT * -1);
        }
        
        public DraggingItem GetDragItemPrefab(InventoryItem item)
        {
            var shapePath = $"SetupItems/SetupItem_{item.Type.ToString().ToLower()}_{item.OperationType}";
            var prefab = Resources.Load<DraggingItem>(shapePath);
            return prefab;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var cell = _activeCells.FirstOrDefault();
            if (cell)
            {
                if (_inventorySheetItem)
                {
                    PlaceItem(_inventorySheetItem.InventoryItem, cell.Position);
                    _draggingItem.Position = cell.Position;
                }
                else
                {
                    MoveItem(_draggingItem, cell.Position);
                    _draggingItem.Position = cell.Position;
                }
            }
            else
            {
                // _draggingItem.transform.anchoredPosition = _lastDraggingItemPos;
                // _lastDraggingItemPos = Vector3.zero;
                MoveItem(_draggingItem, _draggingItem.Position);
            }

            _inventorySheetItem = null;
            _draggingItem = null;
            ClearActiveCells();
        }
        

        InventorySheetItem _inventorySheetItem;
        private DraggingItem _draggingItem;
        private List<SlotCell> _activeCells = new();
        private List<SlotCell> _beforeMoveActiveCells = new();
        // private Vector3 _lastDraggingItemPos;

        public void OnPointerEnter(PointerEventData eventData)
        {
            HandlePointerMove(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inventorySheetItem = null;
            _draggingItem = null;
            ClearActiveCells();
        }

        private void ClearActiveCells()
        {
            _activeCells.ForEach(c => c.IsHighLight = false);
            _activeCells.Clear();
        }

        public void OnPointerMove(PointerEventData eventData) => HandlePointerMove(eventData);

        private void HandlePointerMove(PointerEventData eventData)
        {
            if (!eventData.pointerDrag)
            {
                return;
            }

            if (!_inventorySheetItem)
            {
                _inventorySheetItem = eventData.pointerDrag.GetComponent<InventorySheetItem>();
                _draggingItem = _inventorySheetItem != null ? _inventorySheetItem.DraggingItem : null;
            }
            
            if (!_draggingItem)
            {
                _draggingItem = eventData.pointerDrag.GetComponent<DraggingItem>();
                // _lastDraggingItemPos = _draggingItem.transform.position;
            }
            
            
            
            
            // var hoveredCell = cellTracker.FindSlotCell(((RectTransform) _draggingItem.transform).rect.position);
            var mousePos = eventData.position;
            if (!_inventorySheetItem)
            {
                var rect = ((RectTransform)_draggingItem.transform);

                // Convert the screen point to a local point in the raycast target
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rect,
                    eventData.position,
                    eventData.pressEventCamera, // The camera used to detect the pointer event
                    out var localPointerPosition
                );
                
                
                mousePos.x -= localPointerPosition.x / 2;
                mousePos.y -= localPointerPosition.y / 2;
            }
            var hoveredCell = cellTracker.FindSlotCell(mousePos);

            if (!hoveredCell || !_draggingItem) return;

            var cells = Vector2IntArrayUtils
                .Shift(
                    _draggingItem.Shape.Data, hoveredCell.Position
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
    }
}
