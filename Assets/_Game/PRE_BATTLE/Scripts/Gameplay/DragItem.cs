using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    private ItemMenuData _itemMenuData;
    private BoxCollider2D _collider;

    private List<Cell> _cells = new List<Cell>();
    private string _gridID;

    public void Setup(ItemMenuData itemMenuData)
    {
        _collider = this.GetComponent<BoxCollider2D>();
        _itemMenuData = itemMenuData;
        SetSizeItemDrag(_itemMenuData.sizeCollision);
    }

    private void SetSizeItemDrag(Vector2 size)
    {
        _collider.offset = new Vector2(0, 0);
        _collider.size = size;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Cell")
        {
            var cell = collider2D.gameObject.GetComponent<Cell>();
            cell.SetItemType(_itemMenuData.itemType);

            var grid = collider2D.gameObject.GetComponentInParent<Grid>();
            _gridID = grid.ID;

            if (!_cells.Any(c => c.Id == cell.Id))
            {
                cell.SetItemType(_itemMenuData.itemType);
                cell.OnChangeColorCell(true);
                _cells.Add(cell);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Cell")
        {
            var cell = collider2D.gameObject.GetComponent<Cell>();
            var cellsToRemove = new List<Cell>();
            foreach (var item in _cells)
            {
                if (item.Id == cell.Id)
                {
                    cell.SetItemType(ItemType.None);
                    cell.OnChangeColorCell(false);
                    cellsToRemove.Add(cell);
                }
            }
            foreach (var item in cellsToRemove)
            {
                _cells.Remove(item);
            }
        }
    }

    public ItemMenuData GetItemMenuData()
    {
        return _itemMenuData;
    }


    public void GetCellSelectFromDragItem(ItemMenuData itemMenuData)
    {
        if (itemMenuData.numbCell == _cells.Count && CheckCellsEmty())
        {
            foreach (var item in _cells)
            {
                item.SetItemType(itemMenuData.itemType);

            }
            SetupWeaponsManager.Instance.SetDataToCells(_gridID, _cells, itemMenuData);
        }


    }

    public bool CheckCellsEmty()
    {
        foreach (var item in _cells)
        {
            if (!item.IsCellEmty())
            {
                return false;
            }

        }
        return true;
    }
}