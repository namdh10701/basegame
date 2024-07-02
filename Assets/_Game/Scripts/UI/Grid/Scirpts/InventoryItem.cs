using _Base.Scripts.UI;
using _Game.Scripts;
using UnityEngine;
using UnityEngine.UI;
namespace _Base.Scripts.UI
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] public Image Icon;
        private InventoryItemData _inventoryItemData;
        private int[,] _shape;

        public void Setup(InventoryItemData inventoryItemData)
        {
            _inventoryItemData = inventoryItemData;
            // Icon.sprite = inventoryItemInfo.inventoryItemData.gridItemDef.Image;
            Icon.sprite = Resources.Load<Sprite>(inventoryItemData.gridItemDef.Path);
            Icon.SetNativeSize();
            _shape = Shape.ShapeDic[inventoryItemData.gridItemDef.ShapeId];
            this.transform.localPosition = inventoryItemData.position;
        }

        public int[,] GetShape()
        {
            return _shape;
        }

        public void Setposition(Vector2 position)
        {
            _inventoryItemData.position = position;
        }

        public InventoryItemData GetInventorInfo()
        {
            return _inventoryItemData;
        }

        public void UpdateInventoryItemInfo(InventoryItemData inventoryItemData)
        {
            this._inventoryItemData = inventoryItemData;
            this.transform.localPosition = inventoryItemData.position;
        }

    }
}
