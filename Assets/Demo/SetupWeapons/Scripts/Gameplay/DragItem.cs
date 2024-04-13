using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    private ItemMenuData _itemMenuData;
    private BoxCollider2D _collider;

    public void Setup(ItemMenuData itemMenuData)
    {
        _collider = this.GetComponent<BoxCollider2D>();
        _collider.enabled = true;
        _itemMenuData = itemMenuData;
        SetSizeItemDrag(_itemMenuData.SizeItem);
    }

    private void SetSizeItemDrag(Vector2 size)
    {
        _collider.size = size;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log("OnTriggerEnter2D");
    }
}
