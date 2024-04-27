using UnityEngine;
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
        _icon.color = itemMenuData.color;
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
        var screenPosition = new Vector2(mousePosition.x, mousePosition.y);
        DragHandler(screenPosition);
    }

    private void Drop() 
    {
        _isDragging = false;
        _icon.enabled = false;
    }

    public void DragHandler(Vector2 posMouse)
    {
        _icon.enabled = true;
        var pos = new Vector2(posMouse.x, posMouse.y + 100);
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, pos, _canvas.worldCamera, out position);
        transform.position = _canvas.transform.TransformPoint(position);
    }
}
