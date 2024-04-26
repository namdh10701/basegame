using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItemUI : MonoBehaviour
{
    [SerializeField] Image _icon;
    private ItemMenuData _itemMenuData;
    private Canvas _canvas;
    private bool _isDragging;
    public void Setup(ItemMenuData itemMenuData, Canvas canvas)
    {
        _icon.sprite = itemMenuData.sprite;
        // _icon.SetNativeSize();
        _itemMenuData = itemMenuData;
        _canvas = canvas;
        _isDragging = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Drop();
            return;

        }
        if (!_isDragging)
            return;
        var mousePosition = Input.mousePosition;
        var screenPosition = mousePosition;
        DragHandler(screenPosition);
    }

    private void Drop()
    {
        _isDragging = false;
        _icon.enabled = false;
    }

    public void DragHandler(Vector2 posMouse)
    {
        Debug.Log("dsfdsf");
        _icon.enabled = true;
        var pos = new Vector2(posMouse.x, posMouse.y + 100);
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, pos, _canvas.worldCamera, out position);
        transform.position = _canvas.transform.TransformPoint(position);
    }
}
