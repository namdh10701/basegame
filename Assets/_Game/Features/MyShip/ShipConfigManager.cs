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
        
        public DraggingItem GetDragItemPrefab(InventoryItem item)
        {
            var shapePath = $"SetupItems/SetupItem_{item.Type.ToString().ToLower()}_{item.OperationType}";
            var prefab = Resources.Load<DraggingItem>(shapePath);
            return prefab;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var cell = _activedCells.FirstOrDefault();
            if (cell)
            {
                PlaceItem(_inventorySheetItem.InventoryItem, cell.Position);
            }

            _inventorySheetItem = null;
            ClearActiveCells();
        }

        InventorySheetItem _inventorySheetItem;
        private List<SlotCell> _activedCells = new();
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!eventData.pointerDrag)
            {
                return;
            }
            _inventorySheetItem = eventData.pointerDrag.GetComponent<InventorySheetItem>();
            HandlePointerMove(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inventorySheetItem = null;
            ClearActiveCells();
        }

        private void ClearActiveCells()
        {
            _activedCells.ForEach(c => c.IsHighLight = false);
            _activedCells.Clear();
        }

        public void OnPointerMove(PointerEventData eventData) => HandlePointerMove(eventData);

        private void HandlePointerMove(PointerEventData eventData)
        {
            var hoveredCell = cellTracker.FindSlotCell(eventData.position);

            if (!hoveredCell || !_inventorySheetItem) return;

            var cells = Vector2IntArrayUtils
                .Shift(
                    _inventorySheetItem.DraggingItem.Shape.Data, hoveredCell.Position
                )
                .Select(c => Grid.GetCell(c))
                .Where(c => c)
                .ToList();
            
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
                if (_activedCells.Contains(cell)) continue;
                cell.IsHighLight = true;
                _activedCells.Add(cell);
            }
        }
    }
}
