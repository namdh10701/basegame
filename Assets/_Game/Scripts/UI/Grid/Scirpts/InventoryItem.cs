using _Base.Scripts.UI;
using _Game.Scripts;
using UnityEngine;
using UnityEngine.UI;
namespace _Base.Scripts.UI
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        private InventoryItemInfo _inventoryItemInfo;
        private int[,] _shape;

        public void Setup(InventoryItemInfo inventoryItemInfo)
        {
            _inventoryItemInfo = inventoryItemInfo;
            _icon.sprite = inventoryItemInfo.inventoryItemData.sprite;
            _icon.SetNativeSize();
            _shape = Shape.ShapeDic[inventoryItemInfo.inventoryItemData.shapeId];
            this.transform.localPosition = inventoryItemInfo.inventoryItemData.position;
        }

        public int[,] GetShape()
        {
            return _shape;
        }

        public void Setposition(Vector2 position)
        {
            _inventoryItemInfo.inventoryItemData.position = position;
        }

        public InventoryItemInfo GetInventorInfo()
        {
            return _inventoryItemInfo;
        }

        public void UpdateInventoryItemInfo(InventoryItemInfo inventoryItemInfo)
        {
            _inventoryItemInfo = inventoryItemInfo;
            this.transform.localPosition = inventoryItemInfo.inventoryItemData.position;
        }

    }
}
