using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private WeaponItemData _weaponItemData;
    private List<Cell> _cells = new List<Cell>();
    private string _gridID;

    public string PreviousGridID;
    public Vector2 PreviousPosition;
    private bool _isDesTroy;

    public void Setup(WeaponItemData weaponItemData, string oldGridID, Vector2 oldPosition)
    {
        _weaponItemData = weaponItemData;
        _spriteRenderer.sprite = weaponItemData.itemMenuData.sprite;
        _weaponItemData.previousGridID = oldGridID;
        _weaponItemData.previousPosition = oldPosition;
        _collider = this.GetComponent<BoxCollider2D>();
        SetSizeItemDrag(weaponItemData.itemMenuData.sizeCollision);
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
            if (cell == null || !cell.IsCellEmty())
                return;

            cell.CheckCellsEmty(false);
            cell.SetItemType(_weaponItemData.itemMenuData.itemType);
            var grid = cell.GetComponentInParent<Grid>();
            _gridID = grid.ID;
            cell.EnableCell(false);
            _cells.Add(cell);
            Debug.Log("OnTriggerEnter2D:" + _cells.Count);

        }
        else if (collider2D.gameObject.tag == "OutSize")
        {
            _isDesTroy = true;
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
                    cell.CheckCellsEmty(true);
                    cell.SetItemType(ItemType.None);
                    cell.EnableCell(true);
                    cellsToRemove.Add(item);
                }
            }
            foreach (var item in cellsToRemove)
            {
                _cells.Remove(cell);

            }
            Debug.Log("OnTriggerExit2D:" + _cells.Count);

        }
        else if (collider2D.gameObject.tag == "OutSize")
        {
            _isDesTroy = false;
        }
    }

    public ItemMenuData GetItemMenuData()
    {
        return _weaponItemData.itemMenuData;
    }

    public WeaponItemData GetWeaponItemData()
    {
        return _weaponItemData;

    }

    public void GetCellSelectFromWeaponItem(ItemMenuData itemMenuData)
    {
        if (_isDesTroy)
        {
            SetupWeaponsManager.Instance.RemoveDataWeaponItem(itemMenuData);
            Destroy(this.gameObject);
        }
        else if (itemMenuData.numbCell == _cells.Count)
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

}
