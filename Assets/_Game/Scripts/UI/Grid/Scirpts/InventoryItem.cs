using UnityEngine;
using UnityEngine.UI;
namespace _Base.Scripts.UI
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] public Image Icon;
        [SerializeField] public Button BtnClose;
        private InventoryItemData _inventoryItemData;
        private int[,] _shape;
        private bool _isActive = true;
        public void Setup(InventoryItemData inventoryItemData)
        {
            _inventoryItemData = inventoryItemData;
            Icon.sprite = inventoryItemData.Image;
            // Icon.sprite = Resources.Load<Sprite>(inventoryItemData.gridItemDef.Image);
            Icon.SetNativeSize();
            _shape = inventoryItemData.Shape;
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

        public void EnableButtonClose(System.Action cb)
        {
            BtnClose.gameObject.SetActive(_isActive);
            BtnClose.onClick.AddListener(() => cb?.Invoke());
            _isActive = !_isActive;
        }

    }
}
