using _Base.Scripts.UI;
using _Game.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    private InventoryItemData _inventoryItemData;
    private int[,] _shape;

    public void Setup(InventoryItemData inventoryItemData)
    {
        _inventoryItemData = inventoryItemData;
        _icon.sprite = _inventoryItemData.sprite;
        _icon.SetNativeSize();
        _shape = Shape.ShapeDic[_inventoryItemData.shapeId];
    }

    public int[,] GetShape()
    {
        return _shape;
    }

}
