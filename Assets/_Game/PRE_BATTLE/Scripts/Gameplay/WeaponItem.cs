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
    private bool _isDesTroy;

    public void Setup(WeaponItemData weaponItemData)
    {
        _weaponItemData = weaponItemData;
        _spriteRenderer.sprite = weaponItemData.itemMenuData.sprite;
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
            if (cell == null)
                return;

            if (!cell.IsCellEmty())
                return;

            // Store item type in a local variable for readability and efficiency
            var itemType = _weaponItemData.itemMenuData.itemType;

            // Set cell properties and add to _cells list
            cell.CheckCellsEmty(false);
            cell.SetItemType(itemType);
            cell.EnableCell(false);
            _cells.Add(cell);
            _gridID = cell.GetComponentInParent<Grid>().ID;
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

            // Remove cells from _cells list and update cell properties
            foreach (var item in _cells.ToList()) // Convert to List to avoid modifying while iterating
            {
                if (item.Id == cell.Id)
                {
                    cell.CheckCellsEmty(true);
                    cell.SetItemType(ItemType.None);
                    cell.EnableCell(true);
                    _cells.Remove(item); // Remove the correct item from the list
                }
            }
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
        else if (itemMenuData.numbCell != _cells.Count)
        {
            SetupWeaponsManager.Instance.ReturnWeaponItemToPreviousPosition(this);
            return;
        }

        foreach (var item in _cells)
        {
            item.SetItemType(itemMenuData.itemType);

        }
        SetupWeaponsManager.Instance.OnChangeDataByMoveWeaponItem(_gridID, _cells, this);

    }

}
