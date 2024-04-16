using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private ItemMenuData _itemMenuData;
    private List<Cell> _cells = new List<Cell>();
    private string _gridID;

    public string OldGridID;
    public Vector2 OldPosition;

    public void Setup(ItemMenuData itemMenuData, string oldGridID, Vector2 oldPosition)
    {
        _itemMenuData = itemMenuData;
        _spriteRenderer.sprite = itemMenuData.sprite;
        OldGridID = oldGridID;
        OldPosition = oldPosition;
        _collider = this.GetComponent<BoxCollider2D>();
        SetSizeItemDrag(itemMenuData.sizeCollision);
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

            if (!_cells.Contains(cell))
            {
                _cells.Add(cell);
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
                cell.SetItemType(ItemType.None);
                _cells.Remove(cell);
            }

        }
    }

    public ItemMenuData GetItemMenuData()
    {
        return _itemMenuData;
    }
    
    public void GetCellSelectFromWeaponItem(ItemMenuData itemMenuData)
    {

        if (itemMenuData.numbCell == _cells.Count && CheckCellsEmty())
        {
            foreach (var item in _cells)
            {
                item.SetItemType(itemMenuData.itemType);

            }
            SetupWeaponsManager.Instance.OnChangeDataByMoveWeaponItem(_gridID, _cells, this);
        }
        else
        {
            SetupWeaponsManager.Instance.ReturnWeaponItemToPreviousPosition(this);
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
