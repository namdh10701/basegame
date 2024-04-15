using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    public ItemMenuData ItemMenuData;
    private BoxCollider2D _collider;

    public List<Cell> _cells = new List<Cell>();
    public string GridID;

    public void Setup(ItemMenuData itemMenuData)
    {
        _collider = this.GetComponent<BoxCollider2D>();
        _collider.enabled = true;
        ItemMenuData = itemMenuData;
        SetSizeItemDrag(ItemMenuData.sizeCollision);
    }

    private void SetSizeItemDrag(Vector2 size)
    {
        _collider.size = size;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Cell")
        {
            var cell = collider2D.gameObject.GetComponent<Cell>();
            cell.itemType = ItemMenuData.itemType;

            var grid = collider2D.gameObject.GetComponentInParent<Grid>();
            GridID = grid.ID;

            // Kiểm tra xem cell đã tồn tại trong danh sách _cells chưa
            if (!_cells.Contains(cell))
            {
                _cells.Add(cell); // Thêm cell vào danh sách _cells
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Cell")
        {
            var cell = collider2D.gameObject.GetComponent<Cell>();
            if (_cells.Contains(cell))
            {
                _cells.Remove(cell);
            }

        }
    }


    public void GetCellSelect(ItemMenuData itemMenuData)
    {
        if (itemMenuData.numbCell == _cells.Count)
        {
            foreach (var item in _cells)
            {
                item.itemType = itemMenuData.itemType;
            }
            SetupWeaponsManager.Instance.SetDataToCells(GridID, _cells);
        }

    }
}
