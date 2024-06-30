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
        GridInfor _curentGrid;
        Vector2 _previousPosition;
        int _previousParentIndex;
        int _curentParentIndex = -1;

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
            this.gameObject.transform.SetParent(parent.transform);
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_parent, Input.mousePosition, Camera.main, out position);
            m_rect.anchoredPosition = position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

            Debug.Log("OnBeginDrag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            var parentIndex = _gridManager.GetParentIndex(Input.mousePosition);
            if (parentIndex != _curentParentIndex && parentIndex != -1)
            {
                if (_curentParentIndex == -1)
                {
                    _previousParentIndex = parentIndex;
                }
                _curentParentIndex = parentIndex;
            }
            if (parentIndex == -1)
                parentIndex = _previousParentIndex;

            _curentGrid = _gridManager.GridConfig.grids[parentIndex];


            Debug.Log("[OnDrag] _curentParentIndex: " + _curentParentIndex);
            Debug.Log("[OnDrag] _previousParentIndex: " + _previousParentIndex);
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_gridManager.ParentCells[parentIndex], Input.mousePosition, Camera.main, out position);
            m_rect.anchoredPosition = position;
            var result = _gridManager.GetCellsByPosition(this, parentIndex, Input.mousePosition, position, _inventoryItem, _curentCells.Count == 0);
            if (result.grid == null || result.cells.Count == 0)
            {
                _gridManager.OnDragChangeColorCells(_nextCells, _curentGrid, true);
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
            }


        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_nextCells.Count > 0 && CanMoveItem(_nextCells))
            {
                _gridManager.ChangeStatusCellEndDrag(_nextCells, _curentGrid, StatusCell.Empty);

                var inventoryItemsInfoRemoveGrid = new List<InventoryItemInfo>();
                inventoryItemsInfoRemoveGrid.Add(_inventoryItem.GetInventorInfo());
                _gridManager.RemoveInventoryItemsInfo(inventoryItemsInfoRemoveGrid, _curentGrid);

                // Get pos 
                _inventoryItem.GetInventorInfo().inventoryItemData.position = _nextCells[0].GetCellData().position;
                _inventoryItem.GetInventorInfo().inventoryItemData.gridID = _curentGrid.id;
                var shape = Shape.ShapeDic[_inventoryItem.GetInventorInfo().inventoryItemData.gridItemDef.ShapeId];
                var pos = _gridManager.GetPositionCell(shape, _inventoryItem.GetInventorInfo().inventoryItemData.startX,
                _inventoryItem.GetInventorInfo().inventoryItemData.startY, _curentGrid);

                m_rect.anchoredPosition = pos;
                _previousPosition = m_rect.anchoredPosition;
                _gridManager.OnDragChangeColorCells(_nextCells, _curentGrid, true);

                _gridManager.AddInventoryItemsInfoEndDrag(_inventoryItem.GetInventorInfo(), _curentGrid);
                _gridManager.ChangeStatusCellEndDrag(_nextCells, _curentGrid, StatusCell.Occupied);
                _previousParentIndex = _curentParentIndex;
                _curentParentIndex = -1;

            }
            else
            {
                foreach (var grid in _gridManager.GridConfig.grids)
                {
                    if (_previousParentIndex != _curentParentIndex)
                    {
                        _curentGrid = _gridManager.GridConfig.grids[_curentParentIndex];
                        _gridManager.OnDragChangeColorCells(_nextCells, _curentGrid, true);

                        _curentGrid = _gridManager.GridConfig.grids[_previousParentIndex];
                        _parent = _gridManager.ParentCells[_previousParentIndex];
                        this.gameObject.transform.SetParent(_parent.transform);
                    }
                    m_rect.anchoredPosition = _previousPosition;
                    _gridManager.OnDragChangeColorCells(_nextCells, _curentGrid, true);
                    _gridManager.ChangeStatusCellEndDrag(_curentCells, _curentGrid, StatusCell.Occupied);
                    _inventoryItem.GetInventorInfo().inventoryItemData.startX = _curentCells[0].GetCellData().r;
                    _inventoryItem.GetInventorInfo().inventoryItemData.startY = _curentCells[0].GetCellData().c;


                }
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
