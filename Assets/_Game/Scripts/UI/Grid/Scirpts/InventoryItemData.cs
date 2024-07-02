using _Game.Features.Inventory;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Inventory Item Data")]
    public class InventoryItemData : ScriptableObject
    {
        public int startX;
        public int startY;
        public string gridID;
        public Vector2 position;
        public string Id;
        public ItemType Type;
        public int[,] Shape;
        public Sprite Image;
    }
}
