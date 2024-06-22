using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Inventory Item Data")]
    public class InventoryItemData : ScriptableObject
    {
        public int shapeId;
        public string gridID;
        public InventoryItemType inventoryItemType;
        public Vector2 position;
        public Sprite sprite;
    }

    public enum InventoryItemType
    {
        None = 0,
        Gun = 1,
        Bullet = 2,
        Crew
    }
}
