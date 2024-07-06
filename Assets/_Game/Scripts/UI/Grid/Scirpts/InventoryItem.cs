using _Game.Features.Inventory;
using UnityEngine;
using UnityEngine.UI;
using _Game.Scripts.DB;
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
            switch (_inventoryItemData.Type)
            {
                case ItemType.CANNON:
                    Icon.sprite = _Game.Scripts.DB.Database.GetCannonImage(_inventoryItemData.Id);
                    break;
                case ItemType.CREW:
                    Icon.sprite = _Game.Scripts.DB.Database.GetCrewImage(_inventoryItemData.Id);
                    break;
                case ItemType.AMMO:
                    Icon.sprite = _Game.Scripts.DB.Database.GetAmmoImage(_inventoryItemData.Id);
                    break;

            }
            Icon.SetNativeSize();
            _shape = _Game.Scripts.DB.Database.GetShapeByTypeAndOperationType(inventoryItemData.Id, inventoryItemData.Type);
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
