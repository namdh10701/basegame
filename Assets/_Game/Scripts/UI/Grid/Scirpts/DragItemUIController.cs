using System.Collections.Generic;
using _Game.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Base.Scripts.UI
{
    public class DragItemUIController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private InventoryItem _inventoryItem;
        public RectTransform Grid;
        private RectTransform _parent;
        private RectTransform m_rect;
        private GridManager _gridManager;
        List<Cell> _previouCells = new List<Cell>();
        List<Cell> _nextCells = new List<Cell>();
        List<Cell> _curentCells = new List<Cell>();
        GridInfor _previousGrid;
        Vector2 _previousPosition;
        void Start()
        {
            m_rect = this.gameObject.GetComponentInParent<RectTransform>();
            _gridManager = this.gameObject.GetComponentInParent<GridManager>();
            _parent = this.gameObject.transform.parent.GetComponent<RectTransform>();
            Debug.Log("DragItemUIController" + _parent.name);
            _previousPosition = m_rect.anchoredPosition;
        }

        public void SetParent(RectTransform parent)
        {
            _parent = parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

            Debug.Log("OnBeginDrag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_parent, Input.mousePosition, Camera.main, out position);
            m_rect.anchoredPosition = position;
            var result = _gridManager.GetCellsByPosition(this, Input.mousePosition, position, _inventoryItem, _curentCells.Count == 0);
            // if (result.cells.Count == 0 ) return;
            if (result.grid == null || result.cells.Count == 0)
            {
                _gridManager.OnDragChangeColorCells(_nextCells, _previousGrid, true);
            }
            else
                ChangeColor(result.cells, result.grid);
        }

        private void ChangeColor(List<Cell> cells, GridInfor grid)
        {
            if (_curentCells.Count == 0)
                _curentCells = cells;
            if (cells != _nextCells)
            {
                if (_nextCells != null && _nextCells.Count > 0)
                {
                    _gridManager.OnDragChangeColorCells(_nextCells, grid, true);
                }
                _nextCells = cells;
                _gridManager.OnDragChangeColorCells(_nextCells, grid);
                _previouCells = _nextCells;
                _previousGrid = grid;
            }


        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_nextCells.Count > 0 && CanMoveItem(_nextCells))
            {
                _gridManager.ChangeStatusCellEndDrag(_nextCells, _previousGrid, StatusCell.Empty);

                var inventoryItemsInfoRemoveGrid = new List<InventoryItemInfo>();
                inventoryItemsInfoRemoveGrid.Add(_inventoryItem.GetInventorInfo());
                _gridManager.RemoveInventoryItemsInfo(inventoryItemsInfoRemoveGrid, _previousGrid);

                // Get pos 
                _inventoryItem.GetInventorInfo().inventoryItemData.position = _nextCells[0].GetCellData().position;
                _inventoryItem.GetInventorInfo().inventoryItemData.gridID = _previousGrid.id;
                var shape = Shape.ShapeDic[_inventoryItem.GetInventorInfo().inventoryItemData.shapeId];
                var pos = _gridManager.GetPositionCell(shape, _inventoryItem.GetInventorInfo().inventoryItemData.startX,
                _inventoryItem.GetInventorInfo().inventoryItemData.startY, _previousGrid);

                m_rect.anchoredPosition = pos;
                _previousPosition = m_rect.anchoredPosition;
                _gridManager.OnDragChangeColorCells(_nextCells, _previousGrid, true);

                _gridManager.AddInventoryItemsInfoEndDrag(_inventoryItem.GetInventorInfo(), _previousGrid);
                _gridManager.ChangeStatusCellEndDrag(_nextCells, _previousGrid, StatusCell.Occupied);

            }
            else
            {
                m_rect.anchoredPosition = _previousPosition;
                _gridManager.OnDragChangeColorCells(_nextCells, _previousGrid, true);
                _gridManager.ChangeStatusCellEndDrag(_curentCells, _previousGrid, StatusCell.Occupied);
                _inventoryItem.GetInventorInfo().inventoryItemData.startX = _curentCells[0].GetCellData().r;
                _inventoryItem.GetInventorInfo().inventoryItemData.startY = _curentCells[0].GetCellData().c;


            }
            _curentCells.Clear();
            _nextCells.Clear();
            _previouCells.Clear();

        }

        private bool CanMoveItem(List<Cell> cells)
        {
            foreach (var cell in cells)
            {
                if (cell.GetStatusCell() == StatusCell.Occupied)
                    return false;
            }
            return true;
        }
    }
}
