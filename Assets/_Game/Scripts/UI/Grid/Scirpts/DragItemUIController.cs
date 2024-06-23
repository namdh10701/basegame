using System.Collections.Generic;
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
        List<Cell> oldCells = new List<Cell>();
        List<Cell> newCells = new List<Cell>();
        List<Cell> curentCells = new List<Cell>();
        GridInfor _previousGrid;
        void Start()
        {
            m_rect = this.gameObject.GetComponentInParent<RectTransform>();
            _gridManager = this.gameObject.GetComponentInParent<GridManager>();
            _parent = this.gameObject.transform.parent.GetComponent<RectTransform>();
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
            var result = _gridManager.GetCellsByPosition(Input.mousePosition, position, _inventoryItem, curentCells.Count == 0);
            // if (result.cells.Count == 0 ) return;
            if (result.grid == null)
            {
                var c = Color.white;
                c.a = 0;
                _gridManager.OnDragChangeColorCells(oldCells, _previousGrid, c, true);
            }
            else
                ChangeColor(result.cells, result.grid);
        }

        private void ChangeColor(List<Cell> cells, GridInfor grid)
        {
            curentCells = cells;
            if (cells != newCells)
            {
                if (newCells != null && newCells.Count > 0)
                {
                    var c = Color.white;
                    c.a = 0;
                    _gridManager.OnDragChangeColorCells(newCells, grid, c, true);
                }
                newCells = cells;
                _gridManager.OnDragChangeColorCells(newCells, grid, Color.green);
                oldCells = newCells;
                _previousGrid = grid;
            }


        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_parent, Input.mousePosition, Camera.main, out position);
            m_rect.anchoredPosition = position;

        }
    }
}
